using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeaker
{
    string speakerID { get; set; }
    Sprite portrait { get; set; }
    AudioClip voice { get; set; }
    string speakerName { get; set; }
    bool isNameKnown { get; set; }
}

public interface IItem
{
    string itemID { get; set; }
}

public interface IEnvironment
{
    string environmentID { get; set; }
}

public interface ILine
{
    string lineID { get; set; }
    string setID { get; set; }
    string speakerID { get; set; }
    string text { get; set; }
    AudioClip audio { get; set; }
    Animation animation { get; set; }
}

public interface IConversation
{
    string conversationID { get; set; }
    ISet[] sets { get; set; }
}

public interface ISet
{
    string setID { get; set; }
    ILine[] lines { get; set; }
}