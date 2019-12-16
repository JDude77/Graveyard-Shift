using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState _instance;

    public static GameState Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }//End if
        else
        {
            _instance = this;
        }//End else
    }//End Awake

    public void updateGameState()
    {

    }//End updateGameState
}
