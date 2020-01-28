using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    #region Attributes
    private GameObject player;
    private Camera playerCam;
    [SerializeField]
    private GameObject other;
    private float interactRange;
    private int interactLayer;
    [SerializeField]
    private Material interactibleMaterial;
    [SerializeField]
    private Material nonInteractibleMaterial;
    [SerializeField]
    private Material meshMaterial;
    private bool interactButtonDown;
    private bool isInteracting;
    private bool otherIsInteractible;
    private ExplorationHUD HUDHandler;
    #endregion

    #region Getters & Setters
    //Player Getter
    public GameObject getPlayer()
    {
        return player;
    }//End player getter
    //Player Setter
    public void setPlayer(GameObject player)
    {
        this.player = player;
    }//End player setter
    
    //Other Interactible Getter
    public GameObject getOther()
    {
        return other;
    }//End other interactible getter
    //Other Interactible Setter
    public void setOther(GameObject other)
    {
        this.other = other;
    }//End other interactible setter

    //Interaction Range Getter
    public float getInteractRange()
    {
        return interactRange;
    }//End interaction range getter
    //Interaction Range Setter
    public void setInteractRange(float interactRange)
    {
        this.interactRange = interactRange;
    }//End interaction range setter

    //Interaction Layer Getter
    public int getInteractionLayer()
    {
        return interactLayer;
    }//End interaction layer getter
    //Interaction Layer Setter
    public void setInteractionLayer(int interactLayer)
    {
        this.interactLayer = interactLayer;
    }//End interaction layer setter

    //Is Interacting Getter
    public bool getIsInteracting()
    {
        return isInteracting;
    }//End Is Interacting Getter
    //Is Interacting Setter
    public void setIsInteracting(bool isInteracting)
    {
        this.isInteracting = isInteracting;
    }//End Is Interacting Setter
    #endregion

    //Initialise variables on player start
    private void Start()
    {
        //Set the player to whatever is currently set as the player
        player = GameObject.FindGameObjectWithTag("Player");
        //Find camera
        playerCam = FindObjectOfType<Camera>();
        //Set the interaction range to a test value of ten
        interactRange = 1.5f;
        //Set the interaction layer to... the interaction layer
        interactLayer = LayerMask.GetMask("Interactible");
        //Set the HUD Handler to the correct object
        HUDHandler = GameObject.FindGameObjectWithTag("HUD").GetComponent<ExplorationHUD>();
    }//End Start

    //Check for interaction
    private void Update()
    {
        //Send out raycast in direction of player forward
        Ray interact = new Ray(playerCam.transform.position, playerCam.transform.forward);
        Debug.DrawRay(interact.origin, interact.direction * interactRange, Color.red);
        //If the raycast hits an interactible
        if (Physics.Raycast(interact, out RaycastHit hitInfo, interactRange, interactLayer))
        {
            //Set other to the object being hit
            other = hitInfo.transform.gameObject;
            //Activate interaction hit glow
            GameObject child = other.transform.GetChild(0).gameObject;
            child.SetActive(true);
            HUDHandler.setNameText(other.GetComponent<Interact>().getDisplayName());
            HUDHandler.setVerbText(other.GetComponent<Interact>().getDisplayVerb());
            HUDHandler.setHovering(true, other.GetComponent<Interact>().getIsInteractible());
            //If the interaction button is used
            if (Input.GetAxisRaw("Interact") != 0)
            {
                //If the button wasn't pressed last frame
                if (interactButtonDown == false)
                {
                    Debug.Log("Interaction with " + other.name);
                    //Run interaction function
                    if (other.GetComponent<Interact>() != null)
                    {
                        Debug.Log("Interaction script found.");
                        otherIsInteractible = other.GetComponent<Interact>().getIsInteractible();
                    }//End if
                    else
                    {
                        Debug.LogWarning("Interaction script not found.");
                        otherIsInteractible = false;
                    }//End else
                    if(otherIsInteractible)
                    {
                        Debug.Log("Interaction able to start.");
                        isInteracting = true;
                        player.GetComponentInChildren<MouseLook>().setSwivel(true);
                        other.GetComponent<Interact>().interact();
                    }//End if
                    else
                    {
                        Debug.LogError("Interaction didn't happen: isInteractible is false.");
                        player.GetComponentInChildren<MouseLook>().setSwivel(false);
                    }//End else
                    //Set interaction button in use to true
                    interactButtonDown = true;
                }//End if
            }//End if
            //If the interaction button is not being used
            if (Input.GetAxisRaw("Interact") == 0)
            {
                //Set interaction button in use to false
                interactButtonDown = false;
            }//End if
        }//End if
        //If the raycast doesn't hit an interactible
        else
        {
            //Deactivate the interaction text
            HUDHandler.setHovering(false, false);
            HUDHandler.setNameText("");
            HUDHandler.setVerbText("");
            //If other is not already set to null
            if(other != null)
            {
                //Turn off interaction glow
                GameObject child = other.transform.GetChild(0).gameObject;
                Transform[] children = child.GetComponentsInChildren<Transform>();
                child.SetActive(false);
                //Reset other to be null
                other = null;
            }//End if
        }//End else
    }//End Update
}