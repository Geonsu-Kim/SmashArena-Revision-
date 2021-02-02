using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : Command
{
    private int m_num;
    
    public SkillCommand(PlayerAction action,int num):base(action)
    {
        m_num = num;
    }
    public override bool Execute()
    {
        if (action != null)
        {
            action.UseSkill(m_num);
            return true;
        }
        else return false;
    }

}
