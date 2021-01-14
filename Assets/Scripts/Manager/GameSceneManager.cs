using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : SingletonBase<GameSceneManager>
{
    // Start is called before the first frame update
    [SerializeField] private string sceneName;
    void Start()
    {
        Time.timeScale = 1;
        PlayerManager.Instance.Player.transform.position = PlayerManager.Instance.Player.StartPos;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        Debug.Log(SceneManager.GetActiveScene().name);
    }
}
