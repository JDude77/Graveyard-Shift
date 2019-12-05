using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    #region Attributes
    private bool isInteractible;
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
        isInteractible = false;
    }//End Start

    //Update is called once per frame
    private void Update()
    {
        
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

        }//End else
    }//End Interact
    #endregion
}
