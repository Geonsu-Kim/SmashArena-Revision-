using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class Health
{

    [SerializeField]  private float curHP;

    [SerializeField]  private float maxHP;

    public float MaxHP { get { return maxHP; } }
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
    public void Revive()
    {
        curHP = MaxHP;
    }
    public float Ratio() { return curHP / maxHP; }
    public bool IsDead() { return curHP <= 0.0f; }
    public bool IsFilled() { return curHP >= maxHP; }
}
