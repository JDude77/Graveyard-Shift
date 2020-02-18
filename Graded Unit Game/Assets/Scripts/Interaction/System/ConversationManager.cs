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
        //Initial conversation data set-up
        this.npc = npc;
        npcSpeakerData = jsonHolder.getSpeaker(npc.name);
        conversationHUD.setNPCData(npcSpeakerData);
        Debug.Log("Starting conversation with " + this.npc.name);
        bool conversationIsOver = false;

        //Get the correct conversation for the state of the game
        getCorrectConvo(gameState, npc, out Conversation convo);

        //Set up all the variables to unpack all sets and all lines from the conversation
        setsFromConvo = new Set[convo.setIDs.Length];
        allLinesInConvo = new Dictionary<string, Line>();
        //Go through every set in the conversation
        for(int i = 0; i < setsFromConvo.Length; i++)
        {
            //Get that specific set from the conversation using the index of the loop
            setsFromConvo[i] = jsonHolder.getSetFromConversation(i, convo);
            //Set up an array to hold all the lines in this set
            linesFromSet = new Line[setsFromConvo[i].lineIDs.Length];
            //Go through every line in the set
            for(int j = 0; j < setsFromConvo[i].lineIDs.Length; j++)
            {
                //Add the line to the lines from set array using the indices of the loops
                linesFromSet[j] = jsonHolder.getLineFromSet(j, setsFromConvo[i]);
                //Add the line to the conversation lines dictionary, using the "lineID/setID" format for storage key
                allLinesInConvo.Add(linesFromSet[j].lineID + "/" + setsFromConvo[i].setID, linesFromSet[j]);
            }//End for
        }//End for
        //If the conversation is not yet over
        while (!conversationIsOver)
        {
            //Get the first set in the conversation
            currentSet = jsonHolder.getSet(convo.setIDs[0]);
            //Get the correct line from the set
            currentLine = getTheCorrectLine(currentSet, convo);
            //Mark the line as having been seen
            gameState.lineHasBeenSeen[currentLine.lineID] = true;
            if (currentLine == null)
            {
                conversationIsOver = true;
            }//End if
            else
            {
                conversationHUD.setLineInBox(currentLine.text);
                conversationHUD.setName(npcSpeakerData.speakerName);
                conversationHUD.setPortrait(npcSpeakerData.portrait);
            }//End else
        }//End while
        Debug.Log("Ending conversation with " + this.npc.name);
        convoHUDObject.SetActive(false);
        player.GetComponent<PlayerInteraction>().setIsInteracting(false);
    }//End converse

    private Line getTheCorrectLine(Set currentSet, Conversation currentConversation)
    {
        //If it's a "pick one line" set, pick one line randomly
        if(currentSet.pickOne)
        {
            return jsonHolder.getLine(currentSet.lineIDs[UnityEngine.Random.Range(0, currentSet.lineIDs.Length)]);
        }//End if
        //If it's a player choice set, return null because the whole set is needed
        else if(currentSet.playerChoice)
        {
            return null;
        }//End if
        //If it's a sequential line set, get the next line
        else
        {
            //If the conversation hasn't actually started yet, pick the first line
            if(currentLine == null)
            {
                return jsonHolder.getLine(currentSet.lineIDs[0]);
            }//End if
            //If the conversation has started, use the current set and current line to find the next one
            else
            {
                for(int i = 0; i < currentSet.lineIDs.Length; i++)
                {
                    if(currentSet.lineIDs[i].Equals(currentLine.lineID))
                    {
                        if(currentSet.lineIDs[i+1] != null)
                        {
                            return jsonHolder.getLine(currentSet.lineIDs[i + 1]);
                        }//End if
                        else
                        {
                            Debug.Log("End of set or an error if more is expected.");
                        }//End if
                    }//End if
                }//End for
            }//End else
        }//End else
        Debug.LogError("Something went very wrong in getting the correct line.");
        return null;
    }//End getTheCorrectLine

    private void getCorrectConvo(GameStateShell gameState, GameObject npc, out Conversation convo)
    {
        //Get the relevant speaker
        Dictionary<string, SpeakingNPC> speakers = jsonHolder.getSpeakers();
        SpeakingNPC speaker;
        speakers.TryGetValue(npc.GetComponent<Interactive>().getID(), out speaker);
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
    #endregion
}
