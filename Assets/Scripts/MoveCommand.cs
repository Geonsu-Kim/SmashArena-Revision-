using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public override bool Execute(GameObject obj)
    {
        fsmPlayer = obj.GetComponent<FSMPlayer>();
        if (fsmPlayer != null)
        {
            if (fsmPlayer.CheckAction())
            {
                obj.transform.rotation = Quaternion.LookRotation(fsmPlayer.Dir);
                return true;
            }
            obj.transform.rotation = Quaternion.LookRotation(fsmPlayer.Dir);
            fsmPlayer.m_cc.Move(fsmPlayer.Dir * Time.deltaTime * 2f);
            fsmPlayer.SetState(State.Run);
            return true;
        }
        return false;
    }
}
