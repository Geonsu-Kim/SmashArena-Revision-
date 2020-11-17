using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public abstract class FSMBase : MonoBehaviour
{

    private const string stringParam = "state";


    protected bool invincibility = false;
    protected bool isNewState;

    [SerializeField]
    protected State m_state;
    protected Animator animator;
    protected SkinnedMeshRenderer[] skin;
    protected MeshRenderer[] mesh;
    protected List<Material> mats;//skin,mesh
    protected Color originalColor = new Color(0, 0, 0);

    public CharacterState characterState;
    public Health health;
    public bool Invincibility { get { return invincibility; } }
    public bool isDead { get { return m_state == State.Dead; } }
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        mesh = GetComponentsInChildren<MeshRenderer>();
        mats = new List<Material>();
        for (int i = 0; i < skin.Length; i++)
        {
            for (int j = 0; j < skin[i].materials.Length; j++)
            {
                mats.Add(skin[i].materials[j]);
            }
        }
        for (int i = 0; i < mesh.Length; i++)
        {
            for (int j = 0; j < mesh[i].materials.Length; j++)
            {
                mats.Add(mesh[i].materials[j]);
            }
        }
    }
    protected virtual void OnEnable()
    {
        m_state = State.Idle;
        StartCoroutine(FSMMain());
    }

    protected void ChangeColor(Color color)
    {
        for (int i = 0; i < mats.Count; i++)
        {
            mats[i].SetColor("_Emission", color);
        }
    }
    public void Damaged(float amount)
    {
        if (isDead) return;

        health.Damaged(amount);
        ObjectPoolManager.Instance.CallText("DamageText", this.transform.position + Vector3.up * 1.0f, amount);
        StartCoroutine(ColorByHit());
        if (health.IsDead())
        {
            SetStateTrigger(State.Dead);
        }
    }
    public void SetState(State newState)
    {
        isNewState = true;
        m_state = newState;
        animator.SetInteger(stringParam, (int)m_state);
    }
    public void SetStateTrigger(State newState)
    {
        isNewState = true;
        m_state = newState;
        animator.SetTrigger(newState.ToString());
        animator.SetInteger(stringParam, (int)m_state);
    }
    IEnumerator FSMMain()
    {
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(m_state.ToString());
        }
    }


    protected abstract IEnumerator Idle();
    protected abstract IEnumerator Run();
    protected abstract IEnumerator Attack();
    protected abstract IEnumerator Dead();
    protected virtual IEnumerator Stun()
    {

        ChangeColor(Color.yellow);
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
            {

                ChangeColor(Color.clear);
                SetState(State.Idle);
            }
        } while (!isNewState);
    }
    protected IEnumerator ColorByHit()
    {
        ChangeColor(Color.red);
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        if (m_state == State.Stun)
            ChangeColor(Color.yellow);
        else
        ChangeColor(Color.clear);
    }
}
