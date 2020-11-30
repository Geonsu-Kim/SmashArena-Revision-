using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    protected FSMEnemy Enemy;
    protected FSMPlayer Player;
    protected Collider[] colliders;

    // Start is called before the first frame update
    protected void Start()
    {
        Enemy = GetComponent<FSMEnemy>();
        Player = Enemy.Player;
        colliders = Enemy.colliders;
    }
    protected void EnabledCollider(int num)
    {
        colliders[num].enabled = true;
    }
    protected void DisabledCollider(int num)
    {
        colliders[num].enabled = false;
    }


    
}
