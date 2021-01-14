using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool :MonoBehaviour
{
    public int[] effectCount;
    public GameObject[] effect;
    private void Start()
    {
        if (   effectCount == null 
            || effect == null
            || effect.Length!= effectCount.Length) return;
        for (int i = 0; i < effectCount.Length; i++)
        {
            ObjectPoolManager.Instance.CreateObject(effect[i], effect[i].name, effectCount[i], this.transform);
        }

    }
}
