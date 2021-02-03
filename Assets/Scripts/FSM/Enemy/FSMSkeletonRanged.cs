using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSkeletonRanged : FSMEnemy
{
    public Transform FirePos;
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
                RotateToPlayer();
                if (!DistanceCheck(player.transform.position, info.EnemyAtkRange))
                {
                    SetState(State.Run);
                }
            }
        } while (!isNewState);
    }
}

