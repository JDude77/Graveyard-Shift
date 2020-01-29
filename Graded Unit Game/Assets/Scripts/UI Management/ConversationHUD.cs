using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationHUD : MonoBehaviour
{
    #region Attributes
    private GameObject convoHUDObject;
    private ConversationHUD conversationHUD;
    private JSONHolder jsonHolder;
    private SpeakingNPC playerData, npcData;
    private Line currentLine;
    private Image portrait;
    private Text speakerName, lineInBox;
    private Text[] linesForPlayer;
    #endregion

    #region Getters & Setters
    //Portrait Getter
    public Image getPortrait()
    {
        return portrait;
    }//End Portrait Getter

    //Portrait Setter
    public void setPortrait(Image portrait)
    {
        this.portrait = portrait;
    }//End Portrait Setter

    //Just-In-Case Portrait Sprite Setter
    public void setPortrait(Sprite portrait)
    {
        Debug.LogWarning("You asked setPortrait to take in a sprite. It needs an image.");
        this.portrait.sprite = portrait;
    }//End Portrait Setter

    //Name Getter
    public Text getName()
    {
        return speakerName;
    }//End Name Getter

    //Name Setter
    public void setName(Text name)
    {
        this.speakerName = name;
    }//End Name Setter

    //Just-In-Case Name String Setter
    public void setName(string name)
    {
        Debug.LogWarning("You asked setName to take in a string. It needs a Text asset.");
        this.speakerName.text = name;
    }//End Name Setter

    //LineInBox Getter
    public Text getLineInBox()
    {
        return lineInBox;
    }//End LineInBox Getter

    //LineInBox Setter
    public void setLineInBox(Text lineInBox)
    {
        this.lineInBox = lineInBox;
    }//End LineInBox Setter

    //Just-In-Case LineInBox String Setter
    public void setLineInBox(string lineInBox)
    {
        Debug.LogWarning("You asked setLineInBox to take in a string. It needs a Text asset.");
        this.lineInBox.text = lineInBox;
    }//End LineInBox Setter

    //LinesForPlayer Getter
    public Text[] getLinesForPlayer()
    {
        return linesForPlayer;
    }//End LinesForPlayer Getter

    //LinesForPlayer Setter
    public void setLinesForPlayer(Text[] linesForPlayer)
    {
        this.linesForPlayer = linesForPlayer;
    }//End LinesForPlayer Setter

    //Just-In-Case LinesForPlayer String Array Setter
    public void setLinesForPlayer(string[] linesForPlayer)
    {
        Debug.LogWarning("You asked setLinesForPlayer to take in a string array. It needs a Text asset array.");
        for(int i = 0; i < this.linesForPlayer.Length; i++)
        {
            this.linesForPlayer[i].text = linesForPlayer[i];
        }//End for
    }//End LinesForPlayer Setter
    #endregion

    private void Start()
    {
        //Get access to the JSON Holder
        jsonHolder = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        //Get the player speaker data
        playerData = jsonHolder.getSpeaker("Player");
        //Get the NPC speaker data
        
        //Get the actual game object Conversation HUD
        convoHUDObject = GameObject.FindGameObjectWithTag("Conversation HUD");
        //Get the Conversation HUD script attached to the HUD
        conversationHUD = GetComponent<ConversationHUD>();
        //Get the portrait for the current speaker from the Conversation HUD object
        portrait = convoHUDObject.transform.Find("Portrait").GetComponent<Image>();
        //Get the text for the name of the current speaker from the Conversation HUD object
        speakerName = convoHUDObject.transform.Find("Name").GetComponent<Text>();
        //Get the line of text currently being displayed from the Conversation HUD object
        lineInBox = convoHUDObject.transform.Find("Line").GetComponent<Text>();
    }//End Start

    private void Update()
    {
        //If the HUD as a whole is active
        if(convoHUDObject.activeSelf)
        {
            //If NPC is talking
            if(!speakerName.text.Equals("Illden"))
            {
                
            }//End if
            //If it's a player choice scenario
            else
            {

            }//End else
        }//End if
    }//End Update
    #region Behaviours

    #endregion
}
