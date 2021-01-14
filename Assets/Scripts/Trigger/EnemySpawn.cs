using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject target;
    public void Spawn()
    {
        if (target != null)
        {
            ObjectPoolManager.Instance.CallObject(target.name, this.transform);
        }
    }
}
