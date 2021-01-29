using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DroppedItem
{
    [SerializeField] private GameObject item;
    [SerializeField] private int min,max;
    public int Min { get { return min; } }
    public int Max { get { return max; } }
    public GameObject Item { get { return item; } }
}
