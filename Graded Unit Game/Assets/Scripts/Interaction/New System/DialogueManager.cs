using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Attributes
    //What kind of dialogue set is currently being dealt with?
    private string dialogueType;
    //What conversation is being had at the moment (if any)?
    private Conversation currentConversation;
    //What set is being dealt with in the current conversation (if any)?
    private Set currentSet;
    //What what lines are there within the current set?
    private List<Line> currentLines;
    #endregion

    #region Behaviours
    private void initialiseConversationData()
    {
        JSONHolder jSON = new JSONHolder();
        currentConversation = jSON.getConversation("TestConversation1");
        currentSet = jSON.getSetFromConversation(0, currentConversation);
        getLinesFromCurrentSet();
        getDialogueType();
    }//End initialiseConversationData
    #endregion

    private void getDialogueType()
    {
        if (currentSet.pickOne)
        {
            dialogueType = "random";
        }//End if
        else if (currentSet.playerChoice)
        {
            dialogueType = "choices";
        }//End else if
        else
        {
            dialogueType = "sequence";
        }//End else
    }//End getDialogueType

    private void getLinesFromCurrentSet()
    {
        currentLines.Clear();
        JSONHolder jSON = new JSONHolder();
        for (int i = 0; i < currentSet.lineIDs.Length; i++)
        {
            currentLines.Add(jSON.getLineFromSet(i, currentSet));
        }//End for
    }//End getLinesFromCurrentSet
}
