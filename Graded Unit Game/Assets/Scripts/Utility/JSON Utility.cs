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
    
    public Dictionary<string, Conversation> getConversations(TextAsset conversationData)
    {
        Debug.Log("Trying to convert conversation JSON data to conversation objects...");

        //Checking if the data isn't blank and getting the data if it is
        if (conversationData.ToString().Equals(""))
        {
            Debug.LogWarning("Conversation Data passed in was empty. This failsafe caught that, and will now attempt to fill it.");
            if (Resources.Load<TextAsset>("JSON/Conversations") != null)
            {
                Debug.Log("Conversation Data failsafe success.");
                conversationData = Resources.Load<TextAsset>("JSON/Conversations");
            }//End if
            else
            {
                Debug.LogError("Conversation Data failsafe failed - JSON file not found.");
                return null;
            }//End else
        }//End if

        ConversationList conversationList = JsonUtility.FromJson<ConversationList>(conversationData.text);
        Dictionary<string, Conversation> convos = new Dictionary<string, Conversation>();

        int index = 0;
        foreach(Conversation c in conversationList.conversations)
        {
            Debug.Log("Coversation Found! ID: " + c.conversationID);
            convos.Add(c.conversationID, c);
            index++;
            Debug.Log("Conversation Added To Conversation Dictionary (" + index + " of " + conversationList.conversations.Count + ")");
        }//End foreach

        return convos;
    }

    public Dictionary<string, Set> getSets(TextAsset setData)
    {
        Debug.Log("Trying convert set JSON data to set objects...");

        //Checking if the data isn't blank and getting the data if it is
        if (setData.ToString().Equals(""))
        {
            Debug.LogWarning("Set Data passed in was empty. This failsafe caught that, and will now attempt to fill it.");
            if (Resources.Load<TextAsset>("JSON/Sets") != null)
            {
                Debug.Log("Set Data failsafe success.");
                setData = Resources.Load<TextAsset>("JSON/Sets");
            }//End if
            else
            {
                Debug.LogError("Set Data failsafe failed - JSON file not found.");
                return null;
            }//End else
        }//End if

        SetList setList = JsonUtility.FromJson<SetList>(setData.text);
        Dictionary<string, Set> sets = new Dictionary<string, Set>();

        int index = 0;
        foreach(Set s in setList.sets)
        {
            Debug.Log("Set Found! ID: " + s.setID);
            sets.Add(s.setID, s);
            index++;
            Debug.Log("Set Added To Set Dictionary (" + index + " of " + setList.sets.Count + ")");
        }//End foreach

        return sets;
    }

    public Dictionary<string, Line> getLines(TextAsset lineData)
    {
        Debug.Log("Trying to convert line JSON data to line objects...");

        //Checking if the data isn't blank and getting the data if it is
        if (lineData.ToString().Equals(""))
        {
            Debug.LogWarning("Line Data passed in was empty. This failsafe caught that, and will now attempt to fill it.");
            if (Resources.Load<TextAsset>("JSON/Lines") != null)
            {
                Debug.Log("Line Data failsafe success.");
                lineData = Resources.Load<TextAsset>("JSON/Lines");
            }//End if
            else
            {
                Debug.LogError("Line Data failsafe failed - JSON file not found.");
                return null;
            }//End else
        }//End if

        LineList lineList = JsonUtility.FromJson<LineList>(lineData.text);
        Dictionary<string, Line> lines = new Dictionary<string, Line>();

        int index = 0;
        foreach(Line l in lineList.lines)
        {
            Debug.Log("Line Found! ID: " + l.lineID);
            lines.Add(l.lineID, l);
            index++;
            Debug.Log("Line Added To Line Dictionary (" + index + " of " + lineList.lines.Count + ")");
        }//End foreach

        return lines;
    }

    public Dictionary<string, SpeakingNPC> getSpeakers(TextAsset speakerData)
    {
        Debug.Log("Trying to convert speaker JSON data to speaking NPC objects...");

        //Checking if the data isn't blank and getting the data if it is
        if (speakerData.ToString().Equals(""))
        {
            Debug.LogWarning("Speaker Data passed in was empty. This failsafe caught that, and will now attempt to fill it.");
            if (Resources.Load<TextAsset>("JSON/Speakers") != null)
            {
                Debug.Log("Speaker Data failsafe success.");
                speakerData = Resources.Load<TextAsset>("JSON/Speakers");
            }//End if
            else
            {
                Debug.LogError("Speaker Data failsafe failed - JSON file not found.");
                return null;
            }//End else
        }//End if

        SpeakingNPCList speakerList = JsonUtility.FromJson<SpeakingNPCList>(speakerData.text);
        Dictionary<string, SpeakingNPC> speakers = new Dictionary<string, SpeakingNPC>();

        int index = 0;
        foreach(SpeakingNPC s in speakerList.speakers)
        {
            Debug.Log("Speaking NPC Found! ID: " + s.speakerID);
            speakers.Add(s.speakerID, s);
            index++;
            Debug.Log("Speaking NPC Added To Speaking NPC Dictionary (" + index + " of " + speakerList.speakers.Count + ")");
        }//End foreach

        return speakers;
    }
}