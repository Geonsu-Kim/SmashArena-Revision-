using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuffType
{
    AttackUp,DefenseUp,CriticalUp,CooltimeDown
}
public class BuffItem : Item
{
    public  BuffType buff;


    protected override void GetItem()
    {
        player.GetBuff(buff);
    }
}
