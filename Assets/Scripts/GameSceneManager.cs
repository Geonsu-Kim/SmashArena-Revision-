using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : SingletonBase<GameSceneManager>
{
    public FSMPlayer player;
    public FollowCam cam;
    public GameObject canvas;
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
        DialogDataYesNo data = new DialogDataYesNo("Start the Game?", delegate (bool b)
        {
            if (b)
            {
                Debug.Log("Yes");

            }
            else
            {

                Debug.Log("No");
            }
        });
        DialogManager.Instance.Push(data);

    }
    public FollowCam Cam { set { value = cam; } get { return cam; } }
    public FSMPlayer Player { set { value = player; } get { return player; } }
    public GameObject Canvas { set { value = canvas; } get { return canvas; } }
}
