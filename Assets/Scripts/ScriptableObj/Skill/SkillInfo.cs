using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Skill",menuName = "ScriptableObject/Skill")]

//스킬의 기본 정보
public class SkillInfo : ScriptableObject
{
    [SerializeField] private int skillID;
    [SerializeField] private float mana;
    [SerializeField] private float coefMin;
    [SerializeField] private float coefMax;
    [SerializeField] private float coolTime;
    [SerializeField] private string skillName;

    public int SkillID { get { return skillID; } }
    public float Mana { get { return mana; } }
    public float CoefMin { get { return coefMin; } }
    public float CoefMax { get { return coefMax; } }
    public float CoolTime { get { return coolTime; } }
    public string SkillName { get { return skillName; } }

}
