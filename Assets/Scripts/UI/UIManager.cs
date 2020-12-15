using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager :SingletonBase<UIManager>
{
    public GameObject EnemyInfo;
    public Image EnemyHpBar;
    public Text EnemyName;

    public Image PlayerHpBar;
    public Image PlayerMpBar;


    public void RenewPlayerUI(ref Image image,float ratio)
    {
        image.fillAmount = ratio;
    }
    public void RenewEnemyUI(ref Color color,ref string name,float ratio)
    {
        if (!EnemyInfo.activeSelf) EnemyInfo.SetActive(true);
        EnemyName.text = name;
        EnemyHpBar.fillAmount = ratio;
    }
}
