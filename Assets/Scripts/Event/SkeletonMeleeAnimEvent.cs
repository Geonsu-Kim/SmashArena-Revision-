using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
public class SkeletonMeleeAnimEvent : EnemyAnimationEvent
{
    private int randomPos = 0;
    public Transform[] StrikePts;
    public Transform[] SummonPts;

    private const string enemyName = "SkeletonMelee";
    // Start is called before the first frame update
    private void Skeleton_MeleeAttackStart()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        int rand = Random.Range(0, 1);
        if (rand == 0)
        {
            stringBuilder.Append("Attack1");
        }
        else
        {
            stringBuilder.Append("Attack2");
        }
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        WeaponOn(0);
    }
    private void Skeleton_MeleeAttackEnd()
    {
        WeaponOff(0);
    }
    private void OnSummon() //Summon
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("Summon");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        for (int i = 0; i < SummonPts.Length; i++)
        {
            ObjectPoolManager.Instance.CallObject("Summon", SummonPts[i].transform.position + Vector3.up * 0.2f, Quaternion.Euler(-90,0,0), true, 2.0f);

            ObjectPoolManager.Instance.CallObject("SkeletonInfantry", SummonPts[i]);
        }
    }
    private void OnDisappointed() //중대장은 너희에게 실망했다
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("Buff");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        ObjectPoolManager.Instance.CallObject("Buff", this.transform.position + Vector3.up * 0.2f, Quaternion.identity,true,2.0f);
        Collider[] colls = Physics.OverlapSphere(this.transform.position + this.transform.forward * 0.1f, 3f, 1 << 8);
        int max = colls.Length > 5 ? 5 : colls.Length ;
        for (int i = 0; i < max; i++)
        {
            FSMEnemy melee = colls[i].GetComponent<FSMEnemy>();

                ObjectPoolManager.Instance.CallObject("GettingBuff", melee.transform.position + Vector3.up * 0.2f, Quaternion.Euler(-90f, 0f, 0f), true,2.0f);
                StartCoroutine(melee.Buff(1.2f));
            
        }
    }
    private void OnCompanyStrikeReady() //Company Strike
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("Summon");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        randomPos = Random.Range(0, StrikePts.Length);
        float y=0f;
        switch (randomPos)
        {
            case 0: y = -180f; break;
            case 1: y = -90f; break;
            case 2: y = 0f; break;
            case 3: y = 90f; break;
        }

        ObjectPoolManager.Instance.CallIndicator(StrikePts[randomPos].position + Vector3.up * 0.1f,Quaternion.Euler(13f, y, 0f), 1, 3, 7f);
    }
    private void OnCompanyStrike()
    { 
        for (int i = 0; i < StrikePts[randomPos].childCount; i++)
        {
            ObjectPoolManager.Instance.CallBulletTypeObj("SkeletonStriker", StrikePts[randomPos].GetChild(i).transform, 1000f, true, 2.0f);
        }
    }
}
