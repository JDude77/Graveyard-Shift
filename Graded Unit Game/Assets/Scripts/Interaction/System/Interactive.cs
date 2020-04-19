using System;
using System.Collections.Generic;
using UnityEngine;

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
    protected GameHandler gameHandler;
    [SerializeField]
    protected Material[] materials;
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
    protected virtual void Start()
    {
        List<Material> tempList = new List<Material>();
        //Set to not be interactive, and set the interaction mode to default
        foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            foreach(Material material in renderer.materials)
            {
                tempList.Add(material);
            }//End foreach
        }//End foreach
        materials = new Material[tempList.Count];
        materials = tempList.ToArray();
        isInteracting = false;
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        gameHandler = gameManager.GetComponent<GameHandler>();
    }//End Start

    protected virtual void Update()
    {
        if(isInteractible)
        {
            foreach(Collider collider in GetComponents<Collider>())
            {
                if (collider.isTrigger) collider.enabled = true;
            }//End foreach
        }//End if
        else
        {
            foreach (Collider collider in GetComponents<Collider>())
            {
                if (collider.isTrigger) collider.enabled = false;
            }//End foreach
        }//End else
    }//End Update

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

    public void changeToInteractiveMaterial(Material interactibleMaterial)
    {
        foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] mats = new Material[r.materials.Length];
            for (int i = 0; i < r.materials.Length; i++)
            {
                mats[i] = interactibleMaterial;
            }//End foreach
            r.materials = mats;
        }//End foreach
    }//End changeToInteractiveMaterial

    public virtual void interact()
    {
        //If is set to not be interactible right now, don't interact
        if(!isInteractible)
        {
            Debug.Log("Interaction with " + name + " failed: isInteractible set to false.");
            return;
        }//End if
    }//End Interact

    public void revertMaterials()
    {
        int count = 0;
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            Material[] mats = new Material[meshes[i].materials.Length];
            MeshRenderer renderer = meshes[i];
            for (int j = 0; j < renderer.materials.Length; j++)
            {
                mats[j] = materials[count];
                count++;
            }//End foreach
            meshes[i].materials = mats;
        }//End foreach
    }//End revertMaterials
    #endregion
}
