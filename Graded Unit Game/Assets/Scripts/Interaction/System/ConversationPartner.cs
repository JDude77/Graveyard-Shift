using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPartner : Interactive
{
    [SerializeField]
    protected GameObject conversationHUDGameObject;

    private new void Start()
    {
        base.Start();
        conversationHUDGameObject = GameObject.FindGameObjectWithTag("Conversation HUD");
        interactionMode = modes[0];
        displayVerb = "Talk";
    }//End Start

    protected override void Update()
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
        //Set the object to have been interacted with at least once
        GameState.interactedWithAtLeastOnce.TryGetValue(id, out bool interactedOnce);
        if (!interactedOnce)
        {
            GameState.updateGameState(id, "interacted");
        }//End if
    }//End Interact
}
