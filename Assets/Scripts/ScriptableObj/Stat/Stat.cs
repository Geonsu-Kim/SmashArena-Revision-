using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Stat",menuName = "ScriptableObject/Stat")]
public class Stat : ScriptableObject
{
    [SerializeField] private int level;
    [SerializeField] private int hp;
    [SerializeField] private int mp;
    [SerializeField] private int needExp;
    [SerializeField] private int atk;

    public int Level { get { return level; } }
    public int Hp { get { return hp; } }
    public int Mp { get { return mp; } }
    public int NeedExp{ get { return needExp; } }
    public int Atk { get { return atk; } }
}
