using System.Collections.Generic;
using UnityEngine;
using JSONUtilityExtended;

public static class JSONHolder
{
    private static TextAsset[] jsonData;
    [SerializeField]
    private static Dictionary<GameStateShell, Conversation> conversations;
    [SerializeField]
    private static Dictionary<string, Set> sets;
    [SerializeField]
    private static Dictionary<string, Line> lines;
    [SerializeField]
    private static Dictionary<string, SpeakingNPC> speakers;
    private static JSONUtility jsonFunctions;

    private static void Awake()
    {
        jsonFunctions = new JSONUtility();
        jsonData = jsonFunctions.getConversationData();
        conversations = jsonFunctions.getConversations(jsonData[0]);
        sets = jsonFunctions.getSets(jsonData[1]);
        lines = jsonFunctions.getLines(jsonData[2]);
        speakers = jsonFunctions.getSpeakers(jsonData[3]);
    }//End Awake

    static JSONHolder()
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
    public static Dictionary<GameStateShell, Conversation> getConversations()
    {
        return conversations;
    }//End Conversation getter

    //Set Dictionary Getter
    public static Dictionary<string, Set> getSets()
    {
        return sets;
    }//End Sets getter

    //Line Dictionary Getter
    public static Dictionary<string, Line> getLines()
    {
        return lines;
    }//End Lines getter

    //Speaker Dictionary Getter
    public static Dictionary<string, SpeakingNPC> getSpeakers()
    {
        return speakers;
    }//End Speakers getter
    #endregion

    #region Behaviours
    //Find and return a specific conversation
    public static Conversation getConversation(string conversationID)
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

    //Get a line held in a specific set
    public static Line getLineFromSet(int indexInSetLines, Set set)
    {
        //Get the line from a specific index in a set
        if (getLine(set.setLines[indexInSetLines].lineID) != null)
        {
            return getLine(set.setLines[indexInSetLines].lineID);
        }//End if
        else
        {
            return null;
        }//End else
    }//End Line From Set Getter

    //Get a set held in a specific conversation
    public static Set getSetFromConversation(int indexInSetIDs, Conversation convo)
    {
        //Get the set from a specific index in a conversation
        if (indexInSetIDs < convo.setIDs.Length)
        {
            return getSet(convo.setIDs[indexInSetIDs]);
        }//End if
        else
        {
            return null;
        }//End else
    }//End Set From Conversation Getter

    public static Set getSetFromConversation(string nextSet, Conversation convo)
    {
        //Get the set using a specific ID pointer, checking it's in the given conversation
        int index = 0;
        while(index < convo.setIDs.Length)
        {
            if (convo.setIDs[index].Equals(nextSet))
            {
                return getSet(nextSet);
            }//End if
            else
            {
                index++;
            }//End else
        }//End while
        Debug.LogError("Set ID " + nextSet + " not found in Conversation " + convo.conversationID + ".");
        return null;
    }//End Set From Conversation Getter

    //Find and return a specific set
    public static Set getSet(string setID)
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
    public static Line getLine(string lineID)
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
    public static SpeakingNPC getSpeaker(string speakerID)
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

    //Find a conversation with the given game state
    public static Conversation findConversation(SpeakingNPC npc, GameStateShell gameState)
    {
        Conversation result = null;
        List<Conversation> convosToSearch = new List<Conversation>();
        foreach (Conversation conversation in conversations.Values)
        {
            result = conversation;
            break;
            if (conversation.speakerID.Equals(npc.speakerID))
            {
                convosToSearch.Add(conversation);
            }//End if
        }//End foreach
        bool spoken;
        gameState.interactedWithAtLeastOnce.TryGetValue(npc.speakerID, out spoken);
        if (!spoken)
        {

        }//End if
        return result;
    }//End findConversationWithGameState
    #endregion
}