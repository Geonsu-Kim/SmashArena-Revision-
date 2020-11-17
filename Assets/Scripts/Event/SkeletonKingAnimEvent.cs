using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKingAnimEvent : EnemyAnimationEvent
{
    private void Skeleton_KingAttackStart(AnimationEvent animationEvent)
    {
        EnabledCollider(animationEvent.intParameter);
    }

    private void Skeleton_KingAttackEnd(AnimationEvent animationEvent)
    {
        DisabledCollider(animationEvent.intParameter);
    }
    private void WaveAttack()
    {
        ObjectPoolManager.Instance.CallObject("WaveAttack", this.transform.position + Vector3.up * 0.1f, Quaternion.identity, true, 1.5f);

    }
    private void DarkExplosion()
    {

        StartCoroutine(DarkExplosionCoroutine());
    }
    private void KingBuff()
    {
        ObjectPoolManager.Instance.CallObject("InfantryBuff", this.transform.position + Vector3.up * 0.2f, Quaternion.identity, true, 2.0f);
        StartCoroutine(Enemy.Buff());
    }
    private void DarkThunderFallReady()
    {

        StartCoroutine(DarkThunderFallCoroutine(false));
    }

    private void DarkThunderFall ()
    {
        StartCoroutine(DarkThunderFallCoroutine(true));
    }
    IEnumerator DarkExplosionCoroutine()
    {
        Vector3 pos = GameSceneManager.Instance.Player.transform.position + Vector3.up * 0.1f;
        ObjectPoolManager.Instance.CallObject("SkillMark", pos, Quaternion.identity, true, 1.5f);

        yield return YieldInstructionCache.WaitForSeconds(2.0f);

        ObjectPoolManager.Instance.CallObject("DarkExplosion", pos, Quaternion.identity, true, 1.5f);
    }
    IEnumerator DarkThunderFallCoroutine(bool attack)
    {
        if (attack)
        {
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
            for (int j = 0; j < 2; j++)
            {

                for (int i = 0; i < points.Length; i++)
                {
                    yield return YieldInstructionCache.WaitForSeconds(0.1f);
                    ObjectPoolManager.Instance.CallObject("DarkThunderFall", points[i].position, Quaternion.Euler(60, -90, -90));
                }
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                yield return YieldInstructionCache.WaitForSeconds(0.1f);
                ObjectPoolManager.Instance.CallObject("SkillMark", points[i].position + Vector3.up * 0.1f, Quaternion.identity, true, 1.5f);
            }
        }
    }
}
