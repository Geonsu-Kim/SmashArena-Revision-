using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class SkillData 
{
    public static List<Skill> skillList;
    // Start is called before the first frame update
    public static void Init()
    {
        skillList = new List<Skill>();
        if (!System.IO.File.Exists(Application.dataPath + "/Data/BaseSkill.xml")) return;

        XmlDocument Parent = new XmlDocument();
        Parent.Load(Application.dataPath + "/Data/BaseSkill.xml");
        XmlElement SkillNode = Parent["SkillDB"];
        XmlNodeList playerStats = SkillNode.ChildNodes;
        foreach (XmlElement stats in SkillNode.ChildNodes)
        {
            Skill s = new Skill(System.Convert.ToString(stats.GetAttribute("name")), System.Convert.ToInt32(stats.GetAttribute("skillID"))
                , System.Convert.ToSingle(stats.GetAttribute("coolTime"))
                , System.Convert.ToSingle(stats.GetAttribute("mana"))
                , System.Convert.ToSingle(stats.GetAttribute("coefMin"))
                , System.Convert.ToSingle(stats.GetAttribute("coefMax"))
                , 1);
            skillList.Add(s);
        }
    }
}
