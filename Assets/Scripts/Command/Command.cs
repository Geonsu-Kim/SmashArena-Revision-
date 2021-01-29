using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Command
{
    protected FSMPlayer fsmPlayer=null;

    // Start is called before the first frame update
    public virtual bool Execute(GameObject obj)
    {
        return false;
    }
}
