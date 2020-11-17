using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
private  NavMeshAgent nav;
    public float Speed
    {
        set
        {
            nav.speed = value;
        }
        get
        {
            return nav.speed;
        }
    }
    public float AngularSpeed
    {
        set
        {
            nav.angularSpeed = value;
        }
        get
        {
            return nav.angularSpeed;
        }
    }
    public void _Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    public void TraceTarget(Vector3 tr)
    {
        nav.isStopped = false;
        nav.SetDestination(tr);
    }
    public void Stop()
    {
        nav.isStopped = true;
    }
    public void Turn(bool _set)
    {
        nav.enabled = _set;
    }
}

