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
    private SpeakingNPC playerSpeakerData, npcSpeakerData;
    private GameStateShell gameState;
    private TextAsset[] JSONData;
    private JSONHolder jsonHolder;
    [SerializeField]
    private GameObject convoHUDObject;
    private ConversationHUD conversationHUD;
    #endregion

    private void Start()
    {
        conversationHUD = convoHUDObject.transform.parent.gameObject.GetComponent<ConversationHUD>();
        jsonHolder = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpeakerData = jsonHolder.getSpeaker("Player");
        gameState = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameState>().getGameState();
    }//End Start

    #region Behaviours

    public void converse(GameObject npc)
    {
        this.npc = npc;
        npcSpeakerData = jsonHolder.getSpeaker(npc.name);
        conversationHUD.setNPCData(npcSpeakerData);
        Debug.Log("Starting conversation with " + this.npc.name);
        //1 - get correct conversation
        getCorrectConvo(gameState, npc, out Conversation convo);
        //2 - speaker stuff
        Set currentSet = jsonHolder.getSet(convo.setIDs[0]);
        Line currentLine = jsonHolder.getLine(currentSet.lineIDs[0]);
        conversationHUD.setLineInBox(currentLine.text);
        conversationHUD.setName(npcSpeakerData.speakerName);
        conversationHUD.setPortrait(npcSpeakerData.portrait);
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
        convo = jsonHolder.getConversation("TestConversation1");
        //If a relevant item has been found, load that relevant conversation
        //If certain lines have been seen, load the relevant conversation
        //If not matching conditions are found, load the default conversation
    }//End getCorrectConvo
    #endregion
}
