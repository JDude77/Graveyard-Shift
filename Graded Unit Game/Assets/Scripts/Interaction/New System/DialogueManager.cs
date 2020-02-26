using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Attributes
    private SpeakingNPC playerData, NPCData;
    private JSONHolder allData;
    private GameStateShell gameState;
    private Conversation conversation;
    private Set set;
    private List<Line> lines;
    private bool conversationIsOver;
    //Could be either CurrentDialogue or direct link to Conversation HUD
    private CurrentDialogue currentDialogue;
    private UIManager uiManager;
    #endregion

    #region Behaviours
    //Get the values that will always need to be carried by the dialogue manager
    private void Start()
    {
        conversationIsOver = true;
        allData = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        playerData = allData.getSpeaker("Player");
        gameState = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameState>().currentGameState;
        uiManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<UIManager>();
    }//End Start

    private void Update()
    {
        if(conversationIsOver)
        {
            uiManager.setHUD("exploration");
        }//End if
        else
        {
            uiManager.setHUD("conversation");
        }//End else
    }//End Update

    //Start a new conversation with an NPC
    public void startDialogue(GameObject npcGameObject)
    {
        //Set the conversation being over variable to false
        conversationIsOver = false;
        //Get NPC speaker data
        NPCData = allData.getSpeaker(npcGameObject.name);
        //Find the relevant conversation
        conversation = allData.findConversation(NPCData, gameState);
        set = null;
        lines = new List<Line>();
        while (!conversationIsOver)
        {
            runDialogue();
        }//End if
    }//End startDialogue

    private void runDialogue()
    {
        //Try to get the next set in the conversation
        set = getNextSet(conversation, set);
        //If there is no next set in the conversation, the conversation is over
        if (set != null)
        {
            lines = getNextLines(set);
            if(set.speaker.Equals("PLAYER"))
            {
                
            }//End if
            else if(set.speaker.Equals("NPC"))
            {
                
            }//End else if
            else
            {
                Debug.LogError("ERROR IN SET JSON: PLAYER or NPC speaker marker in " + set.setID + " mistyped as " + set.speaker + ".");
            }//End else
        }//End if
    }//End runDialogue

    private Set getNextSet(Conversation conversation, Set set)
    {
        //If it's the start of the conversation, get the first set
        if(set == null)
        {
            return allData.getSetFromConversation(0, conversation);
        }//End if
        //Get specific next set pointed to by the current set
        if(set.setLines[0].nextSet != null)
        {
            return allData.getSetFromConversation(set.setLines[0].nextSet, conversation);
        }//End if
        //Move on to the next set in the array
        else
        {
            int index = System.Array.IndexOf(conversation.setIDs, set.setLines[0].nextSet);
            if (index == -1)
            {
                conversationIsOver = true;
                return null;
            }//End else
            else
            {
                return allData.getSetFromConversation(index, conversation);
            }//End if
        }//End else
    }//End getNextSet

    private List<Line> getNextLines(Set set)
    {
        List<Line> lines = new List<Line>();
        //If the set is a player choice set, return all lines
        if(set.speaker.Equals("PLAYER"))
        {
            for(int i = 0; i < set.setLines.Length; i++)
            {
                lines.Add(allData.getLineFromSet(i, set));
            }//End for
        }//End if
        //If the set is a random choice set, return one line at random
        else if(set.speaker.Equals("NPC") && set.setLines.Length > 1)
        {
            int indexOfChoice = UnityEngine.Random.Range(0, set.setLines.Length);
            lines.Add(allData.getLineFromSet(indexOfChoice, set));
        }//End if
        //If the set is a sequential choice set, return the one line available
        else
        {
            lines.Add(allData.getLineFromSet(0, set));
        }//End else
        return lines;
    }//End getNextLine
    #endregion
}
