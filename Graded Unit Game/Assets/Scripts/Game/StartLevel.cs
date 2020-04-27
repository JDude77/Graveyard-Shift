using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    float count = 2;
    bool done = false;

    // Update is called once per frame
    void Update()
    {
        if(count > 0 && !done)
        {
            count -= Time.deltaTime;
        }
        else if(count <= 0 && !done)
        {
            GameObject.FindGameObjectWithTag("Conversation Partner").GetComponent<ConversationPartner>().interact();
            GameObject.FindGameObjectWithTag("HUD").GetComponent<UIManager>().setHUD("Conversation");
            done = true;
        }
    }
}
