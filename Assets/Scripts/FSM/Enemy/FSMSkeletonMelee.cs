using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSkeletonMelee : FSMEnemy
{
    [SerializeField]
    private int enemyType=0;

    protected override void Awake()
    {
        base.Awake();
        switch (enemyType)
        {
            case 0:animator.SetInteger("atkType", 0); break;
            case 1:animator.SetInteger("atkType", Random.Range(0, 2)); break;
               
        }
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
        do
        {
            if (isDead()) break;
            agent.TraceTarget(Player.transform.position);
            yield return null;
            if (DistanceCheck(player.transform.position, 50f))
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
        agent.Stop();
        do
        {
            if (isDead()) break;
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f)
            {
                RotateToPlayer();
                if (enemyType==1)
                {
                    animator.SetInteger("atkType", Random.Range(0, 2));
                }
                if (!DistanceCheck(player.transform.position, info.EnemyAtkRange))
                {
                    SetState(State.Run);
                }
            }
        } while (!isNewState);
    }
}
