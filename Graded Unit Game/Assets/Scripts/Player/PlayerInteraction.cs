using System;
using UnityEngine;

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
    private bool interactButtonDown;
    private bool isInteracting;
    private bool otherIsInteractible;
    private ExplorationHUD HUDHandler;
    private bool onHitOnce;
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
        //Set player voice
        GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<AudioSource>().clip = JSONHolder.getSpeaker("Player").voice;
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
            if (!onHitOnce)
            {
                nonInteractibleMaterial = other.GetComponentInChildren<MeshRenderer>().material;
                onHitOnce = true;
            }//End if
            if (!isInteracting && other.GetComponent<Interactive>().getIsInteractible())
            {
                //Activate interaction hit glow
                foreach(MeshRenderer renderer in other.transform.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.material = interactibleMaterial;
                }//End foreach
                setNameTextForHUD();
                HUDHandler.setVerbText(other.GetComponent<Interactive>().getDisplayVerb());
                HUDHandler.setHovering(true, other.GetComponent<Interactive>().getIsInteractible());
                //If the interaction button is used
                if (Input.GetAxisRaw("Interact") != 0)
                {
                    //If the button wasn't pressed last frame
                    if (interactButtonDown == false)
                    {
                        Debug.Log("Interaction with " + other.name);
                        
                        //Check that the interactive thing has an Interactive script attached to it
                        if (other.GetComponent<Interactive>() != null)
                        {
                            Debug.Log("Interaction script found.");
                            otherIsInteractible = other.GetComponent<Interactive>().getIsInteractible();
                        }//End if
                        else
                        {
                            Debug.LogWarning("Interaction script not found.");
                            otherIsInteractible = false;
                        }//End else

                        //If the interactive thing is set to currently be interacted with
                        if (otherIsInteractible)
                        {
                            Debug.Log("Interaction able to start.");
                            isInteracting = true;
                            //Make the camera face the interactive object
                            player.GetComponentInChildren<MouseLook>().setSwivel(true);
                            //Start the interaction
                            other.GetComponent<Interactive>().interact();
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
            //If the player is interacting
            else
            {
                //Deactivate interaction hit glow
                foreach (MeshRenderer renderer in other.transform.GetComponentsInChildren<MeshRenderer>())
                {
                    onHitOnce = false;
                    renderer.material = nonInteractibleMaterial;
                }//End foreach
            }//End else
        }//End if
        //If the raycast doesn't hit an interactible
        else
        {
            onHitOnce = false;
            //Deactivate the interaction text
            HUDHandler.setHovering(false, false);
            HUDHandler.setNameText("");
            HUDHandler.setVerbText("");
            //If other is not already set to null
            if(other != null)
            {
                //Turn off interaction glow
                foreach (MeshRenderer renderer in other.transform.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.material = nonInteractibleMaterial;
                }//End foreach
                //Reset other to be null
                other = null;
            }//End if
        }//End else
    }//End Update

    private void setNameTextForHUD()
    {
        //If the current interactive thing is a person
        if(other.GetComponent<ConversationPartner>())
        {
            GameState.currentGameState.characterNameIsKnown.TryGetValue(other.GetComponent<ConversationPartner>().getID(), out bool nameKnown);
            if (!nameKnown)
            {
                HUDHandler.setNameText("???");
                return;
            }//End if
        }//End if
        //Otherwise, set the name to be their ID as usual
        HUDHandler.setNameText(other.GetComponent<Interactive>().getID());
    }//End setNameTextForHUD
}