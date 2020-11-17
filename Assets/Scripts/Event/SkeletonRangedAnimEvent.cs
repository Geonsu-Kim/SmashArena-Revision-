using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRangedAnimEvent : EnemyAnimationEvent
{
    // Start is called before the first frame update
    public Transform FirePos;

    protected override void Start()
    {
        base.Start();
    }
    private void Skeleton_RangedAttack()
    {
        ObjectPoolManager.Instance.CallObject("NormalArrow", FirePos, true, 2.0f);
    }
    private void OnHomingArrowReady() //Homing Arrow
    {

        ObjectPoolManager.Instance.CallObject("SkillMark", this.transform.position + Vector3.up * 0.1f, Quaternion.identity,true,1.5f);
    }
    private void OnHomingArrowFire()
    {

        ObjectPoolManager.Instance.CallObject("HomingArrow", FirePos,true , 7.0f);
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
        StartCoroutine(ArrowRain(false));
    }
    private void OnArrowRainFire()
    {
        StartCoroutine(ArrowRain(true));
    }
    IEnumerator ArrowRain(bool attack)
    {


        if (attack)
        {
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
            for (int j = 0; j < 2; j++)
            {

                for (int i = 0; i < points.Length; i++)
                {
                    yield return YieldInstructionCache.WaitForSeconds(0.1f);
                    ObjectPoolManager.Instance.CallObject("ExplosiveArrow", points[i].position, Quaternion.Euler(60, -90, -90));
                }
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                yield return YieldInstructionCache.WaitForSeconds(0.1f);
                ObjectPoolManager.Instance.CallObject("SkillMark", points[i].position+Vector3.up*0.1f, Quaternion.identity, true, 1.5f);
            }
        }
    }
}
