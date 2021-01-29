using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : SingletonBase<GameSceneManager>
{
    public Transform startPos;
    public int BlueGemAmountInit;
    [HideInInspector] public string dungeonName="";
    [HideInInspector] public float playTime=0;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;

        dungeonName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync("scPlayer", LoadSceneMode.Additive);
    }
}
