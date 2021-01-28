using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
class PlayerAnimationEvent : MonoBehaviour
{
    private StringBuilder stringBuilder;
    private const string playerName = "Player";
    private string SFXname;
    private float damage;
    private FSMPlayer player;
    public GameObject SwordTrail;
    private void Awake()
    {
        player = this.GetComponent<FSMPlayer>();
        stringBuilder = new StringBuilder(64);
    }
    bool IsCritical()
    {
        int p = Random.Range(0, 100);
        if (p > 47.5 * (1- player.cri_Level*0.1f-player.CheckBuff(player.BuffCritical)) && p < 57.5 * (1+player.cri_Level * 0.1f+ player.CheckBuff(player.BuffCritical)))
        {
            return true;
        }
        return false;
    }
    public void OnAttackStart()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(playerName);
        int rand = Random.Range(0, 1);
        if (rand == 0)
        {
            stringBuilder.Append("Attack1");
        }
        else
        {
            stringBuilder.Append("Attack2");
        }
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        //SwordTrail.GetComponent<TrailRenderer>().emitting = true;
        Collider[] colls = Physics.OverlapBox(this.transform.position + this.transform.forward * 0.5f + Vector3.up * 0.5f, new Vector3(0.75f, 0.5f, 0.5f),Quaternion.identity,1<<8);
        if(colls.Length!=0)
        {
            stringBuilder.Length = 0;
            rand = Random.Range(0, 1);
            if (rand == 0)
            {
                stringBuilder.Append("Hit1");
            }
            else
            {
                stringBuilder.Append("Hit2");
            }
            SFXname = stringBuilder.ToString();
            SoundManager.Instance.PlaySFX(SFXname);
        }
        for (int i = 0; i < colls.Length; i++)
        {
            damage = player.skills[0].CalcDamage(player.BaseAtkDamage)*(1+player.CheckBuff(player.BuffAttack));
            FSMEnemy fSM = colls[i].gameObject.GetComponent<FSMEnemy>();
            fSM.Damaged(damage, IsCritical()); 
            ObjectPoolManager.Instance.CallObject("Hit",
           colls[i].gameObject.transform.position + Vector3.up * 1.0f,
           Quaternion.identity, true, 0.5f);
        }
    }
    public void OnAttackEnd()
    {
        //SwordTrail.GetComponent<TrailRenderer>().emitting = false;
    }
    public void OnSkill1()
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(playerName);
        stringBuilder.Append("Slash");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);

        StartCoroutine(PlayerManager.Instance.Cam.Shake(0.1f, 0.25f));
        ObjectPoolManager.Instance.CallObject("OverpoweredSlash1",
            this.transform.position + Vector3.up * 1.0f,
            this.transform.rotation * Quaternion.Euler(0, 0, 90), true, 1.5f);
        Collider[] colls = Physics.OverlapSphere(this.transform.position, 3f, 1 << 8);
        if (colls.Length != 0)
        {
            stringBuilder.Length = 0;
            int rand = Random.Range(0, 1);
            if (rand == 0)
            {
                stringBuilder.Append("Hit1");
            }
            else
            {
                stringBuilder.Append("Hit2");
            }
            SFXname = stringBuilder.ToString();
            SoundManager.Instance.PlaySFX(SFXname);
        }
        for (int i = 0; i < colls.Length; i++)
        {
            damage = player.skills[2].CalcDamage(player.BaseAtkDamage) * (1 + player.CheckBuff(player.BuffAttack));
            FSMEnemy fSM = colls[i].gameObject.GetComponent<FSMEnemy>();
            fSM.Damaged(damage, IsCritical());
            ObjectPoolManager.Instance.CallObject("Hit",
          colls[i].gameObject.transform.position + Vector3.up * 1.0f,
          Quaternion.identity, true, 0.5f);
        }
        if (player.skills[2].Level >= 4)
        {
            
        }
    }
    public void OnSkill2Ready()
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(playerName);
        stringBuilder.Append("CrasherReady");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
    }
    public void OnSkill2()
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(playerName);
        stringBuilder.Append("Crasher");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
        StartCoroutine(PlayerManager.Instance.Cam.Shake(0.3f, 0.5f));
        ObjectPoolManager.Instance.CallObject("Crasher",
     this.transform.position + Vector3.up * 1.0f + this.transform.forward * 0.1f,
     Quaternion.identity, true, 1.5f);

        Collider[] colls = Physics.OverlapSphere(this.transform.position + this.transform.forward * 0.1f, 3f, 1 << 8);
        for (int i = 0; i < colls.Length; i++)
        {

            damage = player.skills[3].CalcDamage(player.BaseAtkDamage) * (1 + player.CheckBuff(player.BuffAttack)); ;
            FSMEnemy fSM = colls[i].gameObject.GetComponent<FSMEnemy>();
            fSM.Damaged(damage, IsCritical());
            ObjectPoolManager.Instance.CallObject("Hit",
          colls[i].gameObject.transform.position + Vector3.up * 1.0f,
          Quaternion.identity, true, 0.5f);
        }
        if (player.skills[3].Level >= 4)
        {
           
        }
    }
    public void OnWalk()
    {

        stringBuilder.Length = 0;
        stringBuilder.Append(playerName);
        stringBuilder.Append("Step");
        SFXname = stringBuilder.ToString();
        SoundManager.Instance.PlaySFX(SFXname);
    }
}

