using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveConversation : MonoBehaviour
{
    #region Attributes
    private GameObject player, npc;
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
        //Turn the JSONs into their object things
        //Have the actual conversation
        Debug.Log("Ending conversation with " + this.npc.name);
        player.GetComponent<PlayerInteraction>().setIsInteracting(false);
    }
    #endregion
}
