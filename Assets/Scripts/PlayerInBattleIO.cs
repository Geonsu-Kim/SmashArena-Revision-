using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class PlayerInBattleIO : MonoBehaviour
{
    public static void SaveData()
    {
        FSMPlayer player = GameSceneManager.Instance.Player;

        XmlDocument Parent = new XmlDocument();
        XmlElement PlayerNode = Parent.CreateElement("PlayerInBattleDB");
        Parent.AppendChild(PlayerNode);

        XmlElement PlayerStatNode = Parent.CreateElement("Player");

        PlayerStatNode.SetAttribute("CurHp", player.health.CurHP.ToString());
        PlayerStatNode.SetAttribute("BlueGem", player.blueGem.ToString());
        PlayerStatNode.SetAttribute("RedGem", player.redGem.ToString());
        PlayerStatNode.SetAttribute("Critical", player.cri_Level.ToString());
        PlayerStatNode.SetAttribute("Defense", player.def_Level.ToString());
        for (int i = 0; i < player.skills.Count; i++)
        {

            PlayerStatNode.SetAttribute(player.skills[i].SkillId.ToString(), player.skills[i].Level.ToString());
        }

        PlayerNode.AppendChild(PlayerStatNode);
        Parent.Save(Application.dataPath + "/Data/PlayerInBattleData.xml");
    }
    public static void LoadData()
    {
        if (!System.IO.File.Exists(Application.dataPath + "/Data/PlayerInBattleData.xml")) return;

        FSMPlayer player = GameSceneManager.Instance.Player;
        XmlDocument Parent = new XmlDocument();
        Parent.Load(Application.dataPath + "/Data/PlayerInBattleData.xml");
        XmlElement PlayerNode = Parent["PlayerInBattleDB"];
        XmlNodeList playerStats = PlayerNode.ChildNodes;
        foreach(XmlElement stats in PlayerNode.ChildNodes)
        {
            player.health.CurHP = System.Convert.ToSingle(stats.GetAttribute("CurHp"));
            player.blueGem = System.Convert.ToInt32(stats.GetAttribute("BlueGem"));
            player.redGem=System.Convert.ToInt32(stats.GetAttribute("RedGem"));
            player.cri_Level = System.Convert.ToInt32(stats.GetAttribute("Critical"));
            player.def_Level=System.Convert.ToInt32(stats.GetAttribute("Defense"));
            for (int i = 0; i < SkillData.skillList.Count; i++)
            {
                player.skills.Add(new Skill(SkillData.skillList[i], System.Convert.ToInt32(stats.GetAttribute(SkillData.skillList[i].Name.ToString()))));
            }

        }
    }
}
