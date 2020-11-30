using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Agent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public  class FSMEnemy : FSMBase
{

    private Vector3 Dir;

    protected FSMPlayer player;
    protected Agent agent;
    protected new CapsuleCollider collider;
    public Rigidbody rb;
    public FSMPlayer Player { get { return player; } }

    public Collider[] colliders;
    protected virtual void Start()
    {
        player = GameSceneManager.Instance.Player;
        ObjectPoolManager.Instance.CreateObject("DamageText");
    }
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<Agent>();
        agent._Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        Debug.Log(m_state.ToString());
    }
    public override  void  Damaged(int amount,bool critical=false)
    {
        if (isDead) return;
        if (critical) amount *= 2;
        health.Damaged(amount);
        ObjectPoolManager.Instance.CallText("DamageText", this.transform.position + Vector3.up * 1.0f, amount);
        if (EnemyHpbar.Instance.GetActiveSelf()) EnemyHpbar.Instance.TurnOn();
        EnemyHpbar.Instance.RenewGauge(health.Ratio());
        StartCoroutine(ColorByHit());
        if (health.IsDead())
        {
            SetStateTrigger(State.Dead);
            EnemyHpbar.Instance.TurnOff();
        }
    }
    public bool DistanceCheck(Vector3 target, float dist)
    {
        return Vector3.SqrMagnitude(target - transform.position) <= (dist * dist);
    }
    public void RotateToPlayer()
    {
        Dir = new Vector3((player.transform.position - this.transform.position).x, 0, (player.transform.position - this.transform.position).z);
        if (Dir == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(Dir);
    }

    public IEnumerator Buff()
    {
        animator.speed = 1.2f;
        yield return YieldInstructionCache.WaitForSeconds(7.0f);
        animator.speed = 1f;
    }

    protected override IEnumerator Stun()
    {
        agent.Stop();
        return base.Stun();
    }
    protected override IEnumerator Idle()
    {
        agent.Stop();
        do
        {
            if (isDead) break;
            yield return null;
            if (!isDead && DistanceCheck(player.transform.position, 50))
            {
                SetState(State.Run);
            }
        } while (!isNewState);


    }
    protected override IEnumerator Run()
    {

        yield return null;
       
    }
    protected override IEnumerator Attack()
    {
        agent.Stop();
        yield return null;
    }


    protected override IEnumerator Dead()
    {
        yield return null;
        agent.Stop();
        collider.enabled = false;
        rb.isKinematic = true;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        yield return YieldInstructionCache.WaitForSeconds(2.0f);
        float amount = 0;
        do
        {
            yield return null;
            amount += Time.smoothDeltaTime*0.5f;
            for (int i = 0; i < mats.Count; i++)
            {
                mats[i].SetFloat("_SliceAmount", amount);
            }
        } while (amount<1);
        this.gameObject.SetActive(false);
        collider.enabled = true;

        rb.isKinematic = false;
        agent.Turn(true);
        health.Revive();
        for (int i = 0; i < mats.Count; i++)
        {
            mats[i].SetFloat("_SliceAmount", 0);
        }
    }

}
