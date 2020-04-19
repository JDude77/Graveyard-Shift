using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Action
{
    private bool doorOpen;
    private Animator animator;
    private ConversationPartner janice;

    protected override void Start()
    {
        base.Start();
        doorOpen = false;
        animator = GetComponent<Animator>();
        displayVerb = "Open";
        janice = GameObject.FindGameObjectWithTag("Conversation Partner").GetComponent<ConversationPartner>();
    }//End Start

    protected override void Update()
    {
        base.Update();
        if(doorOpen && animator.GetCurrentAnimatorStateInfo(0).IsName("Opened"))
        {
            janice.setIsInteractible(true);
        }//End if
        else
        {
            janice.setIsInteractible(false);
        }//End else

        if ((!doorOpen && animator.GetCurrentAnimatorStateInfo(0).IsName("Closed")) || (doorOpen && animator.GetCurrentAnimatorStateInfo(0).IsName("Opened")))
        {
            isInteractible = true;
        }//End if
        else
        {
            isInteractible = false;
        }//End else
    }//End Update

    public override void interact()
    {
        base.interact();
        if(!doorOpen && animator.GetCurrentAnimatorStateInfo(0).IsName("Closed"))
        {
            animator.Play("Open Door");
            displayVerb = "Close";
        }//End if
        else if(doorOpen && animator.GetCurrentAnimatorStateInfo(0).IsName("Opened"))
        {
            animator.Play("Close Door");
            displayVerb = "Open";
        }//End else
        doorOpen = !doorOpen;
    }//End interact
}