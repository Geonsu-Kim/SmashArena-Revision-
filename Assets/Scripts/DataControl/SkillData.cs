using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class SkillData 
{
    public static List<Skill> skillList=new List<Skill>();
    // Start is called before the first frame update
    public static void Init(SkillInfo[] baseSkill)
    {
        for (int i = 0; i < baseSkill.Length; i++)
        {
            skillList.Add(new Skill(baseSkill[i]));
        }
    }
}
