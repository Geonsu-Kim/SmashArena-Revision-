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
        PlayerStatNode.SetAttribute("BlueGem", GameDataBase.Instance.blueGem.ToString());


        PlayerNode.AppendChild(PlayerStatNode);
        Parent.Save(Application.dataPath + "/Resources/PlayerInBattleData.xml");
    }
    public static void LoadData()
    {
        if (!System.IO.File.Exists(Application.dataPath + "/Resources/PlayerInBattleData.xml")) return;

        FSMPlayer player = GameSceneManager.Instance.Player;
        XmlDocument Parent = new XmlDocument();
        Parent.Load(Application.dataPath + "/Resources/PlayerInBattleData.xml");
        XmlElement PlayerNode = Parent["PlayerInBattleDB"];
        XmlNodeList playerStats = PlayerNode.ChildNodes;
        foreach(XmlElement stats in PlayerNode.ChildNodes)
        {
            player.health.CurHP = System.Convert.ToSingle(stats.GetAttribute("CurHp"));
            GameDataBase.Instance.blueGem = System.Convert.ToInt32(stats.GetAttribute("BlueGem"));
        }
    }
}
