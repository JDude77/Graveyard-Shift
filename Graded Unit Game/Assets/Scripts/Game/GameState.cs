using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private void Awake()
    {
        //Make sure there is only one game manager
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Game Manager");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }//End if

        //Don't destroy the state tracker between 
        DontDestroyOnLoad(this.gameObject);
    }//End awake
}
