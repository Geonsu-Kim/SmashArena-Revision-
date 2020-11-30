using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectTrigger : MonoBehaviour
{
    [SerializeField]
    protected int damageScalar = 0;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.CompareTag("PlayerAttack")&&other.gameObject.CompareTag("Enemy"))
            ||(this.gameObject.CompareTag("EnemyAttack")&& other.gameObject.CompareTag("Player")))
        {
            FSMBase fSM = other.gameObject.GetComponent<FSMBase>();
            float damage = Random.Range(0.85f, 1.15f) * damageScalar;
            fSM.Damaged((int)damage);
        }
    }
}
