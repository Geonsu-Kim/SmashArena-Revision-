using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSet : MonoBehaviour
{
    public GameObject Menu;
    public void OnClickMenu()
    {
        Time.timeScale = 0;
        Menu.SetActive(true);
    }
    public void OnClickContinue()
    {

        Time.timeScale = 1;

        Menu.SetActive(false);
    }
    public void OnClickSave()
    {

    }
    public void OnClickLoad()
    {

    }
    public void OnClickExit()
    {

    }
}
