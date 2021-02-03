using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SkeletonRangedAnimEvent))]

public class FSMSkeletonCenturionArcher : FSMEnemy
{
    [SerializeField]
    private float[] MaxCooltime;
    private bool skillCooldown;
    private Queue<State> SkillQueue;
    protected override void Awake()
    {
        base.Awake();
        SkillQueue = new Queue<State>();

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        skillCooldown = false;
        SkillQueue.Clear();
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(SkillCoolDown(i));
        }
        StartCoroutine(QueueCoolDown(3f));
    }

    //스킬 자체의 쿨타임(쿨타임이 차면 스킬 준비 큐에 삽입된다)
    private IEnumerator SkillCoolDown(int num)
    {

        yield return YieldInstructionCache.WaitForSeconds(MaxCooltime[num]);
        switch (num)
        {
            case 0: SkillQueue.Enqueue(State.Skill1); break;
            case 1: SkillQueue.Enqueue(State.Skill2); break;
            case 2: SkillQueue.Enqueue(State.Skill3); break;
        }
    }

    //스킬 사용에 대한 쿨타임
    //큐에 사용할 스킬이 준비되어있어도 이 쿨타임이 다 차야 스킬을 사용할 수 있다.
    private IEnumerator QueueCoolDown(float param)
    {

        yield return YieldInstructionCache.WaitForSeconds(param);
        skillCooldown = true;
    }
    protected override IEnumerator Idle()
    {
        do
        {
            if (isDead()) break;
            yield return null;
            if (!DistanceCheck(player.transform.position, info.EnemyAtkRange))
            {
                SetState(State.Run);
            }
            else
            {

                SetState(State.Attack);
            }
        } while (!isNewState);
    }
    protected override IEnumerator Run()
    {
        RotateToPlayer();
        do
        {
            if (isDead()) break;
            agent.TraceTarget(Player.transform.position);
            yield return null;
            if (DistanceCheck(player.transform.position, info.EnemyChasingRange))
            {
                if (SkillQueue.Count != 0&& skillCooldown)
                {
                    agent.Stop();
                    StartCoroutine(QueueCoolDown(10f));
                    SetState(SkillQueue.Dequeue());
                    break;
                }
                if (DistanceCheck(player.transform.position, info.EnemyAtkRange))
                {
                    SetState(State.Attack);
                }

            }
            else
            {
                SetState(State.Idle);
            }
        } while (!isNewState);
    }
    protected override IEnumerator Attack()
    {
        RotateToPlayer();
        agent.Stop();
        do
        {
            if (isDead()) break;
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f)
            {
                if (SkillQueue.Count != 0&& skillCooldown)
                {
                    StartCoroutine(QueueCoolDown(10f));
                    SetState(SkillQueue.Dequeue());
                    break;
                }
                if (!DistanceCheck(player.transform.position, info.EnemyAtkRange))
                {
                    SetState(State.Run);
                }

            }
        } while (!isNewState);
    }
    protected override IEnumerator Dead()
    {
        yield return base.Dead();
        SkillQueue.Clear();
        StopCoroutine("SkillCoolDown");
        StopCoroutine("QueueCoolDown");
    }
    private IEnumerator Skill1()
    {

        skillCooldown = false;
        StartCoroutine(SkillCoolDown(0));

        do
        {
            if (isDead()) break;
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("ArcherSkill1On"))
            {

                if (!DistanceCheck(player.transform.position, info.EnemyAtkRange))
                {
                    SetState(State.Run);
                }
                else
                {
                    SetState(State.Attack);
                }

            }
        } while (!isNewState);
    }
    private IEnumerator Skill2()
    {
        skillCooldown = false;
        StartCoroutine(SkillCoolDown(1));
        do
        {
            if (isDead()) break;
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("ArcherSkill2On"))
            {

                if (!DistanceCheck(player.transform.position, info.EnemyAtkRange))
                {
                    SetState(State.Run);
                }
                else
                {
                    SetState(State.Attack);
                }

            }
        } while (!isNewState);
    }
    private IEnumerator Skill3()
    {
        skillCooldown = false;
        StartCoroutine(SkillCoolDown(2));
        do
        {
            if (isDead()) break;
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("ArcherSkill3On"))
            {

                if (!DistanceCheck(player.transform.position, info.EnemyAtkRange))
                {
                    SetState(State.Run);
                }
                else
                {
                    SetState(State.Attack);
                }

            }
        } while (!isNewState);
    }
}
