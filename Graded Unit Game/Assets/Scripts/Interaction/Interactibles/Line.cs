﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line
{
    public string lineID;

    public string text;

    public string audioClipID;

    public AudioClip audioClip;

    public string animationToPlayID;

    public AnimationClip animationToPlay;

    public string doBeforeLine;

    public string doAfterLine;
}

[System.Serializable]
public struct LineList
{
    public List<Line> lines;
}