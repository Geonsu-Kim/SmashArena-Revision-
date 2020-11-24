using System.Collections;
using System;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public Transform Window;
    public bool Visible
    {
        get { return Window.gameObject.activeSelf; }
        set { Window.gameObject.SetActive(value); }
    }

    public virtual void Build(DialogData data)
    {

    }
    public void Show(Action callback)
    {
        Visible = true;
        if (callback != null)
        {
            callback();
        }
    }
    public void Close(Action callback)
    {
        Visible = false;
        if (callback != null)
        {
            callback();
        }
    }
}
