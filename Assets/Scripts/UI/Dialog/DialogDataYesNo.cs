using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogDataYesNo : DialogData
{
    private string message;
    private Action<bool> callback;
    public string Message
    {
        get { return message; }
    }
    public Action<bool> Callback
    {
        get { return callback; }
    }
    public DialogDataYesNo(string _message, Action<bool> _callback = null) : base(DialogType.YesNo)
    {
        message = _message;
        callback = _callback;
    }
}
