using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class DialogControllerYesNo : DialogController
{
    public TextMeshProUGUI TextMessage;
    DialogDataYesNo data;
    private void Awake()
    {
        Debug.Log("sdsd");


        DialogManager.Instance.Regist(DialogType.YesNo, this);
    }
    public override void Build(DialogData _data)
    {
        base.Build(data);
        if (!(_data is DialogDataYesNo)) return;
        data = _data as DialogDataYesNo;
        TextMessage.text = data.Message;
    }
    public void OnClickYes()
    {
        if (data.Callback != null) data.Callback(true);
        DialogManager.Instance.Pop();
    }
    public void OnClickNo()
    {
        if (data.Callback != null) data.Callback(false);
        DialogManager.Instance.Pop();
    }
}
