using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Start is called before the first frame update
    private float _height;
    private float _forward;
    private float _right;
    private float _damp;
    private bool shake = false;
    private Transform targetTr;
    private Vector3 originPos;


    public float originHeight = 6f;
    public float originForward = -1f;
    public float originRight = 0f;
    public float originDamp = 1f;

    void Start()
    {
        _height = originHeight ;
        _forward = originForward;
        _right = originRight;
        _damp = originDamp;
        targetTr = PlayerManager.Instance.Player.transform;
    }
    private void LateUpdate()
    {
        if (!shake)
        {
            transform.position = Vector3.Lerp(transform.position, targetTr.position + Vector3.up * _height + Vector3.forward * _forward + Vector3.right * _right,
                _damp);
            transform.LookAt(targetTr);
        }
    }
    public IEnumerator Shake(float _amount, float _duration)
    {

        originPos = transform.position;
        float timer = 0;
        shake = true;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originPos;
        shake = false;

    }
}