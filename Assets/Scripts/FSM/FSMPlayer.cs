using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAction))]
public class FSMPlayer : FSMBase
{

    private string SFXname;

    private bool comboOnOff = false;
    private bool canRun = true;
    private int btnNum = 0;
    private int exp;
    private int level;

    private bool buffAttack = false;
    private bool buffDefense = false;
    private bool buffCritical = false;
    private bool buffCoolDown = false;

    private float buffTime_Attack = 0;
    private float buffTime_Defense = 0;
    private float buffTime_Critical = 0;
    private float buffTime_CoolDown = 0;

    private Command moveCommand;
    private Command[] skillCommands;

    public Vector3 Dir;
    public Vector3 StartPos;
    public List<Skill> skills;
    public PlayerMana mana;
    public CharacterController m_cc;
    [HideInInspector] public int blueGem;
    [HideInInspector] public int redGem;
    [HideInInspector] public int cri_Level;
    [HideInInspector] public int def_Level;
    [HideInInspector] public StageTrigger stageTrigger;
    [HideInInspector] public PortalTrigger portal;
    public bool CanRun { get { return canRun; } }
    public bool ComboOnOff { get { return comboOnOff; } set { comboOnOff = value; } }
    public int BtnNum { get { return btnNum; } set { btnNum = value; } }

    public int Exp { get { return exp; } set { exp = value; } }

    public int Level { get { return level; } set { level = value; } }
    public bool BuffAttack { get { return buffAttack; } }
    public bool BuffDefense { get { return buffDefense; } }
    public bool BuffCritical { get { return buffCritical; } }
    public bool BuffCoolDown { get { return buffCoolDown; } }
    public float CheckBuff(bool buff) { if (buff) return 0.25f; else return 0f; }

    public bool LevelUpDef() { if (def_Level < 5) { def_Level++; return true; } else { return false; }  }
    public bool LevelUpCri() { if(cri_Level<5){cri_Level++; return true; } else { return false; }   }
    public void GetExp(int amount)
    {
        exp += amount;
        if (level > LevelData.statList.Count) return;
        while (exp>= LevelData.statList[level - 1].needExp)
        {
            exp -= - LevelData.statList[level - 1].needExp;
            level++;
            InitStat();
        }

    }
    protected override void Awake()
    {
        base.Awake();
        m_cc = GetComponent<CharacterController>();
        moveCommand = new MoveCommand();
        skillCommands = new SkillCommand[5];
        for (int i = 0; i < skillCommands.Length; i++)
        {
            skillCommands[i] = new SkillCommand(i);
        }
    }
    
