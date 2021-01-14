using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int[] objCount;
    public GameObject[] obj;
    private void Start()
    {
        if (obj == null
            || objCount == null
            || obj.Length != objCount.Length) return;
        for (int i = 0; i < objCount.Length; i++)
        {
            ObjectPoolManager.Instance.CreateObject(obj[i].name, objCount[i],this.transform);
        }

    }
}
