using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Getting the JSON data stored in a list through a struct
    [System.Serializable]
    public struct TheMainThing
    {
        public List<MyJsonData> entries;
    }

    //The struct that holds the bits of the JSON data
    [System.Serializable]
    public struct MyJsonData
    {
        public int theNumber;
        public string theString;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Load text from a JSON file (Assets/Resources/JSON/WorkingTestJSON.json)
        TextAsset jsonTextFile = Resources.Load<TextAsset>("JSON/WorkingTestJSON");

        //Then use JsonUtility.FromJson<T>() to deserialize jsonTextFile into an object
        TheMainThing newData = JsonUtility.FromJson<TheMainThing>(jsonTextFile.text);
        Debug.Log("Count = " + newData.entries.Count);
        foreach (MyJsonData item in newData.entries)
        {
            Debug.Log("Item = {" + item.theString + ", " + item.theNumber + "}");
        }
        Debug.Log("Hi");

    }
}
