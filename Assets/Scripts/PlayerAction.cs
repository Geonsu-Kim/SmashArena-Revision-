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
    public void Attack()
    {
        if (GameSceneManager.Instance.OnBattle)
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
            if (player.IsInStageBtn&&player.stageTrigger!=null)
            {
                Time.timeScale = 0;
                DialogDataYesNo data = new DialogDataYesNo("Start the Game?", delegate (bool b) {
                    if (b) { player.stageTrigger.StartBattle(); }
                    Time.timeScale = 1;
                });
                DialogManager.Instance.Push(data);
            }
        }

    }
    public void Roll()
    {
        player.SetState(State.Roll);
    }
    public void Skill1()
    {
        player.SetState(State.Skill1);
    }
    public void Skill2()
    {
        player.SetState(State.Skill2);
    }

    public void Skill3()
    {
        ObjectPoolManager.Instance.CallObject("Registance"
            , this.transform.position + Vector3.up * 0.1f
            ,Quaternion.identity
            ,true,1.0f);
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
