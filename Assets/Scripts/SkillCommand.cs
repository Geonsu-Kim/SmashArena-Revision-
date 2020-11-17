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
            switch (m_num)
            {
                case 0: action.Attack(); break;
                case 1: action.Roll(); break;
                case 2: action.Skill1(); break;
                case 3: action.Skill2(); break;
                case 4: action.Skill3(); break;
            }
            return true;
        }
        else return false;
    }

}
