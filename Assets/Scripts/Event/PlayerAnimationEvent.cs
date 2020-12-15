﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerAnimationEvent : MonoBehaviour
{
    private float damage;
    private FSMPlayer player;
    public GameObject Slash;
    public GameObject Crasher;
    public GameObject SwordTrail;
    private void Awake()
    {
        player = this.GetComponent<FSMPlayer>();
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
        //SwordTrail.GetComponent<TrailRenderer>().emitting = true;
        Collider[] colls = Physics.OverlapBox(this.transform.position + this.transform.forward * 0.5f + Vector3.up * 0.5f, new Vector3(0.75f, 0.5f, 0.5f),Quaternion.identity,1<<8);
        for (int i = 0; i < colls.Length; i++)
        {
            damage = player.skills[0].CalcDamage(player.characterState.AttackDamage)*(1+player.CheckBuff(player.BuffAttack));
            FSMEnemy fSM = colls[i].gameObject.GetComponent<FSMEnemy>();
            fSM.Damaged((int)damage, IsCritical()); 
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
        if (player.skills[2].Level < 4)
        {
            StartCoroutine(GameSceneManager.Instance.Cam.Shake(0.1f, 0.25f));
            ObjectPoolManager.Instance.CallObject("OverpoweredSlash",
                this.transform.position + Vector3.up * 1.0f,
                this.transform.rotation * Quaternion.Euler(0, 0, 90), true, 1.5f);
            Collider[] colls = Physics.OverlapSphere(this.transform.position, 3f, 1 << 8);
            for (int i = 0; i < colls.Length; i++)
            {

                damage = player.skills[2].CalcDamage(player.characterState.AttackDamage) * (1 + player.CheckBuff(player.BuffAttack));

                FSMEnemy fSM = colls[i].gameObject.GetComponent<FSMEnemy>();
                fSM.rb.AddForce((fSM.transform.position - this.transform.position).normalized * 20f, ForceMode.VelocityChange);
                fSM.Damaged((int)damage, IsCritical());
                ObjectPoolManager.Instance.CallObject("Hit",
              colls[i].gameObject.transform.position + Vector3.up * 1.0f,
              Quaternion.identity, true, 0.5f);
            }
        }
        else if (GameDataBase.Instance.coef_Skill1 >= 4 && GameDataBase.Instance.coef_Skill1 < 7)
        {

        }
    }
    public void OnSkill2()
    {

        if (GameDataBase.Instance.coef_Skill2 < 4)
        {
            StartCoroutine(GameSceneManager.Instance.Cam.Shake(0.3f, 0.5f));
            ObjectPoolManager.Instance.CallObject("Crasher",
         this.transform.position + Vector3.up * 1.0f + this.transform.forward * 0.1f,
         Quaternion.identity, true, 1.5f);

            Collider[] colls = Physics.OverlapSphere(this.transform.position + this.transform.forward * 0.1f, 3f , 1 << 8);
            for (int i = 0; i < colls.Length; i++)
            {

                damage = player.skills[3].CalcDamage(player.characterState.AttackDamage) * (1 + player.CheckBuff(player.BuffAttack)); ;
                FSMEnemy fSM = colls[i].gameObject.GetComponent<FSMEnemy>();
                fSM.Damaged((int)damage, IsCritical());
                ObjectPoolManager.Instance.CallObject("Hit",
              colls[i].gameObject.transform.position + Vector3.up * 1.0f,
              Quaternion.identity, true, 0.5f);
            }
        }
        else if (GameDataBase.Instance.coef_Skill2 >= 4 && GameDataBase.Instance.coef_Skill2 < 7)
        {

        }
    }
}

