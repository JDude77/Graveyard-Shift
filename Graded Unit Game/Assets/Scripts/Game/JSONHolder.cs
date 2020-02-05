using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONUtilityExtended;

public class JSONHolder : MonoBehaviour
{
    private TextAsset[] jsonData;
    [SerializeField]
    private Dictionary<GameStateShell, Conversation> conversations;
    [SerializeField]
    private Dictionary<string, Set> sets;
    [SerializeField]
    private Dictionary<string, Line> lines;
    [SerializeField]
    private Dictionary<string, SpeakingNPC> speakers;
    private JSONUtility jsonFunctions;

    private void Awake()
    {
        jsonFunctions = new JSONUtility();
        jsonData = jsonFunctions.getConversationData();
        conversations = jsonFunctions.getConversations(jsonData[0]);
        sets = jsonFunctions.getSets(jsonData[1]);
        lines = jsonFunctions.getLines(jsonData[2]);
        speakers = jsonFunctions.getSpeakers(jsonData[3]);
    }//End Awake

    #region Getters
    //Conversation Dictionary Getter
    public Dictionary<GameStateShell, Conversation> getConversations()
    {
        return conversations;
    }//End Conversation getter

    //Set Dictionary Getter
    public Dictionary<string, Set> getSets()
    {
        return sets;
    }//End Sets getter

    //Line Dictionary Getter
    public Dictionary<string, Line> getLines()
    {
        return lines;
    }//End Lines getter

    //Speaker Dictionary Getter
    public Dictionary<string, SpeakingNPC> getSpeakers()
    {
        return speakers;
    }//End Speakers getter
    #endregion

    #region Behaviours
    //Find and return a specific conversation
    public Conversation getConversation(string conversationID)
    {
        int index = 0;
        Conversation[] convosToSearch = new Conversation[conversations.Count];
        conversations.Values.CopyTo(convosToSearch, index);
        //Check if the conversation ID exists anywhere in the dictionary
        while(index < convosToSearch.Length)
        {
            if(convosToSearch[index].conversationID.Equals(conversationID))
            {
                return convosToSearch[index];
            }//End if
            else
            {
                index++;
            }//End else
        }//End found
        Debug.LogError("Conversation with ID " + conversationID + " not found.");
        return null;
    }//End Conversation Getter

    //Find and return a specific set
    public Set getSet(string setID)
    {
        int index = 0;
        Set[] setsToSearch = new Set[sets.Count];
        sets.Values.CopyTo(setsToSearch, index);
        //Check if the set ID exists anywhere in the dictionary
        while(index < setsToSearch.Length)
        {
            if(setsToSearch[index].setID.Equals(setID))
            {
                return setsToSearch[index];
            }//End if
            else
            {
                index++;
            }//End else
        }//End while
        Debug.LogError("Set with ID " + setID + " not found.");
        return null;
    }//End Set Getter

    //Find and return a specific line
    public Line getLine(string lineID)
    {
        int index = 0;
        Line[] linesToSearch = new Line[lines.Count];
        lines.Values.CopyTo(linesToSearch, index);
        //Check if the line ID exists anywhere in the dictionary
        while(index < linesToSearch.Length)
        {
            if (linesToSearch[index].lineID.Equals(lineID))
            {
                return linesToSearch[index];
            }//End if
            else
            {
                index++;
            }//End else
        }//End while
        Debug.LogError("Line with ID " + lineID + " not found.");
        return null;
    }//End Line Getter

    //Find and return a specific speaker
    public SpeakingNPC getSpeaker(string speakerID)
    {
        int index = 0;
        SpeakingNPC[] speakingNPCsToSearch = new SpeakingNPC[speakers.Count];
        speakers.Values.CopyTo(speakingNPCsToSearch, index);
        //Check if the speaker ID exists anywhere in the dictionary
        while(index < speakingNPCsToSearch.Length)
        {
            if(speakingNPCsToSearch[index].speakerID.Equals(speakerID))
            {
                return speakingNPCsToSearch[index];
            }//End if
            else
            {
                index++;
            }//End else
        }//End while
        Debug.LogError("Speaker with ID " + speakerID + " not found.");
        return null;
    }//End SpeakingNPC Getter
    #endregion
}