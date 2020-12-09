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

    public Image[] SkillImage;

    private void Update()
    {
        for (int i = 0; i < SkillImage.Length; i++)
        {
            if (GameSceneManager.Instance.Player.mana.CheckLeftMana(GameSceneManager.Instance.Player.skillMana[i]))
            {
                SkillImage[i].raycastTarget = true;
                SkillImage[i].color = Color.white;
            }
            else
            {
                SkillImage[i].raycastTarget = false;
                SkillImage[i].color = Color.white*0.25f;
            }
        }
    }
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
