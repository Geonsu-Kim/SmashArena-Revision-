using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
public class ResultControl : MonoBehaviour
{
    public TextMeshProUGUI resultTxt;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI scoreText;
    StringBuilder sb=new StringBuilder(64);

    // Start is called before the first frame update
    void OnEnable()
    {
        int m = (int)(GameSceneManager.Instance.playTime / 60);
        int s= (int)(GameSceneManager.Instance.playTime % 60);

        sb.Length = 0;
        sb.Append(m.ToString());
        sb.Append(" : ");
        sb.Append(s.ToString());
        timeText.text = sb.ToString();
        expText.text = PlayerManager.Instance.gainedExpInBattle.ToString();
        scoreText.text = PlayerManager.Instance.score.ToString();
        PlayerManager.Instance.GivePlayerExp();
    }
    public void SetActiveTrue(string txt)
    {
        Time.timeScale = 0;

        resultTxt.text = txt;
        gameObject.SetActive(true);

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
