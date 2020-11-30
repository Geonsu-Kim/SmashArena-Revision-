using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType{
    AttackUp,DefenseUp,CriticalUp,CooltimeDown,BlueGem,RedGem,HP,MP
}
public class Item
{
    private readonly ItemType type;
    public Item(ItemType _type)
    {
        this.type = _type;
    }
    public void GetItem(FSMPlayer player)
    {
        player.GetItem(type);
    }

}
