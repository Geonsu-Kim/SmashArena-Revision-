using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private int level;
    private string name;
    private int skillID;
    private float mana;
    private float coefMin;
    private float coefMax;
    private float coolTime;

    public int SkillId { get { return skillID; } }
    public string  Name{ get { return name; } }
    public int Level { get { return level; } }
    public float Mana { get { return mana; } }
    public float CoolTime { get { return coolTime; } }
    public Skill(SkillInfo s, int _level = 1)
    {
        name = string.Copy(s.SkillName);
        skillID = s.SkillID;
        coolTime = s.CoolTime;
        mana = s.Mana;
        coefMin = s.CoefMin;
        coefMax = s.CoefMax;
        level = _level;
    }
    public Skill( Skill s,int _level=1)
    {
        name = string.Copy(s.name);
        skillID = s.skillID;
        coolTime =s.coolTime;
        mana = s.mana;
        coefMin = s.coefMin;
        coefMax = s.coefMax;
        level = _level;
    }
    public bool LevelUp() {
        if (level < 5)
        {
            level++;
            return true;
        }
        return false;
    
    
    }

    public float CalcDamage(float basicDamage)
    {
        return basicDamage * (0.1f * level + 1f-0.1f) * Random.Range(coefMin, coefMax);
    }
    
}
