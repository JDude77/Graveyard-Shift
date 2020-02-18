using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentDialogue : MonoBehaviour
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
    //Typing speed
    private float typingSpeed = 0.01f;
    #endregion
    #region Data Pass Locations
    //The display used to show the text on screen
    private TextMeshProUGUI lineDisplay;
    #endregion
    #endregion

    #region Behaviours
    private void Start()
    {
        
        StartCoroutine(Type());
    }//End Start

    IEnumerator Type()
    {
        //For every letter in the line of text
        foreach(char letter in currentLine)
        {
            //Add a character from it to the displayed text
            lineDisplay.text += letter;
            //Wait before adding the next one
            yield return new WaitForSeconds(typingSpeed);
        }//End foreach
    }//End Type enumerator
    #endregion
}
