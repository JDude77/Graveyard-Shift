using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line
{
    public string lineID;
    public string setID;

    public string speakerID;

    public string text;

    AudioClip audioClip;

    Animation animationToPlay;
}

[System.Serializable]
public struct LineList
{
    public List<Line> lines;
}