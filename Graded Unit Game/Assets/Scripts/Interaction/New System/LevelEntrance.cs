using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntrance : Interactive
{
    [SerializeField]
    private GameObject portalPrefab;

    private new void Start()
    {
        portalPrefab = (GameObject) Resources.Load("Prefabs/Portal");
        base.Start();
        interactionMode = modes[3];
        displayVerb = "Open Portal";
    }//End Start

    private void Update()
    {
        if (GameState.currentGameState.levelIsUnlocked[id])
        {
            isInteractible = true;
        }//End if
        else
        {
            isInteractible = false;
        }//End else
    }//End Update

    public override void interact()
    {
        base.interact();
        
    }//End Interact
}