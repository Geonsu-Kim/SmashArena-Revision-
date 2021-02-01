using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKingAnimEvent : EnemyAnimationEvent
{
    private const string enemyName = "SkeletonKing";
    private Vector3[] ExplosionPos=new Vector3[5];

    public GameObject kingShield;


    private void Skeleton_KingAttackStart(AnimationEvent animationEvent)
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

    private void Skeleton_KingAttackEnd(AnimationEvent animationEvent)
    {
        WeaponOff(0);
    }
    private void WaveAttackReady()
    {

        ObjectPoolManager.Instance.CallIndicator(this.transform.position + Vector3.up * 0.1f, this.transform.rotation*Quaternion.Euler(7f, 0, 0f), 1, 2.5f, 7f);
    }
    private void WaveAttack()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("WaveAttack");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);

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

        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("Buff");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        ObjectPoolManager.Instance.CallObject("Rage", this.transform.position + Vector3.up * 0.2f, Quaternion.Euler(90f,0f,0f), true, 2.0f);
        StartCoroutine(Enemy.Buff(2f));
    }
    private void KingShield()
    {
        StartCoroutine(UsingKingShield());

        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("Shield");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
    }
    IEnumerator Targeting()
    {
        for (int k = 0; k < ExplosionPos.Length; k++)
        {
            ExplosionPos[k] = PlayerManager.Instance.Player.transform.position;
            ObjectPoolManager.Instance.CallObject("SkillMark_King", ExplosionPos[k] + Vector3.up * 0.1f, Quaternion.identity, true, 2f);
            yield return YieldInstructionCache.WaitForSeconds(0.33f);
        }
    }
    IEnumerator ExcuteExplosion()
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("Explosion");
        SFXname = stringBuilder.ToString();
        for (int k = 0; k < ExplosionPos.Length; k++)
        {

            SoundManager.Instance.PlaySFX(SFXname);
            ObjectPoolManager.Instance.CallObject("Explosion", ExplosionPos[k] + Vector3.up * 0.1f, Quaternion.Euler(-90f, 0f, 0f), true, 2f);
            checkedColliders = OverLapRaycast.CheckSphere(1.5f, ExplosionPos[k]);
            for (int i = 0; i < checkedColliders.Length; i++)
            {
                FSMPlayer player = checkedColliders[i].gameObject.GetComponent<FSMPlayer>();
                if (player != null)
                    player.Damaged(2000f);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.33f);
        }
    }
    IEnumerator UsingKingShield()
    {
        kingShield.SetActive(true);
        Enemy.defenseBuff = true;

        yield return YieldInstructionCache.WaitForSeconds(15f);

        Enemy.defenseBuff = false;
        kingShield.SetActive(false); ;
    }
   
}
