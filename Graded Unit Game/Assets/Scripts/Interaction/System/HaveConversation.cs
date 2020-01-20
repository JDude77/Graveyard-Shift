using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONUtilityExtended;

//Control the functionality of having the conversation
public class HaveConversation : MonoBehaviour
{
    #region Attributes
    private GameObject player, npc, conversationDisplay;
    private int gameState;
    private TextAsset[] JSONData;
    private JSONUtility jsonUtility;
    #endregion

    private void Start()
    {
        jsonUtility = new JSONUtility();
        player = GameObject.FindGameObjectWithTag("Player");
    }//End Start

    #region Behaviours

    public void converse(GameObject npc)
    {
        this.npc = npc;
        Debug.Log("Starting conversation with " + this.npc.name);
        conversationDisplay = GameObject.Find("Conversation UI");
        Debug.Log("Ending conversation with " + this.npc.name);
        player.GetComponent<PlayerInteraction>().setIsInteracting(false);
    }
    #endregion
}
