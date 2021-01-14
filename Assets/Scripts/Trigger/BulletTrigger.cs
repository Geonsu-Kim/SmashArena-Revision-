using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class BulletTrigger : EffectTrigger
{
    [SerializeField] private bool canFierceObj=false;
    private Rigidbody rb;
    public float hitEffectTime;
    public Vector3 forceDir;
    public GameObject hit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(this.transform.right*forceDir.x+ this.transform.up * forceDir.y+ this.transform.forward * forceDir.z);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if ((this.gameObject.CompareTag("PlayerAttack") && other.gameObject.CompareTag("Enemy"))
        || (this.gameObject.CompareTag("EnemyAttack") && other.gameObject.CompareTag("Player")))
        {
            FSMBase fSM = other.gameObject.GetComponent<FSMBase>();
            fSM.Damaged(damageScalar);
        }
        else if (other.gameObject.CompareTag("StaticObj")&& !canFierceObj)
        {
            this.gameObject.SetActive(false);

            if (hit != null)
            {
                ObjectPoolManager.Instance.CallObject(hit.name, this.transform.position+Vector3.up*0.25f,Quaternion.identity, true, hitEffectTime);

            }
        }
    }
}
