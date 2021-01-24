using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class DungeonDataIO : MonoBehaviour
{
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
