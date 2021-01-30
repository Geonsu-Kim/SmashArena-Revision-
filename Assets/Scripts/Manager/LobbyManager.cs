using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonBase<LobbyManager>
{
    // Start is called before the first frame update
    public GameObject DungeonListView;
    public DungeonUI[] dungeons;
    private void Awake()
    {
        dungeons = DungeonListView.GetComponentsInChildren<DungeonUI>();

        for (int i = 0; i < dungeons.Length; i++)
        {
            dungeons[i].SetDungeonInfo(DungeonDataIO.dungeonList[i]);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
