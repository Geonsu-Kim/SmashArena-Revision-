using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchSkillButton : MonoBehaviour
{
    [SerializeField]
    private int skill_ID;
    [SerializeField]
    private float curCoolTime=0;
    private bool coolDown=false;
    private Image Icon;
    private FSMPlayer player;
    private PlayerAction action;
    private Button button;


    private Command skillCommands;

    private void Start()
    {
        player = PlayerManager.Instance.Player;
        action = player.GetComponent<PlayerAction>();
        skillCommands = new SkillCommand(action, skill_ID);
        player.SetCommand(skill_ID, skillCommands);
        Icon = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SkillButtonDown);
    }
    private void Update()
    {

        if (player.isDead() || player.IsEnd()) return;
            if (player.mana.CheckLeftMana(player.skills[skill_ID].Mana))
            {
                Icon.raycastTarget = true;
                Icon.color = Color.white;
            }
            else
            {
                Icon.raycastTarget = false;
                Icon.color = Color.white * 0.25f;
            }
        
    }
    public  void SkillButtonDown()
    {
        if (skill_ID== 0)
        {
            player.ExecuteCommand(skill_ID);
        }
        else {
            if (PlayerManager.Instance.OnBattle)
            {
                if (!coolDown&&!player.IsUsingSkill())
                {
                    coolDown = true;
                    StartCoroutine(SkillCoolDown());
                    player.ExecuteCommand(skill_ID);
                }
            }
        }

    }
    private IEnumerator SkillCoolDown()
    {
        curCoolTime = 0;
        Icon.fillAmount = 0;
        float realMaxCoolTime = player.skills[skill_ID].CoolTime * (1-player.CheckBuff(player.BuffCoolDown));
        while (curCoolTime <realMaxCoolTime )
        {
            curCoolTime += Time.smoothDeltaTime*Time.timeScale;
            Icon.fillAmount = curCoolTime / realMaxCoolTime;
            yield return null;
        }
        coolDown = false;
        yield break;
    }

}
