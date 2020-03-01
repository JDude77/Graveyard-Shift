using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Attributes
    private SpeakingNPC playerData, NPCData;
    private GameStateShell gameState;
    private Conversation conversation;
    private Set set;
    private List<Line> lines;
    private bool conversationIsOver, doneBeforeLine = false, doneAfterLine = false;
    //Could be either CurrentDialogue or direct link to Conversation HUD
    private CurrentDialogue currentDialogue;
    private UIManager uiManager;
    #endregion

    #region Behaviours
    //Get the values that will always need to be carried by the dialogue manager
    private void Start()
    {
        conversationIsOver = true;
        playerData = JSONHolder.getSpeaker("Player");
        gameState = GameState.currentGameState;
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
                        if(Input.GetAxisRaw("Interact") != 0)
                        {
                            runDialogue(null);
                        }//End if
                    }//End if
                }//End if
                else
                {
                    if (!currentDialogue.getOptionChosen())
                    {
                        checkIfAllDialogueOptionsFinishedTyping();
                    }//End if
                }//End else
            }//End if
        }//End else
    }//End Update

    //Start a new conversation with an NPC
    public void startDialogue(GameObject npcGameObject)
    {
        //Set the conversation being over variable to false
        conversationIsOver = false;
        //Get NPC speaker data
        NPCData = JSONHolder.getSpeaker(npcGameObject.GetComponent<Interactive>().getID());
        //Find the relevant conversation
        conversation = JSONHolder.findConversation(NPCData, gameState);
        set = null;
        lines = new List<Line>();
        runDialogue(null);
    }//End startDialogue

    public void runDialogue(SetLine setLineFromDialogueChoice)
    {
        //Reset script run status
        doneBeforeLine = false;
        doneAfterLine = false;
        //Destroy old dialogue object
        if(gameObject.GetComponent<CurrentDialogue>())
        {
            currentDialogue = null;
            DestroyImmediate(gameObject.GetComponent<CurrentDialogue>());
        }//End if
        //Destroy old player choice objects
        if(GameObject.FindGameObjectWithTag("Dialogue Choice"))
        {
            foreach(GameObject oldOption in GameObject.FindGameObjectsWithTag("Dialogue Choice"))
            {
                DestroyImmediate(oldOption);
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
            set = JSONHolder.getSetFromConversation(setLineFromDialogueChoice.nextSet, conversation);
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
                    currentDialogue.speakerIsPlayer(playerData);
                    currentDialogue.setDialogueOptions(dialogueOptions);
                    currentDialogue.setUpDialogueOptions();
                    //Make currentDialogue deal with displaying the player choice
                }//End if
                else if (set.speaker.Equals("NPC"))
                {
                    //Display individual NPC line
                    //currentDialogue.setCurrentImage(NPCData.portrait);
                    //Todo: Get portraits setting on player and NPC text boxes
                    //Todo: Add do before line functionality
                    //Todo: Add animation play functionality
                    //Todo: Add audio clip play functionality
                    runDialogueLineScript(lines[0]);
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
                            unknownName = unknownName.Replace(letter, '?');
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

    private void runDialogueLineScript(Line line)
    {
        if(line.doBeforeLine != null && !doneBeforeLine)
        {
            parseScriptFromLine(line.doBeforeLine);
            doneBeforeLine = true;
            return;
        }//End if
        else if(line.doAfterLine != null && !doneAfterLine)
        {
            parseScriptFromLine(line.doAfterLine);
            doneAfterLine = true;
            return;
        }//End else if
    }//End runDialogueLineScript

    private void parseScriptFromLine(string scriptToParse)
    {
        string scriptToRun = scriptToParse.Substring(0, scriptToParse.IndexOf('('));
        string parameter = scriptToParse.Substring(scriptToParse.IndexOf('(') + 1, (scriptToParse.IndexOf(')') - scriptToParse.IndexOf('(')) - 1);
        switch(scriptToRun)
        {
            case "unlockLevel":
                gameState.updateGameState(parameter, "unlock");
                break;
        }//End switch
    }//End parseScriptFromLine

    private Set getNextSet(Conversation conversation, Set set)
    {
        //If it's the start of the conversation, get the first set
        if(set == null)
        {
            return JSONHolder.getSetFromConversation(0, conversation);
        }//End if
        //Get specific next set pointed to by the current set
        if(set.setLines[0].nextSet != null)
        {
            return JSONHolder.getSetFromConversation(set.setLines[0].nextSet, conversation);
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
                return JSONHolder.getSetFromConversation(index, conversation);
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
                lines.Add(JSONHolder.getLineFromSet(i, set));
            }//End for
        }//End if
        //If the set is a random choice set, return one line at random
        else if(set.speaker.Equals("NPC") && set.setLines.Length > 1)
        {
            int indexOfChoice = UnityEngine.Random.Range(0, set.setLines.Length);
            lines.Add(JSONHolder.getLineFromSet(indexOfChoice, set));
        }//End if
        //If the set is a sequential choice set, return the one line available
        else
        {
            lines.Add(JSONHolder.getLineFromSet(0, set));
        }//End else
        foreach (Line line in lines)
        {
            gameState.updateGameState(line.lineID, "line");
        }//End foreach
        return lines;
    }//End getNextLine

    private void checkIfAllDialogueOptionsFinishedTyping()
    {
        DialogueOption[] dialogueOptions = GameObject.FindGameObjectWithTag("Player Choices").GetComponentsInChildren<DialogueOption>();
        for(int i = 0; i < dialogueOptions.Length; i++)
        {
            if(!dialogueOptions[i].getFinishedTyping())
            {
                return;
            }//End if
        }//End for
        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            dialogueOptions[i].GetComponent<Button>().interactable = true;
        }//End for
    }//End checkIfAllDialogueOptionsFinishedTyping
    #endregion
}