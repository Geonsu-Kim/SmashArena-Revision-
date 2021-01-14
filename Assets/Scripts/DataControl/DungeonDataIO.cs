using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class DungeonDataIO : MonoBehaviour
{/*
    public static void SaveData()
    {

        XmlDocument Parent = new XmlDocument();
        XmlElement DungeonNode = Parent.CreateElement("DungeonDB");
        Parent.AppendChild(DungeonNode);

        XmlElement DungeonInfoNode = Parent.CreateElement("Dungeon");

        DungeonInfoNode.SetAttribute("CurHp", player.health.CurHP.ToString());
        DungeonInfoNode.SetAttribute("BlueGem", player.blueGem.ToString());
        DungeonInfoNode.SetAttribute("RedGem", player.redGem.ToString());
        DungeonInfoNode.SetAttribute("Critical", player.cri_Level.ToString());
        DungeonInfoNode.SetAttribute("Defense", player.def_Level.ToString());
        for (int i = 0; i < player.skills.Count; i++)
        {
            DungeonInfoNode.SetAttribute(player.skills[i].SkillId.ToString(), player.skills[i].Level.ToString());
        }

        PlayerNode.AppendChild(PlayerStatNode);
        Parent.Save(Application.dataPath + "/Data/PlayerInBattleData.xml");
    }*/
    public static void LoadData()
    {
        if (!System.IO.File.Exists(Application.dataPath + "/Data/DungeonData.xml")) return;

        int idx = 0;
        Dungeon[] dungeons = LobbyManager.Instance.dungeons;
        XmlDocument Parent = new XmlDocument();
        Parent.Load(Application.dataPath + "/Data/DungeonData.xml");
        XmlElement DungeonNode = Parent["DungeonDB"];
        XmlNodeList dungeonInfo = DungeonNode.ChildNodes;
        foreach(XmlElement stats in DungeonNode.ChildNodes)
        {
            
            int d = System.Convert.ToInt32(stats.GetAttribute("DemandHeartAmount"));
            bool u= System.Convert.ToBoolean(stats.GetAttribute("unlocked"));
            string n= System.Convert.ToString(stats.GetAttribute("DungeonName"));
            string sn = System.Convert.ToString(stats.GetAttribute("DungeonSceneName"));
            dungeons[idx++].SetDungeonInfo(d, u, n, sn);
        }
    }
}