    private void Start()
    {
        InitStat();
        skills = new List<Skill>();
        blueGem = GameSceneManager.Instance.BlueGemAmountInit;
        cri_Level = 0;
        def_Level = 0;
        for (int i = 0; i < SkillData.skillList.Count; i++)
        {
            skills.Add(new Skill(SkillData.skillList[i], 1));
        }
        Warp(GameSceneManager.Instance.startPos.position);
        InitStat();
    }
    private void InitStat()
    {
        Debug.Log(LevelData.statList.Count);
        if (level > LevelData.statList.Count) return;
        health.MaxHP = LevelData.statList[level - 1].Hp;
        mana.MaxMP = LevelData.statList[level - 1].Mp;
        RecoverHP((int)health.MaxHP);
        RecoverMP((int)mana.MaxMP);
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
    public override void Damaged(float amount, bool critical = false)
    {
        if (isDead()) return;

        health.Damaged(amount * (1-(0.05f*def_Level)-CheckBuff(buffDefense)));
        RenewHpBar();
        StartCoroutine(ColorByHit());
        if (health.IsDead())
        {
            SetStateTrigger(State.Dead);
        }
    }
    public void Warp(Vector3 pos)
    {
        m_cc.enabled = false;
        this.transform.position = pos;
        m_cc.enabled = true;
    }
    public void RecoverHP(int amount)
    {
        if (isDead()) return;
        health.Recovered(amount);
        RenewHpBar();
    }
    public void ConsumeMana(float amount)
    {
        if (isDead()) return;
        mana.Consume(amount);
        RenewMpBar();
    }
    public void RecoverMP(float amount)
    {
        if (isDead()) return;
        mana.Recovered(amount);
        RenewMpBar();
    }
    public void RenewHpBar()
    {
        UIManager.Instance.RenewPlayerUI(ref UIManager.Instance.PlayerHpBar, health.Ratio());
    }
    public void RenewMpBar()
    {
        UIManager.Instance.RenewPlayerUI(ref UIManager.Instance.PlayerMpBar, mana.Ratio());
    }
    public void GetBuff(BuffType buff)
    {
        sb.Length = 0;
        switch (buff)
        {
            case BuffType.AttackUp:
                sb.Append("Attack Up!");
                if (buffAttack) buffTime_Attack = 0; else StartCoroutine(AttackBuff());
                break;
            case BuffType.DefenseUp:
                sb.Append("Defense Up!");
                if (buffDefense) buffTime_Defense = 0; else StartCoroutine(DefenseBuff());
                break;
            case BuffType.CriticalUp:
                sb.Append("Critical Up!");
                if (buffCritical) buffTime_Critical = 0; else StartCoroutine(CriticalBuff());
                break;
            case BuffType.CooltimeDown:
                sb.Append("Cooltime Down!");
                if (buffCoolDown) buffTime_CoolDown = 0; else StartCoroutine(CoolDownBuff());
                break;
        }
        ObjectPoolManager.Instance.CallText(sb.ToString(), this.transform.position + Vector3.up * 1.0f);

        sb.Length = 0;
        sb.Append("PlayerGetItem");
        SFXname = sb.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
    }
    public void GetGoods(GoodsType goods,int amount)
    {
        sb.Length = 0;
        switch (goods)
        {
            case GoodsType.BlueGem:
                sb.Append("Get BlueGem!");
                blueGem += amount;
                break;
            case GoodsType.RedGem:
                sb.Append("Red BlueGem!");
                redGem += amount;
                break;
        }
        ObjectPoolManager.Instance.CallText(sb.ToString(), this.transform.position + Vector3.up * 1.0f);
        sb.Length = 0;
        sb.Append("PlayerGetItem");
        SFXname = sb.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
    }
    public void GetPotion(PotionType potion,int level)
    {
        int amount = 0;
        sb.Length = 0;
        switch (level)
        {
            case 1: amount = 1500; break;
            case 2: amount = 2500; break;
            case 3: amount = 3500; break;
        }
        switch (potion)
        {
            case PotionType.HP:
                RecoverHP(amount);
                break;
            case PotionType.MP:
                amount /= 5;
                RecoverMP(amount);
                break;
        }
        sb.Append(potion.ToString()).Append(" +").Append(amount.ToString());
        ObjectPoolManager.Instance.CallText(sb.ToString(), this.transform.position + Vector3.up * 1.0f);
        sb.Length = 0;
        sb.Append("PlayerGetItem");
        SFXname = sb.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
    }

    IEnumerator AttackBuff()
    {
        UIManager.Instance.SignalOn(0);
        buffAttack = true;
        while (buffTime_Attack < 10f)
        {
            yield return null;
            buffTime_Attack += Time.deltaTime * Time.timeScale;
        }
        buffTime_Attack = 0;
        buffAttack = false;
        UIManager.Instance.SignalOff(0);
    }
    IEnumerator DefenseBuff()
    {
        UIManager.Instance.SignalOn(1);
        buffDefense = true;
        while (buffTime_Defense < 10f)
        {
            yield return null;
            buffTime_Defense += Time.deltaTime * Time.timeScale;
        }
        buffTime_Defense = 0;
       buffDefense = false;
        UIManager.Instance.SignalOff(1);
    }
    IEnumerator CriticalBuff()
    {
        UIManager.Instance.SignalOn(2);
        buffCritical = true;
        while (buffTime_Critical < 10f)
        {
            yield return null;
            buffTime_Critical += Time.deltaTime * Time.timeScale;
        }
        buffTime_Critical = 0;
       buffCritical = false;
        UIManager.Instance.SignalOff(2);
    }
    IEnumerator CoolDownBuff()
    {
        UIManager.Instance.SignalOn(3);
        buffCoolDown = true;
        while (buffTime_CoolDown < 10f)
        {
            yield return null;
            buffTime_CoolDown += Time.deltaTime * Time.timeScale;
        }
        buffTime_CoolDown = 0;
       buffCoolDown = false;
        UIManager.Instance.SignalOff(3);
    }

    public void Action(int num)
    {
        skillCommands[num].Execute(this.gameObject);
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
        
        Time.timeScale = 0;
        UIManager.Instance.ResultWindow.SetActive(true);
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


