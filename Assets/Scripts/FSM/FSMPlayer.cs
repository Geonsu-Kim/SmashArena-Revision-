﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAction))]
public class FSMPlayer : FSMBase
{
    private bool comboOnOff = false;
    private bool canRun = true;
    private bool isInStageBtn = false;

    private bool buffAttack = false;
    private bool buffDefense = false;
    private bool buffCritical = false;
    private bool buffCoolDown = false;

    private float buffTime_Attack = 0;
    private float buffTime_Defense = 0;
    private float buffTime_Critical = 0;
    private float buffTime_CoolDown = 0;


    private Command moveCommand;
    private Command[] skillCommand = new SkillCommand[5];
    [HideInInspector] public float coef_BaseAtk = 1;
    [HideInInspector] public float coef_BaseDefense = 1;
    [HideInInspector] public float coef_MaxHP = 1;
    [HideInInspector] public float coef_Skill1 = 1;
    [HideInInspector] public float coef_Skill2 = 1;
    [HideInInspector] public float coef_Skill3 = 1;
    [HideInInspector] public float coef_CriticalAtk = 0;
    [HideInInspector] public float[] coef_SkillCoolDown;
    [HideInInspector] public float coef_SkillCoolDownAll = 1f;

    public StageTrigger stageTrigger;
    public Vector3 Dir = new Vector3(0, 0, 0);
    public CharacterController m_cc;
    public bool CanRun { get { return canRun; } }
    public bool ComboOnOff { get { return comboOnOff; } set { comboOnOff = value; } }
    public bool IsInStageBtn { get { return isInStageBtn; } set { isInStageBtn = value; } }


    protected override void Awake()
    {
        base.Awake();
        m_cc = GetComponent<CharacterController>();
        moveCommand = new MoveCommand();
        for (int i = 0; i < skillCommand.Length; i++)
        {
            skillCommand[i] = new SkillCommand(i);
        }
        coef_SkillCoolDown = new float[4];
        for (int i = 0; i < coef_SkillCoolDown.Length; i++)
        {
            coef_SkillCoolDown[i] = 1f;
        }
    }
    private void Start()
    {
        ObjectPoolManager.Instance.CreateObject("Crasher", 1);
        ObjectPoolManager.Instance.CreateObject("OverpoweredSlash");
        ObjectPoolManager.Instance.CreateObject("Registance", 1);
    }
    private void Update()
    {
        if (canRun && GetDir())
            moveCommand.Execute(this.gameObject);
    }

