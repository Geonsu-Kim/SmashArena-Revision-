﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class Health
{

    private float curHP=1f;

    private float maxHP=1f;

    public float CurHP { set { curHP = value; } get { return curHP; } }
    public float MaxHP { set { maxHP = value; } get { return maxHP; } }
    public void Damaged(float amount)
    {
        curHP -= amount;
        if (IsDead()) curHP = 0.0f;
    }
    public void Recovered(float amount)
    {
        curHP += amount;
        if (IsFilled()) curHP = maxHP;
    }
    public void ChangeMaxHP(float coef)
    {
        maxHP *= coef;
    }
    public void Revive()
    {
        curHP = MaxHP;
    }
    public float Ratio() { return curHP / maxHP; }
    public bool IsDead() { return curHP <= 0.0f; }
    public bool IsFilled() { return curHP >= maxHP; }
}
