using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeakingNPC
{
    public string speakerID;

    public string portraitID;

    public Sprite portrait;

    public string voiceID;

    public AudioClip voice;

    public bool isNameKnown;

    public string speakerName;
}

[System.Serializable]
public struct SpeakingNPCList
{
    public List<SpeakingNPC> speakers;
}
