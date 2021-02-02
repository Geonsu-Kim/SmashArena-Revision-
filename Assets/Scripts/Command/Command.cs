using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Command
{
    protected PlayerAction action;

    public Command(PlayerAction _action)
    {
        action = _action;
    }
    public abstract bool Execute();
}
