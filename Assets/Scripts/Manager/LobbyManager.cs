using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonBase<LobbyManager>
{
    // Start is called before the first frame update
    public GameObject DungeonListView;
    public Dungeon[] dungeons;
    private void Awake()
    {
        dungeons = DungeonListView.GetComponentsInChildren<Dungeon>();
        DungeonDataIO.LoadData();
    }

}
