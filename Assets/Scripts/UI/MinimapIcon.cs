using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MinimapIcon : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Text;
    public RectTransform RectTransform;
    public RectTransform IconRectTransform;
    public void SetIcon(Sprite icon)
    {
        Image.sprite = icon;
    }
    public void SetColor(Color color)
    {
        Image.color = color;
    }
    public void SetText(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            Text.enabled = true;
            Text.text = text;
        }
    }
    public void SetTextSize(int size)
    {
        Text.fontSize = size;
    }
}
