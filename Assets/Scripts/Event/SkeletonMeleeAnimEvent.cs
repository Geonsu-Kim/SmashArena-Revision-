using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
public class SkeletonMeleeAnimEvent : EnemyAnimationEvent
{
    private int randomPos = 0;
    public Transform[] StrikePts;
    public Transform[] SummonPts;
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
        for (int i = 0; i < SummonPts.Length; i++)
        {
            ObjectPoolManager.Instance.CallObject("BuffMark", SummonPts[i].transform.position + Vector3.up * 0.2f, Quaternion.Euler(-90,0,0), true, 2.0f);

            ObjectPoolManager.Instance.CallObject("SkeletonInfantry", SummonPts[i]);
        }
    }
    private void OnDisappointed() //중대장은 너희에게 실망했다
    {
        ObjectPoolManager.Instance.CallObject("InfantryBuff", this.transform.position + Vector3.up * 0.2f, Quaternion.identity,true,2.0f);
        
        Collider[] colls = Physics.OverlapSphere(this.transform.position + this.transform.forward * 0.1f, 3f, 1 << 8);
        int max = colls.Length > 5 ? 5 : colls.Length ;
        for (int i = 0; i < max; i++)
        {
            FSMEnemy melee = colls[i].GetComponent<FSMEnemy>();

                ObjectPoolManager.Instance.CallObject("BuffMark", melee.transform.position + Vector3.up * 0.2f, Quaternion.identity,true,2.0f);
                StartCoroutine(melee.Buff());
            
        }

    }
    private void OnCompanyStrikeReady() //Company Strike
    {
        randomPos = Random.Range(0, StrikePts.Length);/*
       for (int i = 0; i < StrikePts[randomPos].childCount; i++)
        {
            ObjectPoolManager.Instance.CallObject("StrikeMark", StrikePts[randomPos].GetChild(i).transform,true,2.0f);
        }*/
    }
    private void OnCompanyStrike()
    { 
        for (int i = 0; i < StrikePts[randomPos].childCount; i++)
        {
            ObjectPoolManager.Instance.CallObject("SkeletonStriker", StrikePts[randomPos].GetChild(i).transform, true, 2.0f);
        }
    }
}
