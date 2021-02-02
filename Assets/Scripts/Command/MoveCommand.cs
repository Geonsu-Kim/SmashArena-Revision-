using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public MoveCommand(PlayerAction action) : base(action) { }
    public override bool Execute()
    {
        if (action != null)
        {
            action.Move();
            return true;
        }
        return false;
    }
}
