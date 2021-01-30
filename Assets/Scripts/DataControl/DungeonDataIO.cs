using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class DungeonDataIO : MonoBehaviour
{
    public static List<Dungeon> dungeonList = new List<Dungeon>();
    public static void Init(Dungeon[] dungeons)
    {
        for (int i = 0; i < dungeons.Length; i++)
        {
            dungeonList.Add(dungeons[i]);
        }
    }
}
