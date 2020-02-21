using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Conversation
{
    public string conversationID;

    public string speakerID;

    public string[] setIDs;
}

[System.Serializable]
public struct ConversationList
{
    public List<Conversation> conversations;
}