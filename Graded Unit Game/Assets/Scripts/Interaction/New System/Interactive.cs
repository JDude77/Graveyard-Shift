using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
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
        switch(gameObject.tag.ToString())
        {
            case "Conversation Partner":
                interactionMode = modes[0];
                break;
        }//End switch
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
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
                displayVerb = "Use";
                break;
            case "take":
                interactionMode = modes[2];
                displayVerb = "Take";
                break;
            case "portal":
                interactionMode = modes[3];
                displayVerb = "Open Portal";
                break;
            default:
                Debug.Log("Error: No interaction mode set for " + name + ".");
                break;
        }//End switch
    }//End changeInteractionMode

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
<<<<<<< Updated upstream:Graded Unit Game/Assets/Scripts/Interaction/System/Interact.cs
                    gameManager.GetComponent<HaveConversation>().converse(this.gameObject);
=======
                    //gameManager.GetComponent<ConversationManager>().converse(this.gameObject);
>>>>>>> Stashed changes:Graded Unit Game/Assets/Scripts/Interaction/New System/Interactive.cs
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
