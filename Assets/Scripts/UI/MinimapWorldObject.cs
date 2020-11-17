using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapWorldObject : MonoBehaviour
{
    public Sprite Icon;
    public Color IconColor = Color.white;
    public string Text;
    public int textSize;
    private void Start()
    {
        Minimap.Instance.RegisterMiniMapWorldObject(this);
    }
}
