using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : SingletonBase<GameSceneManager>
{
    private bool onBattle=false;

    private FSMPlayer player;
    private FollowCam cam;
    private GameObject canvas;

    public bool OnBattle { get { return onBattle; } set { onBattle = value; } }
    public FollowCam Cam { set { value = cam; } get { return cam; } }
    public FSMPlayer Player { set { value = player; } get { return player; } }
    public GameObject Canvas { set { value = canvas; } get { return canvas; } }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FSMPlayer>();
        cam= GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCam>();
        canvas = GameObject.Find("Canvas");
    }
    private void Start()
    {

        ObjectPoolManager.Instance.CreateObject("HitWhite");

        ObjectPoolManager.Instance.CreateObject("HitYellow");

        ObjectPoolManager.Instance.CreateObject("HitRed");


    }
}
