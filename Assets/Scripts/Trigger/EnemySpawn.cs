using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject target;

    private void Start()
    {
        if (target != null)
        {
            ObjectPoolManager.Instance.CreateObject(target.name, 5);
        }
    }
    public void Spawn()
    {
        if (target != null)
        {
            ObjectPoolManager.Instance.CallObject(target.name, this.transform);
        }
    }
}
