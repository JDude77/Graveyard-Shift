using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Set
{
    public string setID;
    public string[] lineIDs;
}

[System.Serializable]
public struct SetList
{
    public List<Set> sets;
}
