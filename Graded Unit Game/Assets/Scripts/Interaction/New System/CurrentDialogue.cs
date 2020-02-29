﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//This is what will pass on the information to the dialogue HUD
public class CurrentDialogue : MonoBehaviour
{
    #region Attributes
    #region Data To Pass
    //The line to display now
    [SerializeField]
    private string currentLine;
    //What is currently actually displayed of the chosen dialogue
    [SerializeField]
    private string displayLine = "";
    [SerializeField]
    //The name to display now
    private string currentName;
    //The portrait to display now
    [SerializeField]
    private Sprite currentImage;
    //The sound to play while text scrolls in
    [SerializeField]
    private AudioClip textBlip;
    //Audio source to hold the blips
    [SerializeField]
    private AudioSource blipSource;
    //Typing speed
    [SerializeField]
    private float typingSpeed = 0.01f;
    [SerializeField]
    //Dialogue options for a player
    private List<SetLine> dialogueOptions;
    //Tracking whether an option has been chosen
    private bool optionChosen;
    //Coroutine for typing letter by letter
    [SerializeField]
    private IEnumerator typing;
    #endregion
    #region Data Pass Locations
    //Player Choice Prefab
    private GameObject dialogueChoicePrefab;
    //The Conversation HUD Manager
    private ConversationHUD conversationHUD;
    #endregion
    #endregion

    #region Getters & Setters
    public string getCurrentLine()
    {
        return currentLine;
    }//End Current Line Getter

    public void setCurrentLine(string currentLine)
    {
        this.currentLine = currentLine;
    }//End Current Line Setter

    public string getCurrentName()
    {
        return currentName;
    }//End Current Name Getter

    public void setCurrentName(string currentName)
    {
        this.currentName = currentName;
    }//End Current Name Setter

    public Sprite getCurrentImage()
    {
        return currentImage;
    }//End Current Image Getter

    public void setCurrentImage(Sprite currentImage)
    {
        this.currentImage = currentImage;
    }//End Current Image Setter

    public List<SetLine> getDialogueOptions()
    {
        return dialogueOptions;
    }//End Dialogue Options Getter

    public void setDialogueOptions(List<SetLine> dialogueOptions)
    {
        this.dialogueOptions = dialogueOptions;
    }//End Dialogue Options Setter

    public string getDisplayLine()
    {
        return displayLine;
    }//End Display Line Getter

    public bool getOptionChosen()
    {
        return optionChosen;
    }//End Option Chosen Getter

    internal void setOptionChosen(bool optionChosen)
    {
        this.optionChosen = optionChosen;
    }//End Option Chosen Setter
    #endregion

    #region Behaviours
    private void Awake()
    {
        dialogueChoicePrefab = (GameObject) Resources.Load("Prefabs/Option");
        conversationHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<ConversationHUD>();
        conversationHUD.setNPCLineInBox("");
        conversationHUD.setPlayerLineInBox("");
    }//End Awake

    public CurrentDialogue()
    {
        
    }//End public

    public CurrentDialogue(Line line, SpeakingNPC npc)
    {
        //Set up all CurrentDialogue variables with Line data
        currentLine = line.text;
        currentName = npc.speakerName;
        currentImage = npc.portrait;
        textBlip = npc.voice;
    }//End Constructor

    public void setUpDialogueOptions()
    {
        if (dialogueOptions.Count != 0)
        {
            VerticalLayoutGroup choiceArea = conversationHUD.getPlayerSpeakingDisplay().GetComponentInChildren<VerticalLayoutGroup>();
            GameObject choiceAreaObject = choiceArea.gameObject;
            List<GameObject> dialogueOptionObjects = new List<GameObject>();
            for (int i = 0; i < dialogueOptions.Count; i++)
            {
                dialogueOptionObjects.Add(dialogueChoicePrefab);
                dialogueOptionObjects[i].GetComponent<DialogueOption>().setDialogueOption(dialogueOptions[i]);
                var newOption = Instantiate(dialogueOptionObjects[i], choiceAreaObject.transform);
            }//End for
            conversationHUD.setCurrentDialogue(this);
        }//End if
        else
        {
            Debug.LogError("There was a call to set up dialogue options, but there were no dialogue options set.");
        }//End else
    }//End setUpDialogueOptions

    public void speakLine()
    {
        typing = TypeLine();
        if (typing != null)
        {
            StopCoroutine(typing);
        }//End if
        conversationHUD.setNPCName(currentName);
        conversationHUD.setNPCPortrait(currentImage);
        conversationHUD.setCurrentDialogue(this);
        conversationHUD.setNPCLineInBox(currentLine);
        
        StartCoroutine(typing);
    }//End speakLine

    private IEnumerator TypeLine()
    {
        conversationHUD.setNPCLineInBox("");
        displayLine = "";
        //For every letter in the line of text
        foreach(char letter in currentLine)
        {
            //Add a character from it to the displayed text
            displayLine += letter;
            switch(letter)
            {
                case ',': typingSpeed = 0.25f; break;
                case '.': typingSpeed = 0.5f; break;
                default: typingSpeed = 0.01f; break;
            }//End switch
            conversationHUD.setNPCLineInBox(displayLine);
            //Play speech blip
            if (textBlip != null)
            {
                blipSource.PlayOneShot(textBlip);
            }//End if
            //Wait before adding the next one
            yield return new WaitForSeconds(typingSpeed);
        }//End foreach
    }//End Type enumerator

    public void speakerIsPlayer(SpeakingNPC playerData)
    {
        currentImage = playerData.portrait;
        currentName = playerData.speakerName;
    }//End speakerIsPlayer
    #endregion
}