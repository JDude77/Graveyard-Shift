using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Set
{
    public string setID;

    public SetLine[] setLines;

    public string speaker;
}

[System.Serializable]
public struct SetList
{
    public List<Set> sets;
}
