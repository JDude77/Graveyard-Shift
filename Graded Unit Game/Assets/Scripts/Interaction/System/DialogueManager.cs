using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Attributes
    private SpeakingNPC playerData, NPCData;
    private Conversation conversation;
    private Set set;
    private List<Line> lines;
    private bool conversationIsOver, doneBeforeLine = false, doneAfterLine = false;
    private CurrentDialogue currentDialogue;
    private UIManager uiManager;
    private AudioSource audioSource;
    private bool finishedLine, storedLineSeenResult;
    private int currentIndex;
    #endregion

    #region Behaviours
    //Get the values that will always need to be carried by the dialogue manager
    private void Start()
    {
        currentIndex = -1;
        finishedLine = false;
        conversationIsOver = true;
        playerData = JSONHolder.getSpeaker("Player");
        uiManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<UIManager>();
        audioSource = GameObject.FindGameObjectWithTag("Audio Source").GetComponent<AudioSource>();
    }//End Start

    private void Update()
    {
        if(conversationIsOver)
        {
            currentIndex = -1;
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
                    //AND if the current line of dialogue is completely done
                    if (finishedLine)
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
        currentIndex = 0;
        //Set the conversation being over variable to false
        conversationIsOver = false;
        //Get NPC speaker data
        NPCData = JSONHolder.getSpeaker(npcGameObject.GetComponent<Interactive>().getID());
        //Attach an audio source to the NPC if they don't have one already
        if(!npcGameObject.GetComponent<AudioSource>())
        {
            npcGameObject.AddComponent<AudioSource>();
        }//End if
        //Horrendous workaround
        currentDialogue = gameObject.AddComponent<CurrentDialogue>();
        currentDialogue.setBlipSource(npcGameObject.GetComponent<AudioSource>());
        DestroyImmediate(currentDialogue);
        //Find the relevant conversation
        conversation = JSONHolder.findConversation(NPCData);
        set = null;
        lines = new List<Line>();
        runDialogue(null);
    }//End startDialogue

    public void runDialogue(SetLine setLineFromDialogueChoice)
    {
        //Reset script run status
        storedLineSeenResult = false;
        finishedLine = false;
        doneBeforeLine = false;
        doneAfterLine = false;
        destroyOldDialogueObjects();
        currentDialogue = gameObject.AddComponent<CurrentDialogue>();
        //Try to get the next set in the conversation
        if (setLineFromDialogueChoice == null)
        {
            set = getNextSet(conversation, set);
        }//End if
        else
        {
            set = JSONHolder.getSetFromConversation(setLineFromDialogueChoice.nextSet, conversation);
        }//End else
        //If there is no next set in the conversation, the conversation is over
        if (!conversationIsOver && set != null)
        {
            lines = getNextLines(set);
            if (set.speaker.Equals("PLAYER"))
            {
                List<SetLine> dialogueOptions = setUpDialogueOptions();
                currentDialogue.speakerIsPlayer(playerData);
                currentDialogue.setDialogueOptions(dialogueOptions);
                currentDialogue.displayDialogueOptions();
            }//End if
             //Display individual NPC line
            else if (set.speaker.Equals("NPC"))
            {
                if(lines[0].doBeforeLine != null)
                {
                    //Run a script before the line itself has run
                    runDialogueLineScript(lines[0]);
                }//End if
                //Set NPC dialogue data up
                setNPCDialogueData();
                currentDialogue.speakLine();
                StartCoroutine(WaitForLineToFinish());
            }//End else if
            else
            {
                Debug.LogError("ERROR IN SET JSON: PLAYER or NPC speaker marker in " + set.setID + " mistyped as " + set.speaker + ".");
            }//End else
        }//End if
        else
        {
            currentDialogue.setBlipSource(null);
            PlayerInteraction playerInteraction = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponentInChildren<PlayerInteraction>();
            playerInteraction.setIsInteracting(false);
        }//End else
    }//End runDialogue

    private IEnumerator WaitForLineToFinish()
    {
        yield return new WaitUntil(() => currentDialogue.getFinishedTyping() == true);
        if(lines[0].doAfterLine != null)
        {
            runDialogueLineScript(lines[0]);
        }//End if
        finishedLine = true;
    }//End WaitToRunScript

    private List<SetLine> setUpDialogueOptions()
    {
        //Set up the player choices and present them on the screen
        List<SetLine> dialogueOptions = new List<SetLine>();
        foreach (SetLine setLine in set.setLines)
        {
            dialogueOptions.Add(setLine);
        }//End foreach
        return dialogueOptions;
    }//End setUpDialogueOptions

    //Set all the dialogue data for the NPC
    private void setNPCDialogueData()
    {
        //Set the NPC's portrait if they have one
        setNPCPortrait();
        //Todo: Add animation play functionality
        //Play a specified audio clip
        playAudioClip();
        //Set current dialogue's line text
        currentDialogue.setCurrentLine(lines[0].text);
        //Set current dialogue audio blip
        currentDialogue.setTextBlip(NPCData.voice);
        //Get whether the current NPC's name is known
        GameState.characterNameIsKnown.TryGetValue(NPCData.speakerID, out bool nameKnown);
        //If the NPC's name is known, set the current name to be their name
        if (nameKnown)
        {
            currentDialogue.setCurrentName(NPCData.speakerName);
        }//End if
        //If the NPC's name is not known, just make the entire damn thing a bunch of question marks
        else
        {
            string unknownName = NPCData.speakerName;
            foreach (char letter in NPCData.speakerName)
            {
                if (letter != ' ')
                {
                    unknownName = unknownName.Replace(letter, '?');
                }//End if
            }//End foreach
            currentDialogue.setCurrentName(unknownName);
        }//End else
    }//End setNPCDialogueData

    private void playAudioClip()
    {
        if (lines[0].audioClip != null)
        {
            audioSource.clip = lines[0].audioClip;
            audioSource.Play();
        }//End if
    }//End playAudioClip

    private void setNPCPortrait()
    {
        if (NPCData.portrait != null)
        {
            currentDialogue.setCurrentImage(NPCData.portrait);
        }//End if
    }//End setNPCPortrait

    //Get rid of things from the previous part of the conversation
    private void destroyOldDialogueObjects()
    {
        //Destroy old dialogue object
        if (gameObject.GetComponent<CurrentDialogue>())
        {
            currentDialogue = null;
            DestroyImmediate(gameObject.GetComponent<CurrentDialogue>());
        }//End if
        //Destroy old player choice objects
        if (GameObject.FindGameObjectWithTag("Dialogue Choice"))
        {
            foreach (GameObject oldOption in GameObject.FindGameObjectsWithTag("Dialogue Choice"))
            {
                DestroyImmediate(oldOption);
            }//End foreach
        }//End if
        //Clear old audio clip
        if (audioSource != null)
        {
            if (audioSource.clip != null)
            {
                audioSource.clip = null;
            }//End if
        }//End if
    }//End destroyOldDialogueObjects

    public void runDialogueLineScript(Line line)
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
                GameState.updateGameState(parameter, "unlock");
                break;
            case "learnName":
                GameState.updateGameState(parameter, "name");
                break;
            case "checkLineSeen":
                GameState.lineHasBeenSeen.TryGetValue(parameter, out storedLineSeenResult);
                break;
            case "changeNextSet":
                if(storedLineSeenResult)
                {
                    set.setLines[currentIndex].nextSet = parameter;
                }//End if
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
        if(set.setLines[currentIndex].nextSet != null && set.setLines[currentIndex].nextSet.Length > 0)
        {
            return JSONHolder.getSetFromConversation(set.setLines[currentIndex].nextSet, conversation);
        }//End if
        //Move on to the next set in the array
        else
        {
            int index = System.Array.IndexOf(conversation.setIDs, set.setLines[currentIndex].nextSet);
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
            currentIndex = Random.Range(0, set.setLines.Length);
            lines.Add(JSONHolder.getLineFromSet(currentIndex, set));
        }//End if
        //If the set is a sequential choice set, return the one line available
        else
        {
            currentIndex = 0;
            lines.Add(JSONHolder.getLineFromSet(0, set));
        }//End else
        foreach (Line line in lines)
        {
            GameState.updateGameState(line.lineID, "line");
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