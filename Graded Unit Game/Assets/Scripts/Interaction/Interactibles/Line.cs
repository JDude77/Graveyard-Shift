﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line
{
    public string lineID;

    public string text;

    public AudioClip audioClip;

    public AnimationClip animationToPlay;

    public string doBeforeLine;

    public string doAfterLine;

    public bool isPlayerLine;
}

[System.Serializable]
public struct LineList
{
    public List<Line> lines;
}