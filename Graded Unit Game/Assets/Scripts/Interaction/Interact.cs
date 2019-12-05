using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    #region Attributes
    private bool isInteractible;
    private string[] modes = {"Conversation", "Action", "Take", "Portal", "Default"};
    private string interactionMode;
    private GameObject gameManager;
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
    #endregion

    //Start is called before the first frame update
    private void Start()
    {
        //Set to not be interactive, and set the interaction mode to default
        isInteractible = false;
        interactionMode = modes[0];
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }//End Start

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
                    gameManager.GetComponent<Conversation>().converse(this.gameObject);
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
