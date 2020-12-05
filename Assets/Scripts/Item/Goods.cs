using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GoodsType
{
    BlueGem,RedGem
}
public class Goods : Item
{
    public  GoodsType goods;
    public int minAmount;
    public int maxAmount;

    protected override void GetItem(FSMPlayer player)
    {
        int amount = Random.Range(minAmount, maxAmount);
        player.GetGoods(goods, amount);
    }
}
