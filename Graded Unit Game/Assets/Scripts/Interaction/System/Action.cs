using UnityEngine;

public class Action : Interactive
{
    private new void Start()
    {
        base.Start();
    }//End Start

    private new void interact()
    {
        base.interact();
        Debug.Log("Generic action.");
        gameHandler.changePlayerInteractionState(false);
    }//End interact
}
