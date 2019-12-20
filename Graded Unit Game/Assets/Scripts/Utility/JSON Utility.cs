using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONUtility
{
    public TextAsset[] getConversationData()
    {
        Debug.Log("Trying to retreive conversation JSON data...");

        TextAsset conversationJSON = getJSON("Conversations");
        TextAsset setJSON = getJSON("Sets");
        TextAsset lineJSON = getJSON("Lines");
        TextAsset speakersJSON = getJSON("Speakers");

        Debug.Log("Conversation data retrieved.");

        TextAsset[] JSONs = { conversationJSON, setJSON, lineJSON, speakersJSON };
        return JSONs;
    }

    public TextAsset getJSON(string JSONToGet)
    {
        if (Resources.Load<TextAsset>("JSON/" + JSONToGet) != null)
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
}