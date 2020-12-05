using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PotionType
{
    HP, MP
}
public class Potion : Item
{
    public PotionType potion;
    public int level;

    protected override void GetItem(FSMPlayer player)
    {
        player.GetPotion(potion, level);
    }
}
