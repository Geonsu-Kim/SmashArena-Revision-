using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMeleeAnimEvent : EnemyAnimationEvent
{

    private int randomPos = 0;
    public Transform[] StrikePos;
    // Start is called before the first frame update
    private void Skeleton_MeleeAttackStart(AnimationEvent animationEvent)
    {
        EnabledCollider(animationEvent.intParameter);
    }

    private void Skeleton_MeleeAttackEnd(AnimationEvent animationEvent)
    {
        DisabledCollider(animationEvent.intParameter);
    }

    private void OnSummon() //Summon
    {
        for (int i = 0; i < 3; i++)
        {
            ObjectPoolManager.Instance.CallObject("BuffMark", points[i].transform.position + Vector3.up * 0.2f, Quaternion.Euler(-90,0,0), true, 2.0f);

            ObjectPoolManager.Instance.CallObject("SkeletonInfantry", points[i]);
        }
    }
    private void OnDisappointed() //중대장은 너희에게 실망했다
    {
        ObjectPoolManager.Instance.CallObject("InfantryBuff", this.transform.position + Vector3.up * 0.2f, Quaternion.identity,true,2.0f);
        
        Collider[] colls = Physics.OverlapSphere(this.transform.position + this.transform.forward * 0.1f, 3f, 1 << 8);
        
        for (int i = 0; i < 5; i++)
        {
            FSMEnemy melee = colls[i].GetComponent<FSMEnemy>();
            if (melee == null)
            {
                i--;
                continue;
            }
            else
            {
                ObjectPoolManager.Instance.CallObject("BuffMark", melee.transform.position + Vector3.up * 0.2f, Quaternion.identity,true,2.0f);
                StartCoroutine(melee.Buff());
            }
        }

    }
    private void OnCompanyStrikeReady() //Company Strike
    {
        randomPos = Random.Range(0, StrikePos.Length);
       /* for (int i = 0; i < StrikePos.Length; i++)
        {
            if (i == randomPos) continue;
            ObjectPoolManager.Instance.CallObject("StrikeMark", StrikePos[i].transform.position + Vector3.up * 0.2f, Quaternion.identity, true, 2.0f);
        }*/
    }
    private void OnCompanyStrike()
    { 
    Debug.Log(randomPos);
        for (int i = 0; i < StrikePos.Length; i++)
        {
            if (i == randomPos) continue;
            ObjectPoolManager.Instance.CallObject("SkeletonStriker", StrikePos[i].transform.position + Vector3.up * 0.2f, Quaternion.identity, true, 2.0f);
        }
    }
}
