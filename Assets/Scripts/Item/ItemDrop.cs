using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int max;
    [SerializeField] private DroppedItem[] item;
    public int Max { get { return max; } }
    public void DropItem(int p)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if(p>=item[i].Min&& p <= item[i].Max)
            {
                ObjectPoolManager.Instance.CallObject(item[i].Item.name, this.transform.position + Vector3.up * 0.5f,Quaternion.identity);
                return;
            }
        }
    }
}
