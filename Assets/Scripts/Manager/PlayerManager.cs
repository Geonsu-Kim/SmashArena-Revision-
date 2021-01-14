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
    AsyncOperation op;
    public bool OnBattle { get { return onBattle; } set { onBattle = value; } }
    public FollowCam Cam { set { value = cam; } get { return cam; } }
    public FSMPlayer Player { set { value = player; } get { return player; } }
    public GameObject Canvas { set { value = canvas; } get { return canvas; } }
    private void Awake()
    {
        //op =SceneManager.LoadSceneAsync("scDungeon", LoadSceneMode.Additive);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FSMPlayer>();
        cam= GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCam>();
        canvas = GameObject.Find("Canvas");
        SkillData.Init();
        player.transform.position = new Vector3(61.46f, 1.5f, 0.18f);
        //StartCoroutine(s());
    }
    IEnumerator s()
    {
        while (!op.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("scDungeon"));
    }
}
