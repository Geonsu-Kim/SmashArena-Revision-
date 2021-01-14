using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKingAnimEvent : EnemyAnimationEvent
{

    private Vector3[] ExplosionPos=new Vector3[5];

    public GameObject kingShield;
    private void Skeleton_KingAttackStart(AnimationEvent animationEvent)
    {
        WeaponOn(0);
    }

    private void Skeleton_KingAttackEnd(AnimationEvent animationEvent)
    {
        WeaponOff(0);
    }
    private void WaveAttack()
    {
        ObjectPoolManager.Instance.CallObject("WaveAttack", this.transform.position + Vector3.up * 0.1f, this.transform.rotation*Quaternion.Euler(0f,0f,90f), true, 4f);
    }
    private void DarkExplosionMark()
    {
        StartCoroutine(Targeting());
    }
    private void DarkExplosion()
    {

        StartCoroutine(ExcuteExplosion());
    }
    private void KingBuff()
    {
        ObjectPoolManager.Instance.CallObject("Rage", this.transform.position + Vector3.up * 0.2f, Quaternion.Euler(90f,0f,0f), true, 2.0f);
        StartCoroutine(Enemy.Buff(2f));
    }
    private void KingShield()
    {
        StartCoroutine(UsingKingShield());
    }
    IEnumerator Targeting()
    {
        for (int k = 0; k < ExplosionPos.Length; k++)
        {
            float t = 0;
            ExplosionPos[k] = PlayerManager.Instance.Player.transform.position;
            ObjectPoolManager.Instance.CallObject("SkillMark_King", ExplosionPos[k] + Vector3.up * 0.1f, Quaternion.identity, true, 2f);
            while(t<0.33f)
            {
                yield return null;
                t += Time.deltaTime * Time.timeScale;
            }
        }
    }
    IEnumerator ExcuteExplosion()
    {
        for (int k = 0; k < ExplosionPos.Length; k++)
        {
            float t = 0;
            ObjectPoolManager.Instance.CallObject("Explosion", ExplosionPos[k] + Vector3.up * 0.1f, Quaternion.Euler(-90f, 0f, 0f), true, 2f);
            checkedColliders = OverLapRaycast.CheckSphere(1.5f, ExplosionPos[k]);
            for (int i = 0; i < checkedColliders.Length; i++)
            {
                FSMPlayer player = checkedColliders[i].gameObject.GetComponent<FSMPlayer>();
                if (player != null)
                    player.Damaged(3000);
            }
            while (t < 0.33f)
            {
                yield return null;
                t += Time.deltaTime * Time.timeScale;
            }
        }
    }
    IEnumerator UsingKingShield()
    {
        kingShield.SetActive(true);
        Enemy.defenseBuff = true;
        float t = 0;
        while (t < 15f)
        {
            t += Time.deltaTime * Time.timeScale;
            yield return null;
        }

        Enemy.defenseBuff = false;
        kingShield.SetActive(false); ;
    }
   
}
