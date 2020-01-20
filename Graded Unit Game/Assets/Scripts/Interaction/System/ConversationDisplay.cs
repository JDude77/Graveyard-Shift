using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Control the display of the conversation
public class ConversationDisplay : MonoBehaviour
{
    #region Attributes
    private Camera gameCamera;
    private Canvas conversationCanvas;
    private HaveConversation conversationUpdater;
    private enum conversationStates
    {
        inactive,
        otherIsTalking,
        playerIsTalking
    }//End conversationStates
    private int state = (int) conversationStates.inactive;
    #endregion

    //Start is called before the first frame update
    private void Start()
    {
        gameCamera = Camera.current;
        conversationCanvas = gameObject.GetComponent<Canvas>();
    }//End Start

    // Update is called once per frame
    private void Update()
    {
        switch(state)
        {
            case (int) conversationStates.otherIsTalking:
                displayOtherLine();
                break;

            case (int) conversationStates.playerIsTalking:
                displayPlayerLines();
                break;

            case (int) conversationStates.inactive:
                break;
        }//End switch
    }//End Update

    private void displayPlayerLines()
    {
        throw new NotImplementedException();
    }

    private void displayOtherLine()
    {
        throw new NotImplementedException();
    }
}
