using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName ="Dungeon",menuName = "ScriptableObject/Dungeon")]
public class Dungeon : ScriptableObject
{
    [SerializeField] private int needHeart;
    [SerializeField] private string dungeonName;
    [SerializeField] private string dungeonSceneName;
    public  Sprite thumbnail;

    public int NeedHeart { get { return needHeart; } }
    public string DungeonName { get { return dungeonName; } }
    public string DungeonSceneName { get { return dungeonSceneName; } }

}
