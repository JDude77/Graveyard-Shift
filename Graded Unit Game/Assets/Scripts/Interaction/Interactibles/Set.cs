using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Set
{
    public string setID;

    public string[] lineIDs;

    public bool pickOne;
}

[System.Serializable]
public struct SetList
{
    public List<Set> sets;
}
