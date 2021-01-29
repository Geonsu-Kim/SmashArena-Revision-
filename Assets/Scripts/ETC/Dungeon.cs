using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Dungeon : MonoBehaviour
{
    private string dungeonSceneName;
    private bool unlocked;
    public TextMeshProUGUI DemandHeartText;
    public TextMeshProUGUI DungeonNameText;
    
    void OnEnable()
    {
    }
    public void SetDungeonInfo(int d,bool u,string n,string sn)
    {
        DemandHeartText.text = d.ToString();
        DungeonNameText.text = string.Copy(n);
        dungeonSceneName = string.Copy(sn);
        unlocked = u;
        this.gameObject.SetActive(u);
    }
    public void GotoDungeon()
    {
        LoadingSceneManager.LoadScene(dungeonSceneName);
    }

}
