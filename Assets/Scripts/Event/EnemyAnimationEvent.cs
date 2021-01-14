using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    protected FSMEnemy Enemy;
    protected Collider[] checkedColliders;
    protected  Collider[] Weapon;
    public  EnemySkillIcon skillIcon;
    // Start is called before the first frame update
    protected void Start()
    {
        Enemy = GetComponent<FSMEnemy>();
        Weapon = Enemy.Weapon;
    }
    protected void ShowSkill(AnimationEvent @event)
    {
        if (skillIcon != null) {
            skillIcon.SetIcon(@event.intParameter);
                }
    }
    protected void WeaponOn(int num)
    {
        Weapon[num].enabled=true;
    }
    protected void WeaponOff(int num)
    {
        Weapon[num].enabled = false;
    }
}
