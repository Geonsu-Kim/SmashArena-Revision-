using System.Collections;
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
    private Command[] skillCommands;

    public Vector3 Dir = new Vector3(0, 0, 0);
    public List<Skill> skills;
    public PlayerMana mana;
    public StageTrigger stageTrigger;
    public CharacterController m_cc;
    [HideInInspector] public int blueGem;
    [HideInInspector] public int redGem;
    [HideInInspector] public int cri_Level;
    [HideInInspector] public int def_Level;
    public bool CanRun { get { return canRun; } }
    public bool ComboOnOff { get { return comboOnOff; } set { comboOnOff = value; } }
    public bool IsInStageBtn { get { return isInStageBtn; } set { isInStageBtn = value; } }


    public bool BuffAttack { get { return buffAttack; } }
    public bool BuffDefense { get { return buffDefense; } }
    public bool BuffCritical { get { return buffCritical; } }
    public bool BuffCoolDown { get { return buffCoolDown; } }

    public float CheckBuff(bool buff) { if (buff) return 0.25f; else return 0f; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        m_cc = GetComponent<CharacterController>();
        moveCommand = new MoveCommand();
        skillCommands = new SkillCommand[5];
        for (int i = 0; i < skillCommands.Length; i++)
        {
            skillCommands[i] = new SkillCommand(i);
        }

        skills = new List<Skill>();
    }
    private void Start()
    {
        ObjectPoolManager.Instance.CreateObject("Crasher", 1);
        ObjectPoolManager.Instance.CreateObject("OverpoweredSlash");
        ObjectPoolManager.Instance.CreateObject("Registance", 1);
        PlayerInBattleIO.LoadData();
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
        if (isDead()) return;

        health.Damaged(amount * (1-(0.05f*def_Level)-CheckBuff(buffDefense)));
        RenewHpBar();
        StartCoroutine(ColorByHit());
        if (health.IsDead())
        {
            SetStateTrigger(State.Dead);
        }
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
        switch (buff)
        {
            case BuffType.AttackUp:
                if (buffAttack) buffTime_Attack = 0; else StartCoroutine(AttackBuff());
                break;
            case BuffType.DefenseUp:
                if (buffDefense) buffTime_Defense = 0; else StartCoroutine(DefenseBuff());
                break;
            case BuffType.CriticalUp:
                if (buffCritical) buffTime_Critical = 0; else StartCoroutine(CriticalBuff());
                break;
            case BuffType.CooltimeDown:
                if (buffCoolDown) buffTime_CoolDown = 0; else StartCoroutine(CoolDownBuff());
                break;
        }
    }
    public void GetGoods(GoodsType goods,int amount)
    {
        switch (goods)
        {
            case GoodsType.BlueGem:
                GameDataBase.Instance.blueGem += amount;
                break;
            case GoodsType.RedGem:
                GameDataBase.Instance.redGem += amount;
                break;
        }
    }
    public void GetPotion(PotionType potion,int level)
    {
        int amount = 0;
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
    }

    IEnumerator AttackBuff()
    {
        GameDataBase.Instance.coef_BaseAtk += 0.25f;
        buffAttack = true;
        while (buffTime_Attack < 10f)
        {
            yield return null;
            buffTime_Attack += Time.deltaTime * Time.timeScale;
        }
        GameDataBase.Instance.coef_BaseAtk -= 0.25f;
        buffAttack = false;
    }
    IEnumerator DefenseBuff()
    {
        buffDefense = true;
        while (buffTime_Defense < 10f)
        {
            yield return null;
            buffTime_Defense += Time.deltaTime * Time.timeScale;
        }
        buffDefense = false;
    }
    IEnumerator CriticalBuff()
    {
        buffCritical = true;
        while (buffTime_Critical < 10f)
        {
            yield return null;
            buffTime_Critical += Time.deltaTime * Time.timeScale;
        }
        buffCritical = false;
    }
    IEnumerator CoolDownBuff()
    {
        buffCoolDown = true;
        while (buffTime_CoolDown < 10f)
        {
            yield return null;
            buffTime_CoolDown += Time.deltaTime * Time.timeScale;
        }
        buffCoolDown = false;
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


