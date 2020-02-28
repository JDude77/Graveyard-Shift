using System.Collections;
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
    private string currentLine;
    //The name to display now
    private string currentName;
    //The portrait to display now
    private Sprite currentImage;
    //The sound to play while text scrolls in
    private AudioClip textBlip;
    //Audio source to hold the blips
    private AudioSource blipSource;
    //Typing speed
    private float typingSpeed = 0.01f;
    //Dialogue options for a player
    private List<SetLine> dialogueOptions;
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
    #endregion

    #region Behaviours
    private void Awake()
    {
        dialogueChoicePrefab = (GameObject) Resources.Load("Prefabs/Choice");
    }//End Awake

    public CurrentDialogue()
    {
        conversationHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<ConversationHUD>();
    }//End public

    public CurrentDialogue(Line line, SpeakingNPC npc)
    {
        //Set up all CurrentDialogue variables with Line data
        conversationHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<ConversationHUD>();
        currentLine = line.text;
        currentName = npc.speakerName;
        currentImage = npc.portrait;
        textBlip = npc.voice;
    }//End Constructor

    public void setUpDialogueOptions()
    {
        if (dialogueOptions.Count != 0)
        {
            List<GameObject> dialogueOptionObjects = new List<GameObject>();
            for (int i = 0; i < dialogueOptions.Count; i++)
            {
                dialogueOptionObjects.Add(dialogueChoicePrefab);
                dialogueOptionObjects[i].GetComponent<DialogueOption>().setDialogueOption(dialogueOptions[i]);
            }//End for
            conversationHUD.displayDialogueOptions(dialogueOptionObjects);
        }//End if
        else
        {
            Debug.LogError("There was a call to set up dialogue options, but there were no dialogue options set.");
        }//End else
    }//End setUpDialogueOptions

    public void speakLine()
    {
        conversationHUD.setNPCLineInBox(currentLine);
        conversationHUD.setNPCName(currentName);
        conversationHUD.setNPCPortrait(currentImage);
        conversationHUD.setCurrentDialogue(this);
        //StopAllCoroutines();
        //StartCoroutine(Type());
    }//End speakLine

    IEnumerator Type()
    {
        //For every letter in the line of text
        foreach(char letter in currentLine)
        {
            //Add a character from it to the displayed text
            //lineDisplay.text += letter;
            //Play speech blip
            if (textBlip != null)
            {
                blipSource.PlayOneShot(textBlip);
            }//End if
            //Wait before adding the next one
            yield return new WaitForSeconds(typingSpeed);
        }//End foreach
    }//End Type enumerator
    #endregion
}
