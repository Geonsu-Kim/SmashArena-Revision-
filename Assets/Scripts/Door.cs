using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;


    public StageTrigger Trigger_Pre;
    public StageTrigger Trigger_Cur;
    void Awake()
    {
        animator = GetComponent<Animator>();
        if (Trigger_Pre != null)
        {
            Trigger_Pre.Doors_Next.Add(this);
        }
        if (Trigger_Cur != null)
        {
            Trigger_Cur.Doors_Cur.Add(this);
        }
    }
    public void Open()
    {
        animator.SetBool("Open", true);

    }
    public void Close()
    {

        animator.SetBool("Open", false);
    }
}
