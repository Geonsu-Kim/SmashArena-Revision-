using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DialogType
{
    Confirm, YesNo
}
public class DialogData
{
    private DialogType type;
    public DialogType Type { get { return type; } }

    public DialogData(DialogType _type)
    {
        this.type = _type;
    }
}
