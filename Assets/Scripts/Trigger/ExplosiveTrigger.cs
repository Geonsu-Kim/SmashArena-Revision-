using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrigger : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 Dir;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Dir);
    }
    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }
}
