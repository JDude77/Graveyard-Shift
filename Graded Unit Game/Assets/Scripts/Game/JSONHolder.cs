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
    public Dictionary<GameStateShell, Conversation> getConversations()
    {
        return conversations;
    }//End Conversation getter

    public Dictionary<string, Set> getSets()
    {
        return sets;
    }//End Sets getter

    public Dictionary<string, Line> getLines()
    {
        return lines;
    }//End Lines getter

    public Dictionary<string, SpeakingNPC> getSpeakers()
    {
        return speakers;
    }//End Speakers getter
    #endregion
}
