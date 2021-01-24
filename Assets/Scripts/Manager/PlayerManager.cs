using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerManager : SingletonBase<PlayerManager>
{
    private bool onBattle=false;

    private FSMPlayer player;
    private FollowCam cam;
    private GameObject canvas;
    public int score;
    AsyncOperation op;
    public bool OnBattle { get { return onBattle; } set { onBattle = value; } }
    public FollowCam Cam { set { value = cam; } get { return cam; } }
    public FSMPlayer Player { set { value = player; } get { return player; } }
    public GameObject Canvas { set { value = canvas; } get { return canvas; } }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FSMPlayer>();
        cam= GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCam>();
        canvas = GameObject.Find("Canvas");
        PlayerDataIO.LoadData();
    }
    public void GotoLobby()
    {
        PlayerDataIO.SaveData();
        LoadingSceneManager.LoadScene("scLobby");
    }
    public void Retry()
    {
        PlayerDataIO.SaveData();
        LoadingSceneManager.LoadScene(GameSceneManager.Instance.dungeonName);
    }
}
