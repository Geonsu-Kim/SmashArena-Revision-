using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    protected FSMEnemy Enemy;
    protected FSMPlayer Player;


    public Collider[] colliders;
    public Transform[] points;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Enemy = GetComponent<FSMEnemy>();
        Player = Enemy.Player;
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
