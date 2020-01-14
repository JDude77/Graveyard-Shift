using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Conversation
{
    public string conversationID;
    public Set[] sets;
}

[System.Serializable]
public struct ConversationList
{
    public List<Conversation> conversations;
}