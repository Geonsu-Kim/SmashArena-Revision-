using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class PlayerDataIO : MonoBehaviour
{
    public static void SaveData()
    {
        FSMPlayer player = PlayerManager.Instance.Player;
        PlayerPrefs.SetInt("Level", player.Level);
        PlayerPrefs.SetInt("Exp", player.Exp);
       
    }
    public static void LoadData()
    {
        FSMPlayer player = PlayerManager.Instance.Player;
        if (PlayerPrefs.HasKey("Level"))
        {
            player.Level = PlayerPrefs.GetInt("Level");
            player.Exp = PlayerPrefs.GetInt("Exp");
        }
        else
        {
            player.Level = 1;
            player.Exp = 0;
        }

    }
}
