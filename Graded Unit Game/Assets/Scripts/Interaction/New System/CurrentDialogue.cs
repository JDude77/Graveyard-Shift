using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This is what will pass on the information to the dialogue HUD
public class CurrentDialogue
{
    #region Attributes
    #region Data To Pass
    //The line to display now (NPC talking)
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
    //The display used to show the text on screen
    private TextMeshProUGUI lineDisplay;
    //The Conversation HUD Manager
    private ConversationHUD conversationHUD;
    #endregion
    #endregion

    #region Behaviours
    public CurrentDialogue(Line line, SpeakingNPC npc)
    {
        //Set up all CurrentDialogue variables with Line data
        conversationHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<ConversationHUD>();
        currentLine = line.text;
        currentName = npc.speakerName;
        currentImage = npc.portrait;
        textBlip = npc.voice;
        //lineDisplay = conversationHUD.gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1];
        blipSource = new AudioSource();
    }//End Constructor

    public void speakLine (string currentLine)
    {
        this.currentLine = currentLine;
        Type();
    }//End speakLine

    IEnumerator Type()
    {
        //For every letter in the line of text
        foreach(char letter in currentLine)
        {
            //Add a character from it to the displayed text
            lineDisplay.text += letter;
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
