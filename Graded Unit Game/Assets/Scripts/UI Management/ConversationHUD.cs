﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationHUD : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private GameObject convoHUDObject, playerSpeakingDisplay, npcSpeakingDisplay;
    private JSONHolder jsonHolder;
    private SpeakingNPC playerData, npcData;
    private Line currentLine;
    private Image portrait;
    private TextMeshProUGUI speakerName, lineInBox;
    private TextMeshProUGUI[] linesForPlayer;
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
    public TextMeshProUGUI getName()
    {
        return speakerName;
    }//End Name Getter

    //Name Setter
    public void setName(TextMeshProUGUI name)
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
    public TextMeshProUGUI getLineInBox()
    {
        return lineInBox;
    }//End LineInBox Getter

    //LineInBox Setter
    public void setLineInBox(TextMeshProUGUI lineInBox)
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
    public TextMeshProUGUI[] getLinesForPlayer()
    {
        return linesForPlayer;
    }//End LinesForPlayer Getter

    //LinesForPlayer Setter
    public void setLinesForPlayer(TextMeshProUGUI[] linesForPlayer)
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

    //NPC Speaker Data Setter
    public void setNPCData(SpeakingNPC npcData)
    {
        this.npcData = npcData;
    }//End NPC Data Setter
    #endregion

    private void Start()
    {
        //Get access to the JSON Holder
        jsonHolder = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        //Get the player speaker data
        playerData = jsonHolder.getSpeaker("Player");
        //Get the portrait, name, and line objects from the UI
        while(portrait == null || speakerName == null || lineInBox == null)
        {
            for(int i = 0; i < convoHUDObject.transform.childCount; i++)
            {
                var child = convoHUDObject.transform.GetChild(i);
                switch(child.name)
                {
                    case "Portrait": portrait = child.gameObject.GetComponent<Image>(); break;
                    case "Name": speakerName = child.gameObject.GetComponent<TextMeshProUGUI>(); break;
                    case "Line": lineInBox = child.gameObject.GetComponent<TextMeshProUGUI>(); break;
                }//End switch
            }//End for
        }//End while
    }//End Start

    private void Update()
    {
        //If the HUD as a whole is active
        if(convoHUDObject.activeSelf)
        {
            //If NPC is talking
            if(!speakerName.text.Equals(playerData.speakerName))
            {
                playerSpeakingDisplay.SetActive(false);
                npcSpeakingDisplay.SetActive(true);
            }//End if
            //If it's a player choice scenario
            else
            {
                npcSpeakingDisplay.SetActive(false);
                playerSpeakingDisplay.SetActive(true);
            }//End else
        }//End if
    }//End Update
}
