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
    private Button button;
    private void Start()
    {
        player = GameSceneManager.Instance.Player;
        Icon = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SkillButtonDown);
    }
    private void Update()
    {
        if (GameSceneManager.Instance.Player.mana.CheckLeftMana(GameSceneManager.Instance.Player.skills[skill_ID].Mana))
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
            player.Action(skill_ID);
        }
        else {
            if (GameSceneManager.Instance.OnBattle)
            {
                if (!coolDown)
                {
                    coolDown = true;
                    StartCoroutine(SkillCoolDown());
                    player.Action(skill_ID);
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
