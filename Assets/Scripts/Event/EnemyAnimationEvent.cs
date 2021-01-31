using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
public class EnemyAnimationEvent : MonoBehaviour
{
    protected FSMEnemy Enemy;
    protected Collider[] checkedColliders;
    protected  Collider[] Weapon;
    public  EnemySkillIcon skillIcon;

    protected string SFXname;
    protected StringBuilder stringBuilder;
    // Start is called before the first frame update
    protected void Awake()
    {
        stringBuilder = new StringBuilder(64); 
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
