using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour, ILine
{
    public string lineID { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string setID { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string speakerID { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string text { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    AudioClip ILine.audio { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    Animation ILine.animation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
