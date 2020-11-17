using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSkeletonRanged : FSMEnemy
{
    public Transform FirePos;
    protected override void Start()
    {
        base.Start();
        ObjectPoolManager.Instance.CreateObject("NormalArrow");
    }
    protected override IEnumerator Idle()
    {
        do
        {
            if (isDead) break;
            yield return null;
            if (!DistanceCheck(player.transform.position, characterState.AttackRange))
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
        do
        {
            if (isDead) break;
            agent.TraceTarget(Player.transform.position);
            yield return null;
            if (DistanceCheck(player.transform.position, characterState.ChasingRange))
            {
                if (DistanceCheck(player.transform.position, characterState.AttackRange))
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
        agent.Stop();
        do
        {
            if (isDead) break;
            yield return null;
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f)
            {
                RotateToPlayer();
                if (!DistanceCheck(player.transform.position, characterState.AttackRange))
                {
                    SetState(State.Run);
                }
            }
        } while (!isNewState);
    }
}

