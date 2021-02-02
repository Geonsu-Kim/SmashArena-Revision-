 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class PlayerAction : MonoBehaviour
{

    private StringBuilder stringBuilder;
    private const string playerName = "Player";
    private string SFXname;
    private FSMPlayer player;
    private void Start()
    {
        player = GetComponent<FSMPlayer>();
        stringBuilder = new StringBuilder(64);
    }
    public void UseSkill(int num)
    {
        switch (num)
        {
            case 0: Attack(); break;
            case 1: Roll(); break;
            case 2: Slash(); break;
            case 3: Crash(); break;
            case 4: Light(); break;

        }
    }
    public void Attack()
    {
        if (PlayerManager.Instance.OnBattle)
        {
            player.ComboOnOff = true;
            player.SetState(State.Attack);
        }
        else
        {
            switch (player.BtnNum)
            {
                case 0:
                    break;
                case 1:
                    if (player.stageTrigger != null)
                    {
                        Time.timeScale = 0;
                        DialogManager.Instance.Push(new DialogDataYesNo("Start the Game?", delegate (bool b)
                        {
                            if (b) 
                            {
                                player.stageTrigger.StartBattle();
                                player.stageTrigger = null;
                            }
                            Time.timeScale = 1;
                        }));
                    }
                    break;
                case 2:
                    if (player.portal != null)
                    {
                        Time.timeScale = 0;
                        DialogManager.Instance.Push(new DialogDataYesNo("Want to Move to Boss Room?", delegate (bool b)
                        {
                            if (b)
                            {
                                player.Warp(player.portal.WarpPos.position);
                                player.portal = null;
                            }
                            Time.timeScale = 1;
                        }));
                    }
                    break;
            }
            
        }

    }
    public void Roll()
    {
        player.SetState(State.Roll);
    }
    public void Slash()
    {
        player.SetState(State.Skill1);
        player.ConsumeMana(player.skills[2].Mana);

    }
    public void Crash()
    {
        player.SetState(State.Skill2);
        player.ConsumeMana(player.skills[3].Mana);
    }
  
    public void Light()
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(playerName);
        stringBuilder.Append("Light");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        if (player.skills[4].Level < 4)
        {
            ObjectPoolManager.Instance.CallObject("Light"
                , this.transform.position + Vector3.up * 0.1f
                , Quaternion.identity
                , true, 1.0f);
            Collider[] colls = Physics.OverlapSphere(this.transform.position, 3f, 1 << 8);
            for (int i = 0; i < colls.Length; i++)
            {
                FSMEnemy fSM = colls[i].gameObject.GetComponent<FSMEnemy>();
                fSM.SetStateTrigger(State.Stun);
                ObjectPoolManager.Instance.CallObject("Hit",
              colls[i].gameObject.transform.position + Vector3.up * 1.0f,
              Quaternion.identity, true, 0.5f);
            }
        }
        else
        {

        }

        player.ConsumeMana(player.skills[4].Mana);
    }
    public void Move()
    {
        if (player.GetDir())
        {
            this.transform.rotation = Quaternion.LookRotation(player.Dir);
            player.m_cc.Move(player.Dir * Time.deltaTime * 2f);
            player.SetState(State.Run);
        }
    }
    // Update is called once per frame
}
