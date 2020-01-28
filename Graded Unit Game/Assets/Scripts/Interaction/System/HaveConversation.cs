using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONUtilityExtended;
using System;

//Control the functionality of having the conversation
public class HaveConversation : MonoBehaviour
{
    #region Attributes
    private GameObject player, npc;
    private GameStateShell gameState;
    private TextAsset[] JSONData;
    private JSONUtility jsonUtility;
    private JSONHolder jsonHolder;
    #endregion

    private void Start()
    {
        jsonUtility = new JSONUtility();
        jsonHolder = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameState = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameState>().getGameState();
    }//End Start

    #region Behaviours

    public void converse(GameObject npc)
    {
        this.npc = npc;
        Debug.Log("Starting conversation with " + this.npc.name);
        //1 - get correct conversation
        getCorrectConvo(gameState, npc, out Conversation convo);
        //2 - speaker stuff
        //3 - move to next conversation part
        Debug.Log("Ending conversation with " + this.npc.name);
        player.GetComponent<PlayerInteraction>().setIsInteracting(false);
    }//End converse

    private void getCorrectConvo(GameStateShell gameState, GameObject npc, out Conversation convo)
    {
        //Get the relevant speaker
        Dictionary<string, SpeakingNPC> speakers = jsonHolder.getSpeakers();
        
        SpeakingNPC speaker;
        speakers.TryGetValue(npc.name, out speaker);
        //Check if the speaker has been interacted with before
        bool interacted;
        gameState.interactedWithAtLeastOnce.TryGetValue(speaker.speakerID, out interacted);
        if(!interacted)
        {
            //Load default first conversation with speaker
            
        }//End if
        else
        {

        }//End else
        jsonHolder.getConversations().TryGetValue(gameState, out convo);
        //If a relevant item has been found, load that relevant conversation
        //If certain lines have been seen, load the relevant conversation
        //If not matching conditions are found, load the default conversation
    }//End getCorrectConvo
    #endregion
}
