using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    #region Attributes
    private GameObject player, npc;
    private int gameState;
    private TextAsset[] JSONData;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }//End Start

    #region Behaviours

    public void converse(GameObject npc)
    {
        this.npc = npc;
        Debug.Log("Starting conversation with " + this.npc.name);
        JSONData = getConversationData();
        //Find the correct data sets
        //Turn the JSONs into their object things
        //Have the actual conversation
        Debug.Log("Ending conversation with " + this.npc.name);
        player.GetComponent<PlayerInteraction>().setIsInteracting(false);
    }

    public TextAsset[] getConversationData()
    {
        Debug.Log("Trying to retreive conversation JSON data...");

        TextAsset conversationJSON = getJSON("Conversations");
        TextAsset setJSON = getJSON("Sets");
        TextAsset lineJSON = getJSON("Lines");
        TextAsset speakersJSON = getJSON("Speakers");

        Debug.Log("Conversation data retrieved.");

        TextAsset[] JSONs = {conversationJSON, setJSON, lineJSON, speakersJSON};
        return JSONs;
    }

    public TextAsset getJSON(string JSONToGet)
    {
        if(Resources.Load<TextAsset>("JSON/" + JSONToGet) != null)
        {
            Debug.Log(JSONToGet + " JSON file retrieved.");
            return Resources.Load<TextAsset>("JSON/" + JSONToGet);
        }
        else
        {
            Debug.LogError(JSONToGet + " JSON file retreival failed.");
            return null;
        }
    }
    #endregion
}
