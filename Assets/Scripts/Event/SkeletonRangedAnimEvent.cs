using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRangedAnimEvent : EnemyAnimationEvent
{
    private int randomNum;
    // Start is called before the first frame update
    public Transform FirePos;
    public Transform[] ArrowRainPts;

    private void Skeleton_RangedAttack()
    {
        ObjectPoolManager.Instance.CallObject("NormalArrow", FirePos, true, 2.0f);
    }

    private void OnMultiShot()
    {
        for (int i = 0; i < 5; i++)
        {
            ObjectPoolManager.Instance.CallObject("NormalArrow", FirePos.transform.position, FirePos.transform.rotation * Quaternion.Euler(0, -24 + i * 12, 0), true, 7.0f);
        }
    }
    private void OnChargeShotAiming()
    {
        ObjectPoolManager.Instance.CallObject("ChargingLightningShot", FirePos, true, 1.5f);
    }
    private void OnChargeShotFire() //Arrow Rain
    {
        ObjectPoolManager.Instance.CallObject("LightningShot", FirePos.transform, true, 3.0f);
        checkedColliders = OverLapRaycast.CheckBox((Vector3.forward * 5f + Vector3.up + Vector3.right*2f) * 0.5f, transform.position + transform.forward*2.5f + Vector3.up,transform.rotation);
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
    }
    private void OnArrowRainFire()
    {
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
            checkedColliders = OverLapRaycast.CheckBox((Vector3.right * 10f + Vector3.up * 8f + Vector3.forward * 5f) * 0.5f, ArrowRainPts[randomNum].transform.position + Vector3.up * 4f,Quaternion.identity);
            for (int i = 0; i < checkedColliders.Length; i++)
            {
                FSMPlayer player = checkedColliders[i].gameObject.GetComponent<FSMPlayer>();
                if(player!=null)
                player.Damaged(500);
            }
            count++;
        }

    }
}
