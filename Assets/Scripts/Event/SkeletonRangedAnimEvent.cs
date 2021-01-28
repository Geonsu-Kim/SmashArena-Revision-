using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRangedAnimEvent : EnemyAnimationEvent
{
    private int randomNum;
    // Start is called before the first frame update
    public Transform FirePos;
    public Transform[] ArrowRainPts;

    private const string enemyName = "SkeletonRanged";
    private void Skeleton_RangedAttack()
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("Attack1");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        ObjectPoolManager.Instance.CallBulletTypeObj("NormalArrow", FirePos, Enemy.info.EnemyAtkDamage,true, 2.0f);
    }
    private void OnMultiShotReady()
    {
        ObjectPoolManager.Instance.CallIndicator(this.transform.position + Vector3.up*0.5f,this.transform.rotation* Quaternion.Euler(22f, 0f, 0f), 1f, 1f, 2f, false);

    }
    private void OnMultiShot()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("Attack2");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        for (int i = 0; i < 5; i++)
        {
            ObjectPoolManager.Instance.CallBulletTypeObj("NormalArrow", FirePos.transform.position, FirePos.transform.rotation * Quaternion.Euler(0, -24 + i * 12, 0), Enemy.info.EnemyAtkDamage*7f, true, 7.0f);
        }
    }
    private void OnChargeShotReady()
    {

        ObjectPoolManager.Instance.CallIndicator(this.transform.position + Vector3.up * 0.5f, transform.rotation * Quaternion.Euler(22f, 0f, 0f), 1, 1.25f, 2.5f);

    }
    private void OnChargeShotAiming()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("ElectricShotCharge");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        ObjectPoolManager.Instance.CallObject("ChargingLightningShot", FirePos, true, 1.5f);
    }
    private void OnChargeShotFire() //Arrow Rain
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("ElectricShot");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        ObjectPoolManager.Instance.CallObject("LightningShot", FirePos.transform, true, 3.0f);
        checkedColliders = OverLapRaycast.CheckBox((Vector3.forward * 5f + Vector3.up + Vector3.right * 2f) * 0.5f, transform.position + transform.forward * 2.5f + Vector3.up, transform.rotation);
        for (int i = 0; i < checkedColliders.Length; i++)
        {
            FSMPlayer player = checkedColliders[i].gameObject.GetComponent<FSMPlayer>();
            if (player != null)
                player.Damaged(2000);
        }
    }
    private void OnArrowRainReady()
    {
        randomNum = Random.Range(0, ArrowRainPts.Length);

        ObjectPoolManager.Instance.CallIndicator(this.ArrowRainPts[randomNum].transform.position + Vector3.up, Quaternion.Euler(90f, 0f, 0f), 3, 2f);
    }
    private void OnArrowRainFire()
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(enemyName);
        stringBuilder.Append("ArrowRain");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        ObjectPoolManager.Instance.CallObject("ArrowRain", ArrowRainPts[randomNum].transform.position + Vector3.up * 4f, Quaternion.identity, true, 3.0f);
        StartCoroutine(ArrowRainDamage());
    }
    IEnumerator ArrowRainDamage()
    {
        int count = 0;
        while (count < 10)
        {
            float t = 0;
            while (t < 0.33f)
            {
                yield return null;
                t += Time.deltaTime * Time.timeScale;
            }
            checkedColliders = OverLapRaycast.CheckBox((Vector3.right * 10f + Vector3.up * 8f + Vector3.forward * 5f) * 0.5f, ArrowRainPts[randomNum].transform.position + Vector3.up * 4f, Quaternion.identity);
            for (int i = 0; i < checkedColliders.Length; i++)
            {
                FSMPlayer player = checkedColliders[i].gameObject.GetComponent<FSMPlayer>();
                if (player != null)
                    player.Damaged(500);
            }
            count++;
        }

    }
}
