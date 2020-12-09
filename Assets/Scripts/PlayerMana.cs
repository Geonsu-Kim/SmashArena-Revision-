using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class PlayerMana
{
    [SerializeField] private float curMP;

    [SerializeField] private float maxMP;

    public float CurMP { set { curMP = value; } get { return curMP; } }
    public float MaxMP { get { return maxMP; } }
    public void Consume(float amount)
    {
        curMP -= amount;
        if (IsEmpty()) curMP = 0.0f;
    }
    public void Recovered(float amount)
    {
        curMP += amount;
        if (IsFilled()) curMP = maxMP;
    }

    public void Revive()
    {
        curMP = maxMP;
    }
    public bool CheckLeftMana(float amount){return curMP < amount?false:true;}
    public float Ratio() { return curMP / maxMP; }
    public bool IsEmpty() { return curMP <= 0.0f; }
    public bool IsFilled() { return curMP >= maxMP; }

}
