using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : SingletonBase<UIManager>
{
    private int statNum;
    private int blueGemDemand;
    private int redGemDemand;
    private FSMPlayer player;

    public GameObject EnemyInfo;
    public Image EnemyHpBar;
    public Text EnemyName;

    public Image PlayerHpBar;
    public Image PlayerMpBar;

    public GameObject MenuOnBattle;
    public GameObject MainMenu;
    public GameObject ResultWindow;
    public Text BlueGem;
    public Text RedGem;
    public Text BlueGemDemand;
    public Text RedGemDemand;
    public RawImage[] Signals;
    private void Start()
    {
        player = PlayerManager.Instance.Player;
    }
    public void OnSelectedStatButton(int num)
    {
        statNum = num;
        RenewDemandGem();
    }
    public void RenewDemandGem()
    {
        switch (statNum)
        {
            case 0:
                blueGemDemand = 300 + player.skills[0].Level * 30 - 30;
                redGemDemand = 0;
                break;
            case 1:
                blueGemDemand = 250 + player.def_Level * 25 - 25;
                redGemDemand = 0;
                break;
            case 2:
                blueGemDemand = 300 + player.cri_Level * 60 - 60;
                redGemDemand = 0;
                break;
            case 3:
                blueGemDemand = 400 + player.skills[2].Level * 20 - 20;
                redGemDemand = 0;
                break;
            case 4:
                blueGemDemand = 500 + player.skills[3].Level * 25 - 25;
                redGemDemand = 0;
                break;
            case 5:
                blueGemDemand = 200 + player.skills[4].Level * 30 - 30;
                redGemDemand = 0;
                break;
        }
        RenewText(ref BlueGemDemand, blueGemDemand.ToString());
        RenewText(ref RedGemDemand, redGemDemand.ToString());
    }
    public void OnClickLevelUpButton()
    {
        if (player.blueGem < blueGemDemand || player.redGem < redGemDemand) return;
        bool flag = false;
        switch (statNum)
        {
            case 0: flag = player.skills[0].LevelUp(); break;
            case 1: flag = player.LevelUpDef(); break;
            case 2: flag = player.LevelUpCri(); break;
            case 3: flag = player.skills[2].LevelUp(); break;
            case 4: flag = player.skills[3].LevelUp(); break;
            case 5: flag = player.skills[4].LevelUp(); break;
        }
        if (flag)
        {
            player.blueGem -= blueGemDemand;
            player.redGem -= redGemDemand;
            RenewText(ref BlueGem, player.blueGem.ToString());
            RenewText(ref RedGem, player.redGem.ToString());
            RenewDemandGem();
        }
    }
    public void OpenMenu()
    {
        Time.timeScale = 0;
        if (PlayerManager.Instance.OnBattle)
        {
            MenuOnBattle.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
            RenewText(ref BlueGem, player.blueGem.ToString());
            RenewText(ref RedGem, player.redGem.ToString());
        }
    }
    public void CloseMenu()
    {

        Time.timeScale = 1;
        MenuOnBattle.SetActive(false);
        MainMenu.SetActive(false);
    }
    public void SignalOn(int num)
    {
        Signals[num].enabled = true;
    }
    public void SignalOff(int num)
    {
        Signals[num].enabled = false;
    }
    public void RenewText(ref Text txt,string s)
    {
        txt.text = s;
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