    protected override IEnumerator Idle()
    {
        do
        {
            yield return null;
        } while (!isNewState);
    }
    public override void Damaged(int amount, bool critical = false)
    {
        if (isDead) return;

        health.Damaged(amount * coef_BaseDefense);
        PlayerHpbar.Instance.RenewGauge(health.Ratio());
        StartCoroutine(ColorByHit());
        if (health.IsDead())
        {
            SetStateTrigger(State.Dead);
        }
    }
    public void Recovered(int amount)
    {
        if (isDead) return;

        health.Recovered(amount);
        PlayerHpbar.Instance.RenewGauge(health.Ratio());

    }
    public void GetItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.AttackUp:
                if (buffAttack) { buffTime_Attack = 0.0f; }
                else { StartCoroutine(AttackBuff(0.25f)); }
                break;
            case ItemType.DefenseUp:
                if (buffDefense) { buffTime_Defense = 0.0f; }
                else { StartCoroutine(DefenseBuff(0.2f)); }
                break;
            case ItemType.CriticalUp:
                if (buffCritical) { buffTime_Critical = 0.0f; }
                else { StartCoroutine(CriticalBuff(0.1f)); }
                break;
            case ItemType.CooltimeDown:
                if (buffCoolDown) { buffTime_CoolDown = 0.0f; }
                else { StartCoroutine(CoolDownBuff(0.2f)); }
                break;
            case ItemType.BlueGem:
                break;
            case ItemType.RedGem:
                break;
            case ItemType.HPFew:
                Recovered(1500);
                break;
            case ItemType.HPNormal:
                Recovered(2500);
                break;
            case ItemType.HPMuch:
                Recovered(3500);
                break;
        }
    }

    IEnumerator AttackBuff(float amount)
    {
        coef_BaseAtk += amount;
        buffAttack = true;
        while (buffTime_Attack < 10f)
        {
            yield return null;
        }
        coef_BaseAtk -= amount;
        buffAttack = false;
    }
    IEnumerator DefenseBuff(float amount)
    {
        coef_BaseDefense -= amount;
        buffDefense = true;
        while (buffTime_Defense < 10f)
        {
            yield return null;
        }
        coef_BaseDefense += amount;
        buffDefense = false;
    }
    IEnumerator CriticalBuff(float amount)
    {
        coef_CriticalAtk += amount;
        buffCritical = true;
        while (buffTime_Critical < 10f)
        {
            yield return null;
        }
        coef_CriticalAtk -= amount;
        buffCritical = false;
    }
    IEnumerator CoolDownBuff(float amount)
    {
        coef_SkillCoolDownAll -= amount;
        buffCoolDown = true;
        while (buffTime_CoolDown < 10f)
        {
            yield return null;
        }
        coef_SkillCoolDownAll += amount;
        buffCoolDown = false;
    }

    public void Action(int num)
    {
        skillCommand[num].Execute(this.gameObject);
    }


    public void SetDir(Vector2 dir)//방향키 입력 여부
    {
        Dir.x = dir.x;
        Dir.z = dir.y;
        Dir = Dir.normalized;
    }
    public bool GetDir()//방향키 입력 여부
    {
        if (Dir.x == 0 && Dir.z == 0)
        {
            return false;//방향키를 입력하지않음
        }

        return true;
    }
    protected override IEnumerator Attack()
    {
        canRun = false;
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f)
            {
                comboOnOff = false;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f)
            {
                if (!comboOnOff)
                {
                    SetState(State.Idle);
                }
            }

        } while (!isNewState);
        canRun = true;
    }

    protected override IEnumerator Run()
    {
        do
        {
            yield return null;
            if (!GetDir() && animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                SetState(State.Idle);
            }

        } while (!isNewState);
    }
    protected override IEnumerator Dead()
    {

        do
        {
            yield return null;

        } while (!isNewState);
    }
    private IEnumerator Roll()
    {
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f)
            {
                if (!GetDir())
                {
                    SetState(State.Idle);
                }
                else
                {
                    moveCommand.Execute(this.gameObject);
                }
            }

        } while (!isNewState);
        invincibility = false;
    }


    private IEnumerator Dash()
    {
        canRun = false;
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f)
            {
                SetState(State.Idle);
            }
        } while (!isNewState);
        canRun = true;
    }
    private IEnumerator Buff()
    {
        canRun = false;
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.8f)
            {
                SetState(State.Idle);
            }


        } while (!isNewState);
        canRun = true;
    }
    private IEnumerator Skill1()
    {
        canRun = false;
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f)
            {
                SetState(State.Idle);
            }


        } while (!isNewState);
        canRun = true;
    }
    private IEnumerator Skill2()
    {
        canRun = false;
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.7f)
            {

                SetState(State.Idle);

            }
        } while (!isNewState);
        canRun = true;
    }
    private IEnumerator Skill3()
    {
        canRun = false;
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.9f)
            {

                SetState(State.Idle);

            }
        } while (!isNewState);
        canRun = true;
    }
    private IEnumerator SuperMove()
    {
        canRun = false;
        do
        {
            yield return null;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.9f)
            {
                SetState(State.Idle);
            }
        } while (!isNewState);
        canRun = true;
    }

    public bool IsStanding() { return m_state == State.Idle; }
    public bool IsAttacking() { return m_state == State.Attack; }
    public bool IsDashing() { return m_state == State.Dash; }
    public bool IsRunning() { return m_state == State.Run; }
    public bool IsRolling() { return m_state == State.Roll; }
    public bool IsUsingSkill1() { return m_state == State.Skill1; }
    public bool IsUsingSkill2() { return m_state == State.Skill2; }
    public bool IsUsingSkill3() { return m_state == State.Skill3; }
    public bool IsDead() { return m_state == State.Dead; }
}


