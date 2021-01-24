using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResultControl : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI redGemText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void OnEnable()
    {
        int m = (int)(GameSceneManager.Instance.playTime / 60);
        int s= (int)(GameSceneManager.Instance.playTime % 60);
        timeText.text = m.ToString() + " : " + s.ToString();
        redGemText.text = PlayerManager.Instance.Player.redGem.ToString();
        expText.text = PlayerManager.Instance.Player.Exp.ToString();
        scoreText.text = PlayerManager.Instance.score.ToString();

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
