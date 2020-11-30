using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchSkillButton : MonoBehaviour
{
    [SerializeField]
    private int skill_ID;
    [SerializeField]
    private float maxCoolTime;
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

    public  void SkillButtonDown()
    {
        if (skill_ID== 4)
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
        float realMaxCoolTime = maxCoolTime * player.coef_SkillCoolDown[skill_ID] * player.coef_SkillCoolDownAll;
        while (curCoolTime <realMaxCoolTime )
        {
            curCoolTime += Time.smoothDeltaTime;
            Icon.fillAmount = curCoolTime / realMaxCoolTime;
            yield return null;
        }
        coolDown = false;
        yield break;
    }

}
