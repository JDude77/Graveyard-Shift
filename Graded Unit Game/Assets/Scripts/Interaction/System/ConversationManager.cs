using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONUtilityExtended;
using System;

//Control the functionality of having the conversation
public class ConversationManager : MonoBehaviour
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
    private Line currentLine;
    private Set currentSet;
    private Line[] linesFromSet;
    private Set[] setsFromConvo;
    private Dictionary<string, Line> allLinesInConvo;
    #endregion

    private void Start()
    {
        conversationHUD = convoHUDObject.transform.parent.gameObject.GetComponent<ConversationHUD>();
        jsonHolder = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpeakerData = jsonHolder.getSpeaker("Player");
        gameState = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameState>().getGameState();
        convoHUDObject.SetActive(true);
        currentLine = null;
        linesFromSet = null;
        setsFromConvo = null;
        allLinesInConvo = null;
    }//End Start

    #region Behaviours

    public void converse(GameObject npc)
    {
        this.npc = npc;
        npcSpeakerData = jsonHolder.getSpeaker(npc.name);
        conversationHUD.setNPCData(npcSpeakerData);
        Debug.Log("Starting conversation with " + this.npc.name);
        bool conversationIsOver = false;

        getCorrectConvo(gameState, npc, out Conversation convo);

        setsFromConvo = new Set[convo.setIDs.Length];
        allLinesInConvo = new Dictionary<string, Line>();
        for(int i = 0; i < setsFromConvo.Length; i++)
        {
            setsFromConvo[i] = getSetFromConversation(i, convo);
            linesFromSet = new Line[setsFromConvo[i].lineIDs.Length];
            for(int j = 0; j < setsFromConvo[i].lineIDs.Length; j++)
            {
                linesFromSet[j] = getLineFromSet(j, setsFromConvo[i]);
                allLinesInConvo.Add(linesFromSet[j].lineID+setsFromConvo[i].setID, linesFromSet[j])
            }//End for
        }//End for
        //1 - get correct conversation
        if (!conversationIsOver)
        {
            //2 - speaker stuff
            currentSet = jsonHolder.getSet(convo.setIDs[0]);
            currentLine = getTheCorrectLine(currentSet);
            conversationHUD.setLineInBox(currentLine.text);
            conversationHUD.setName(npcSpeakerData.speakerName);
            conversationHUD.setPortrait(npcSpeakerData.portrait);
            //3 - move to next conversation part
            moveToNextLine(convo);
        }//End if
        else
        {
            Debug.Log("Ending conversation with " + this.npc.name);
            convoHUDObject.SetActive(false);
            player.GetComponent<PlayerInteraction>().setIsInteracting(false);
        }//End else
    }//End converse

    private Line getTheCorrectLine(Set currentSet)
    {
        if(currentSet.pickOne)
        {
            return jsonHolder.getLine(currentSet.lineIDs[UnityEngine.Random.Range(0, currentSet.lineIDs.Length)]);
        }//End if
        else
        {
            if(currentLine == null)
            {
                return jsonHolder.getLine(currentSet.lineIDs[0]);
            }//End if
            else
            {
                if(currentLine.)
            }//End else
        }//End else
    }//End getTheCorrectLine

    private void getCorrectConvo(GameStateShell gameState, GameObject npc, out Conversation convo)
    {
        //Get the relevant speaker
        Dictionary<string, SpeakingNPC> speakers = jsonHolder.getSpeakers();
        SpeakingNPC speaker;
        speakers.TryGetValue(npc.GetComponent<Interact>().getID(), out speaker);
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
        //If no matching conditions are found, load the default conversation
    }//End getCorrectConvo

    private void moveToNextLine(Conversation convo)
    {
        
    }//End move to next line
    #endregion
}
