using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRangedAnimEvent : EnemyAnimationEvent
{
    private int randomNum;
    // Start is called before the first frame update
    public Transform FirePos;
    public Transform[] PArrowRainPts;

    private void Skeleton_RangedAttack()
    {
        ObjectPoolManager.Instance.CallObject("NormalArrow", FirePos, true, 2.0f);
    }
    private void OnHomingArrowReady() //Homing Arrow
    {

        ObjectPoolManager.Instance.CallObject("SkillMark", this.transform.position + Vector3.up * 0.1f, Quaternion.identity, true, 1.5f);
    }
    private void OnHomingArrowFire()
    {

        ObjectPoolManager.Instance.CallObject("HomingArrow", FirePos, true, 7.0f);
    }
    private void OnChargeShotReady() //Charge Shot
    {
        /*
        ObjectPoolManager.Instance.CallObject("ChargeShotMark", this.transform.position+ Vector3.up * 0.1f, Quaternion.identity, true, 1.5f);
    */
    }
    private void OnChargeShotAiming()
    {
        ObjectPoolManager.Instance.CallObject("ChargeShotAiming", FirePos, true, 3.0f);
    }
    private void OnChargeShotFire() //Arrow Rain
    {

        ObjectPoolManager.Instance.CallObject("ChargeShot", FirePos, true, 3.0f);
    }
    private void OnArrowRainReady()
    {
        randomNum = Random.Range(0, PArrowRainPts.Length);
        StartCoroutine(ArrowRain(false, PArrowRainPts[randomNum]));
    }
    private void OnArrowRainFire()
    {
        StartCoroutine(ArrowRain(true, PArrowRainPts[randomNum]));

    }
    IEnumerator ArrowRain(bool attack, Transform points)
    {
        if (attack)
        {
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < points.childCount; i++)
                {
                    yield return YieldInstructionCache.WaitForSeconds(0.02f);
                    ObjectPoolManager.Instance.CallObject("ExplosiveArrow", points.GetChild(i).position + Vector3.up * 30f, Quaternion.Euler(90, 90, -90));
                }
            }
        }
        else
        {
            for (int i = 0; i < points.childCount; i++)
            {
                yield return YieldInstructionCache.WaitForSeconds(0.02f);
                ObjectPoolManager.Instance.CallObject("SkillMark", points.GetChild(i).position + Vector3.up * 0.1f, Quaternion.identity, true, 1.5f);
            }
        }
    }
}
