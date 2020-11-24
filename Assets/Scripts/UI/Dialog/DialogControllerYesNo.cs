using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class DialogControllerYesNo : DialogController
{
    private DialogDataYesNo data;
    public TextMeshProUGUI TextMessage;
    private void Awake()
    {
        DialogManager.Instance.Regist(DialogType.YesNo, this);
        this.gameObject.SetActive(false);
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
