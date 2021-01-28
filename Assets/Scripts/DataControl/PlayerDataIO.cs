using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class PlayerDataIO : MonoBehaviour
{
    public static void SaveData()
    {
        FSMPlayer player = PlayerManager.Instance.Player;

        XmlDocument Parent = new XmlDocument();
        XmlElement PlayerNode = Parent.CreateElement("PlayerDB");
        Parent.AppendChild(PlayerNode);

        XmlElement PlayerStatNode = Parent.CreateElement("Player");

        PlayerStatNode.SetAttribute("Level", player.Level.ToString());
        PlayerStatNode.SetAttribute("Exp", player.Exp.ToString());

        PlayerNode.AppendChild(PlayerStatNode);
        Parent.Save(Application.dataPath + "/Data/PlayerData.xml");
    }
    public static void LoadData()
    {
        if (!System.IO.File.Exists(Application.dataPath + "/Data/PlayerData.xml")) return;

        FSMPlayer player = PlayerManager.Instance.Player;
        XmlDocument Parent = new XmlDocument();
        Parent.Load(Application.dataPath + "/Data/PlayerData.xml");
        XmlElement PlayerNode = Parent["PlayerDB"];
        XmlNodeList playerStats = PlayerNode.ChildNodes;
        foreach(XmlElement stats in PlayerNode.ChildNodes)
        {
            player.Level = System.Convert.ToInt32(stats.GetAttribute("Level"));
            player.Exp = System.Convert.ToInt32(stats.GetAttribute("Exp"));

        }
    }
}
