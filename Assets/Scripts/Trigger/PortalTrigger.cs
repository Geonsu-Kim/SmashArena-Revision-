using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalTrigger : MonoBehaviour
{
    private FSMPlayer player;
    public Transform WarpPos;
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
}
