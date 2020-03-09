﻿using UnityEngine;

public class Interactive : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    protected bool isInteractible, isInteracting;
    protected readonly string[] modes = {"Conversation", "Action", "Take", "Portal", "Default"};
    protected string interactionMode;
    [SerializeField]
    protected GameObject gameManager;
    [SerializeField]
    protected string displayName;
    [SerializeField]
    protected string displayVerb;
    [SerializeField]
    protected string id;
    [SerializeField]
    protected GameObject conversationHUDGameObject;
    #endregion

    #region Getters & Setters
    //isInteractible Getter
    public bool getIsInteractible()
    {
        return isInteractible;
    }//End isInteractible Getter
    //isInteractible Setter
    public void setIsInteractible(bool isInteractible)
    {
        this.isInteractible = isInteractible;
    }//End isInteractible Setter
    //isInteracting Getter
    public bool getIsInteracting()
    {
        return isInteracting;
    }//End isInteracting Getter
    //displayName Getter
    public string getDisplayName()
    {
        return displayName;
    }//End displayName getter
    //displayVerb getter
    public string getDisplayVerb()
    {
        return displayVerb;
    }//End displayVerb getter
    //id getter
    public string getID()
    {
        return id;
    }//End id getter
    #endregion

    //Start is called before the first frame update
    protected void Start()
    {
        //Set to not be interactive, and set the interaction mode to default
        isInteracting = false;
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        conversationHUDGameObject = GameObject.FindGameObjectWithTag("Conversation HUD");
    }//End Start

    #region Behaviours
    public void changeInteractionMode(string mode)
    {
        switch (mode.ToLower())
        {
            case "conversation":
                interactionMode = modes[0];
                displayVerb = "Talk";
                break;
            case "action":
                interactionMode = modes[1];
                break;
            case "take":
                interactionMode = modes[2];
                displayVerb = "Take";
                break;
            case "portal":
                interactionMode = modes[3];
                displayVerb = "Enter Portal";
                break;
            default:
                Debug.Log("Error: No interaction mode set for " + name + ".");
                break;
        }//End switch
    }//End changeInteractionMode

    public virtual void interact()
    {
        //If is set to not be interactible right now, don't interact
        if(!isInteractible)
        {
            Debug.Log("Interaction with " + name + " failed: isInteractible set to false.");
            return;
        }//End if
        //Set the object to have been interacted with at least once
        GameState.currentGameState.interactedWithAtLeastOnce.TryGetValue(id, out bool interactedOnce);
        if(!interactedOnce)
        {
            GameState.currentGameState.updateGameState(id, "interacted");
        }//End if
    }//End Interact
    #endregion
}
