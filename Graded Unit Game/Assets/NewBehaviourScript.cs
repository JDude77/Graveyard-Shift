using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [System.Serializable]
    public struct TheMainThing
    {
        public List<MyJsonData> entries;
    }

    [System.Serializable]
    public struct MyJsonData
    {
        public int theNumber;
        public string theString;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Load text from a JSON file (Assets/Resources/Text/myData.json)
        TextAsset jsonTextFile = Resources.Load<TextAsset>("JSON/dialog");

        //Then use JsonUtility.FromJson<T>() to deserialize jsonTextFile into an object
        TheMainThing newData = JsonUtility.FromJson<TheMainThing>(jsonTextFile.text);
        Debug.Log("Count = " + newData.entries.Count);
        foreach (MyJsonData item in newData.entries)
        {
            Debug.Log("Item = {" + item.theString + ", " + item.theNumber + "}");
        }
        Debug.Log("Hi");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
