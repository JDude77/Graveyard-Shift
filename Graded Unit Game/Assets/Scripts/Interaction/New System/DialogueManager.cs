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
            //If there is an active speaker
            if(set.speaker != null)
            {
                //If the active speaker is the NPC
                if (set.speaker.Equals("NPC"))
                {
                    //AND if the current line of dialogue has finished typing out in the NPC dialogue display
                    if (currentDialogue.getCurrentLine().Equals(currentDialogue.getDisplayLine()))
                    {
                        //Allow the player to press a button to continue the conversation
                        //THIS IS WHAT YOU WERE DOING BEFORE YOU STOPPED WORKING
                        if(Input.GetAxis("Interact") != 0)
                        {
                            runDialogue(null);
                        }//End if
                    }//End if
                }//End if
            }//End if
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
        runDialogue(null);
    }//End startDialogue

    public void runDialogue(SetLine setLineFromDialogueChoice)
    {
        //Destroy old dialogue object
        if(gameObject.GetComponent<CurrentDialogue>())
        {
            Destroy(gameObject.GetComponent<CurrentDialogue>());
        }//End if
        //Destroy old player choice objects
        if(gameObject.GetComponentsInChildren<DialogueOption>() != null)
        {
            foreach(DialogueOption oldOption in gameObject.GetComponentsInChildren<DialogueOption>())
            {
                Destroy(oldOption.gameObject);
            }//End foreach
        }//End if
        gameObject.AddComponent<CurrentDialogue>();
        currentDialogue = gameObject.GetComponent<CurrentDialogue>();
        //Try to get the next set in the conversation
        if (setLineFromDialogueChoice == null)
        {
            set = getNextSet(conversation, set);
        }//End if
        else
        {
            set = allData.getSetFromConversation(setLineFromDialogueChoice.nextSet, conversation);
        }//End else
        if (!conversationIsOver)
        {
            //If there is no next set in the conversation, the conversation is over
            if (set != null)
            {
                lines = getNextLines(set);
                if (set.speaker.Equals("PLAYER"))
                {
                    //Set up the player choices and present them on the screen
                    List<SetLine> dialogueOptions = new List<SetLine>();
                    foreach (SetLine setLine in set.setLines)
                    {
                        dialogueOptions.Add(setLine);
                    }//End foreach
                    currentDialogue.setDialogueOptions(dialogueOptions);
                    currentDialogue.setUpDialogueOptions();
                    //Make currentDialogue deal with displaying the player choice
                }//End if
                else if (set.speaker.Equals("NPC"))
                {
                    //Display individual NPC line
                    //currentDialogue.setCurrentImage(NPCData.portrait);
                    currentDialogue.setCurrentLine(lines[0].text);
                    if(gameState.characterNameIsKnown.TryGetValue(NPCData.speakerID, out bool nameIsKnown))
                    {
                        currentDialogue.setCurrentName(NPCData.speakerName);
                    }//End if
                    else
                    {
                        string unknownName = NPCData.speakerName;
                        foreach(Char letter in NPCData.speakerName)
                        {
                            unknownName.Replace(letter, '?');
                        }//End foreach
                        currentDialogue.setCurrentName(unknownName);
                    }//End else
                    currentDialogue.speakLine();
                }//End else if
                else
                {
                    Debug.LogError("ERROR IN SET JSON: PLAYER or NPC speaker marker in " + set.setID + " mistyped as " + set.speaker + ".");
                }//End else
            }//End if
        }//End if
        else
        {
            PlayerInteraction playerInteraction = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponentInChildren<PlayerInteraction>();
            playerInteraction.setIsInteracting(false);
        }//End else
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
        foreach (Line line in lines)
        {
            gameState.updateGameState(line.lineID);
        }//End foreach
        return lines;
    }//End getNextLine
    #endregion
}