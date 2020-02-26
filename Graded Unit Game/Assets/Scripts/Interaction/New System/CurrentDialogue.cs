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
    #endregion

    #region Behaviours
    private void Awake()
    {
        dialogueChoicePrefab = (GameObject) Resources.Load("Prefabs/Choice");
    }//End Awake

    public CurrentDialogue(Line line, SpeakingNPC npc)
    {
        //Set up all CurrentDialogue variables with Line data
        conversationHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<ConversationHUD>();
        currentLine = line.text;
        currentName = npc.speakerName;
        currentImage = npc.portrait;
        textBlip = npc.voice;
        blipSource = Instantiate(new AudioSource(), GameObject.FindGameObjectWithTag("Player").transform);
    }//End Constructor

    public void speakLine (string currentLine)
    {
        this.currentLine = currentLine;
        StartCoroutine(Type());
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
