using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Agent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ItemDrop))]
public  class FSMEnemy : FSMBase
{
    private Vector3 Dir;
    [SerializeField]private int exp;
    protected FSMPlayer player;
    protected Agent agent;
    protected CapsuleCollider coll;
    protected ItemDrop drop;

    public EnemyInfo info;
    private Rigidbody rb;
    [HideInInspector] public bool defenseBuff = false;
    
    
    public Collider[] Weapon;
    public FSMPlayer Player { get { return player; } }
    protected virtual void Start()
    {
        player = PlayerManager.Instance.Player;
    }
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<Agent>();
        agent._Awake();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        drop = GetComponent<ItemDrop>();
        for (int i = 0; i < Weapon.Length; i++)
        {
            Weapon[i].enabled = false;
            Weapon[i].GetComponent<EffectTrigger>().DaamageScalar = info.EnemyAtkDamage;
        }
        InitStat();
    }
    protected override void InitStat()
    {
        health.MaxHP = info.EnemyMaxHp;
        health.Revive();
    }
    public override  void  Damaged(float amount,bool critical=false)
    {
        if (isDead()) return;
        if (critical) amount *= 2;
        if (defenseBuff) amount *= 0.5f;
        sb.Length = 0;
        sb.Append(((int)amount).ToString());
        health.Damaged(amount);
        ObjectPoolManager.Instance.CallText(sb.ToString(), this.transform.position + Vector3.up * 1.0f,Color.white) ;
        UIManager.Instance.RenewEnemyUI( info.EnemyNameTxtColor, info.EnemyName,health.Ratio());
        
        StartCoroutine(ColorByHit());
        if (health.IsDead())
        {
            SetStateTrigger(State.Dead);
            drop.DropItem(Random.Range(0, drop.Max));
            player.blueGem += info.BlueGem;
            PlayerManager.Instance.gainedExpInBattle+=exp;
            UIManager.Instance.EnemyInfo.SetActive(false);
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

    public IEnumerator Buff(float speed)
    {
        animator.speed = speed;
        float t = 0f;
        while (t < 7f)
        {
            t += Time.deltaTime * Time.timeScale;
            yield return null;
        }
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
            if (isDead()) break;
            yield return null;
            if (!isDead() && DistanceCheck(player.transform.position, 50))
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
        for (int i = 0; i < Weapon.Length; i++)
        {
            Weapon[i].enabled = false;
        }
        agent.Stop();
        coll.enabled = false;
        rb.isKinematic = true;

        yield return YieldInstructionCache.WaitForSeconds(2f);
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
        coll.enabled = true;

        rb.isKinematic = false;
        agent.Turn(true);
        health.Revive();
        for (int i = 0; i < mats.Count; i++)
        {
            mats[i].SetFloat("_SliceAmount", 0);
        }
    }

}
