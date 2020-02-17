﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private bool isInteractible, isInteracting;
    private string[] modes = {"Conversation", "Action", "Take", "Portal", "Default"};
    private string interactionMode;
    private GameObject gameManager;
    [SerializeField]
    private string displayName;
    [SerializeField]
    private string displayVerb;
    [SerializeField]
    private string id;
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
    private void Start()
    {
        //Set to not be interactive, and set the interaction mode to default
        isInteracting = false;
        //Switch out below line for tag-checking switch statement
        interactionMode = modes[0];
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }//End Start

    //Change appropriate variables for interaction mode
    private void Update()
    {
        switch(interactionMode)
        {
            case "Conversation":
                displayVerb = "Talk";
                break;
            case "Action":
                displayVerb = "Use";
                break;
            case "Take":
                displayVerb = "Take";
                break;
            case "Portal":
                displayVerb = "Open Portal";
                break;
            default:
                Debug.Log("Error: No interaction mode set for " + name + ".");
                break;
        }//End switch
    }//End Update

    #region Behaviours
    public void interact()
    {
        //If is set to not be interactible right now, don't interact
        if(!isInteractible)
        {
            Debug.Log("Interaction with " + name + " failed: isInteractible set to false.");
            return;
        }//End if
        //If is set to be interactible right now, interact
        else
        {
            switch(interactionMode)
            {
                case "Conversation":
                    gameManager.GetComponent<ConversationManager>().converse(this.gameObject);
                    break;
                case "Action":
                    gameManager.GetComponent<Action>();
                    break;
                case "Take":
                    gameManager.GetComponent<TakeItem>();
                    break;
                case "Portal":
                    gameManager.GetComponent<Portal>();
                    break;
                default:
                    Debug.Log("Error: No interaction mode set for " + name + ".");
                    break;
            }//End switch
            Debug.Log("Interaction with " + name + " successful.");
        }//End else
    }//End Interact
    #endregion
}
