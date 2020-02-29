using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueOption : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private SetLine setLine;
    private string displayLine;
    [SerializeField]
    private TextMeshProUGUI text;
    #endregion

    #region Getters & Setters
    public void setDialogueOption(SetLine setLine)
    {
        this.setLine = setLine;
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = JSONHolder.getLine(setLine.lineID).text;
    }//End Dialogue Option Setter

    public SetLine getSetLine()
    {
        return setLine;
    }//End Set Line Getter
    #endregion

    #region Behaviours
    private void Start()
    {
        StartCoroutine(TypeLine());
    }//End Start

    public void thisOptionWasChosen()
    {
        CurrentDialogue currentDialogue = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<CurrentDialogue>();
        if(!currentDialogue.getOptionChosen())
        {
            StartCoroutine(TypeLineIntoBox());
        }//End if
        currentDialogue.setOptionChosen(true);
    }//End thisOptionWasChosen

    private IEnumerator TypeLine()
    {
        displayLine = "";
        float typingSpeed = 0.01f;
        //For every letter in the line of text
        foreach (char letter in JSONHolder.getLine(setLine.lineID).text)
        {
            //Add a character from it to the displayed text
            displayLine += letter;
            switch (letter)
            {
                case ',': typingSpeed = 0.25f; break;
                case '.': typingSpeed = 0.5f; break;
                default: typingSpeed = 0.01f; break;
            }//End switch
            text.text = displayLine;
            //Wait before adding the next one
            yield return new WaitForSeconds(typingSpeed);
        }//End foreach
    }//End Type enumerator

    private IEnumerator TypeLineIntoBox()
    {
        float typingSpeed = 0.01f;
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        stringBuilder.Insert(0, displayLine);
        ConversationHUD conversationHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<ConversationHUD>();
        for (int i = 0; i < displayLine.Length; i++)
        {
            conversationHUD.setPlayerLineInBox(conversationHUD.getPlayerLineInBox().text + displayLine[i]);
            switch (displayLine[i])
            {
                case ',': typingSpeed = 0.25f; break;
                case '.': typingSpeed = 0.5f; break;
                default: typingSpeed = 0.01f; break;
            }//End switch
            stringBuilder.Replace(stringBuilder.ToString()[i], ' ', i, 1);
            text.text = stringBuilder.ToString();
            yield return new WaitForSeconds(typingSpeed);
        }//End for
        yield return new WaitForSeconds(1f);
        DialogueManager dialogueManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<DialogueManager>();
        dialogueManager.runDialogue(setLine);
    }//End UntypeLine
    #endregion
}
