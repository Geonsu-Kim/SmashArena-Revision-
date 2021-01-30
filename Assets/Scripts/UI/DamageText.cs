using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    TextMeshPro text;
    Color changedColor;
    private float time = 0f;
    private float moveSpeed;
    private float alphaSpeed;


    public Color originColor;
    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        moveSpeed = 2.0f;
        alphaSpeed = 1.0f;
        originColor = text.color;
    }
    private void OnEnable()
    {
        text.color = originColor;
        changedColor = originColor;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * alphaSpeed;
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치
        changedColor.a = Mathf.Lerp(originColor.a, 0, time); // 텍스트 알파값
        text.color = changedColor;
        if (changedColor.a <= 0f) this.gameObject.SetActive(false);
    }
}
