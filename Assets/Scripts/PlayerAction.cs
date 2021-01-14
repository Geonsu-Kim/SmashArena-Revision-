 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    private FSMPlayer player;
    public GameObject[] skillEffect;

    public Transform[] skillPos;
    public GameObject Lightning;
    public GameObject Registance;
    private void Start()
    {
        player = GetComponent<FSMPlayer>();
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
            if (player.IsStanding())
            {
                player.SetState(State.Attack);
            }
            else if (player.IsRunning())
            {
                player.SetState(State.Dash);
            }
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
                            if (b) { player.stageTrigger.StartBattle(); }
                            Time.timeScale = 1;
                        }));
                    }
                    break;
                case 2:
                    Time.timeScale = 0;
                    DialogManager.Instance.Push(new DialogDataYesNo("Want to Move to Boss Room?", delegate (bool b) {
                        if (b) {
                            player.portal.MoveToNextScene();
                        }
                        Time.timeScale = 1;
                    }));
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
    private bool CheckAction()
    {
        if (player.IsDashing()) return false;
        if (player.IsDead()) return false;
        if (player.IsRolling()) return false;
        if (player.IsUsingSkill1()) return false;
        if (player.IsUsingSkill2()) return false;
        return true;
    }
    // Update is called once per frame
}
