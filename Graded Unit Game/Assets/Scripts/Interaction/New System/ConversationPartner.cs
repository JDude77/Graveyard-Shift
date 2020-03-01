using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPartner : Interactive
{
    private new void Start()
    {
        base.Start();
        interactionMode = modes[0];
        isInteractible = true;
        displayVerb = "Talk";
    }//End Start

    private void Update()
    {
        if (conversationHUDGameObject.activeSelf)
        {
            isInteracting = true;
        }//End if
        else
        {
            isInteracting = false;
        }//End else
    }//End Update

    public override void interact()
    {
        base.interact();
        gameManager.GetComponent<DialogueManager>().startDialogue(gameObject);
    }//End Interact
}
