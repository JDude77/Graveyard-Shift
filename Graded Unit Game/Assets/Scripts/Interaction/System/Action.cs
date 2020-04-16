using UnityEngine;

public class Action : Interactive
{
    public override void interact()
    {
        base.interact();
        Debug.Log("Generic action.");
        gameHandler.changePlayerInteractionState(false);
    }//End interact
}
