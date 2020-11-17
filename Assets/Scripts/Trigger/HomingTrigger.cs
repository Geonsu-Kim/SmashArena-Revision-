using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
public class HomingTrigger : EffectTrigger
{
    private NavMeshAgent nav;
    private FSMPlayer Player;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        Player = GameSceneManager.Instance.Player;   
    }
    private void Update()
    {
        nav.SetDestination(Player.transform.position+Vector3.up*0.5f);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (Player.gameObject.name.Equals(other.gameObject.name))
        {
            Player.Damaged(damageScalar);
            this.gameObject.SetActive(false);
        }
    }
}
