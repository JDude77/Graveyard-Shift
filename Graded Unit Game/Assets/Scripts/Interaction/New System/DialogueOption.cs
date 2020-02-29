﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueOption : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private SetLine setLine;
    private TextMeshProUGUI text;
    #endregion

    #region Getters & Setters
    public void setDialogueOption(SetLine setLine)
    {
        JSONHolder jsonHolder = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        this.setLine = setLine;
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = jsonHolder.getLine(setLine.lineID).text;
    }//End Dialogue Option Setter

    public SetLine getSetLine()
    {
        return setLine;
    }//End Set Line Getter
    #endregion

    #region Behaviours
    public void thisOptionWasChosen()
    {
        DialogueManager dialogueManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<DialogueManager>();
        dialogueManager.runDialogue(setLine);
    }//End thisOptionWasChosen
    #endregion
}
