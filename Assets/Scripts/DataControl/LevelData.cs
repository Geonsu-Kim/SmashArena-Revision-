using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public sealed class LevelData : MonoBehaviour
{
    public static List<Stat> statList = new List<Stat>();
    public static void Init(Stat[] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statList.Add(stats[i]);
        }
    }
}
