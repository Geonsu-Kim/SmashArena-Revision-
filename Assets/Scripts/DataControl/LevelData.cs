using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public struct Stat
{
    public int level;
    public int Hp;
    public int Mp;
    public int needExp;
    public int Atk;
}
public sealed class LevelData : MonoBehaviour
{
    public static List<Stat> statList = new List<Stat>();
    public static void Init()
    {
        if (!System.IO.File.Exists(Application.dataPath + "/Data/LevelTable.xml")) return;

        XmlDocument Parent = new XmlDocument();
        Parent.Load(Application.dataPath + "/Data/LevelTable.xml");
        XmlElement node = Parent["LevelDB"];
        XmlNodeList levelInfo = node.ChildNodes;
        foreach(XmlElement stats in node.ChildNodes)
        {
            int _level = System.Convert.ToInt32(stats.GetAttribute("Level"));
            int _Hp= System.Convert.ToInt32(stats.GetAttribute("Hp"));
            int _Mp= System.Convert.ToInt32(stats.GetAttribute("Mp"));
            int _needExp = System.Convert.ToInt32(stats.GetAttribute("NeedExp"));
            int _Atk = System.Convert.ToInt32(stats.GetAttribute("Atk"));
            statList.Add(new Stat() { level = _level, Hp = _Hp, Mp = _Mp, needExp = _needExp,Atk=_Atk });
        }
    }
}
