using UnityEngine;
using System;
public enum State 
{
    //Player
    Idle=0,         //0
    Run,            //1
    Attack,         //2
    Dash,           //3
    Roll,           //4
    Skill1,         //5 
    Skill2,         //6
    Skill3,         //7
    Skill4,         //8
    Dead,           //9
    Stun            //10
}
[Serializable]
public struct CharacterState
{
    public float AttackDamage;
    public float AttackRange;
    public float ChasingRange;
    public float WalkSpeed;
    public float RunSpeed;
    public float TurnSpeed;
}
