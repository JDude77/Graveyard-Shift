﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Attributes
    private SpeakingNPC playerData, NPCData;
    private JSONHolder allData;
    private GameStateShell gameState;
    #endregion

    #region Behaviours
    //Get the values that will always need to be carried by the dialogue manager
    private void Start()
    {
        allData = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        playerData = allData.getSpeaker("Player");
        gameState = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameState>().currentGameState;
    }//End Start

    //Start a new conversation with an NPC
    public void startDialogue(GameObject npcGameObject)
    {
        //Set the conversation being over variable to false
        bool conversationIsOver = false;
        //Get NPC speaker data
        NPCData = allData.getSpeaker(npcGameObject.name);
        //Find the relevant conversation
        Conversation conversation = allData.findConversation(NPCData, gameState);
        Set set = null;
        List<Line> lines = new List<Line>();
        if (!conversationIsOver)
        {
            set = getNextSet(conversation, set);
            lines = getNextLines(set);
            runLines(lines);
        }//End if
    }//End startDialogue

    private Set getNextSet(Conversation conversation, Set set)
    {
        //If it's the start of the conversation, get the first set
        if(set == null)
        {
            return allData.getSetFromConversation(0, conversation);
        }//End if
        //Get specific next set pointed to by the current set
        if(set.setLinks[0].nextSet != null)
        {
            return allData.getSetFromConversation(set.setLinks[0].nextSet, conversation);
        }//End if
        //Move on to the next set in the array
        else
        {
            int index = System.Array.IndexOf(conversation.setIDs, set.setLinks[0].nextSet);
            return allData.getSetFromConversation(index, conversation);
        }//End else
    }//End getNextSet

    private List<Line> getNextLines(Set set)
    {
        List<Line> lines = new List<Line>();
        //If the set is a player choice set, return all lines
        if(set.speaker.Equals("PLAYER"))
        {
            for(int i = 0; i < set.setLinks.Length; i++)
            {
                lines.Add(allData.getLineFromSet(i, set));
            }//End for
        }//End if
        //If the set is a random choice set, return one line at random
        else if(set.speaker.Equals("NPC") && set.setLinks.Length > 1)
        {
            int indexOfChoice = Random.Range(0, set.setLinks.Length - 1);
            lines.Add(allData.getLineFromSet(indexOfChoice, set));
        }//End if
        //If the set is a sequential choice set, return the one line available
        else
        {
            lines.Add(allData.getLineFromSet(0, set));
        }//End else
        return lines;
    }//End getNextLine

    private void runLines(List<Line> lines)
    {
        
    }//End runLines
    #endregion
}
