using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : Command
{
    private int m_num;
    private PlayerAction action;
    
    public SkillCommand(int num)
    {
        m_num = num;
    }
    public override bool Execute(GameObject obj)
    {
        action = obj.GetComponent<PlayerAction>();
        if (action != null)
        {
            action.UseSkill(m_num);
            return true;
        }
        else return false;
    }

}
