using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Set
{
    public string setID;

    public SetLink[] setLinks;

    public IEnumerator speaker;
}

[System.Serializable]
public struct SetList
{
    public List<Set> sets;
}
