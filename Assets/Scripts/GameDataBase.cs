using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataBase : SingletonBase<GameDataBase>
{
    [HideInInspector] public int blueGem;
    [HideInInspector] public int redGem    ;
    [HideInInspector] public int skillUpCount = 0;
    [HideInInspector] public int statUpCount = 0;
    [HideInInspector] public float coef_BaseAtk = 1;
    [HideInInspector] public float coef_BaseDefense = 1;
    [HideInInspector] public float coef_CriticalAtk = 0;
    [HideInInspector] public float coef_Skill1 = 1;
    [HideInInspector] public float coef_Skill2 = 1;
    [HideInInspector] public float coef_Skill3 = 1;
    [HideInInspector] public float[] coef_SkillCoolDown;
    [HideInInspector] public float coef_SkillCoolDownAll = 1f;
    void Awake()
    {
        blueGem = 1000; 
        coef_SkillCoolDown = new float[4];
        for (int i = 0; i < coef_SkillCoolDown.Length; i++)
        {
            coef_SkillCoolDown[i] = 1f;
        }
    }

}
