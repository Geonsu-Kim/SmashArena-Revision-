using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalTrigger : MonoBehaviour
{
    private FSMPlayer player;
    public Vector3 WarpPos;
    public string nextScene;
    public string _text;
    AsyncOperation op;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<FSMPlayer>();
            player.BtnNum = 2;
            player.portal = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<FSMPlayer>();
            player.BtnNum = 0;
            player.portal = null;
        }
    }
    public void MoveToNextScene()
    {
        player.StartPos = WarpPos;
        Time.timeScale = 0;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        op=SceneManager.LoadSceneAsync(nextScene,LoadSceneMode.Additive);
    }
}
