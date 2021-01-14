using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySkillIcon : MonoBehaviour
{
    private float time = 0f;
    private float moveSpeed;
    private float alphaSpeed;
    private Color originColor;
    private Color changedColor;
    private SpriteRenderer image;
    public Sprite[] Icons;
    public Transform originalPos;
    // Start is called before the first frame update
    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        moveSpeed = 1.0f;
        alphaSpeed = 1.0f;
        originColor = image.color;
    }
    private void OnEnable()
    {
        image.color = originColor;
        changedColor = originColor;
        time = 0f;
    }
    // Update is called once per frame
    private void Update()
    {
        time += Time.deltaTime * alphaSpeed*Time.timeScale;
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치
        changedColor.a = Mathf.Lerp(originColor.a, 0, time); // 텍스트 알파값
        image.color = changedColor;
        if (changedColor.a <= 0f) this.gameObject.SetActive(false);
    }
    public void SetIcon(int id)
    {
        transform.position = originalPos.position;
        transform.rotation = Quaternion.identity * Quaternion.Euler(45f, 0f, 0f);
        image.sprite = Icons[id];
        this.gameObject.SetActive(true);
    }
}
