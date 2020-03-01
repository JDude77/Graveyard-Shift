using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private GameObject conversationHUDGameObject;
    [SerializeField]
    private GameObject explorationHUDGameObject;
    private ConversationHUD conversationHUDScript;
    private ExplorationHUD explorationHUDScript;
    private enum HUDOptions
    {
        exploration,
        conversation
    };
    [SerializeField]
    private int activeHUD;
    #endregion

    #region Getters & Setters
    //Set the active HUD to whatever it needs to be
    public void setHUD(string HUDName)
    {
        switch(HUDName.ToLower())
        {
            case "exploration":
            case "explore":
                activeHUD = (int) HUDOptions.exploration;
                break;
            case "conversation":
            case "converse":
                activeHUD = (int) HUDOptions.conversation;
                break;
        }//End switch
    }//End HUD setter

    //Get the current active HUD
    public dynamic getHUD(bool gameObjectWanted = false)
    {
        if(gameObjectWanted)
        {
            switch(activeHUD)
            {
                case (int)HUDOptions.exploration:
                    return explorationHUDGameObject;
                case (int)HUDOptions.conversation:
                    return conversationHUDGameObject;
            }//End switch
        }//End if
        else
        {
            return activeHUD;
        }//End else
        return activeHUD;
    }//End HUD int getter
    #endregion

    //Get the game objects and scripts for managing the HUD
    private void Awake()
    {
        if(conversationHUDGameObject == null)
        {
            conversationHUDGameObject = GameObject.FindGameObjectWithTag("Conversation HUD");
        }//End if
        if(explorationHUDGameObject == null)
        {
            explorationHUDGameObject = GameObject.FindGameObjectWithTag("Exploration HUD");
        }//End if
        conversationHUDScript = gameObject.GetComponent<ConversationHUD>();
        explorationHUDScript = gameObject.GetComponent<ExplorationHUD>();
        activeHUD = (int) HUDOptions.exploration;
    }//End Start

    //Make sure there aren't multiple HUDs active at once
    private void Update()
    {
        switch(activeHUD)
        {
            case (int) HUDOptions.conversation:
                enableIfInactive(conversationHUDGameObject);
                disableIfActive(explorationHUDGameObject);
                break;
            case (int) HUDOptions.exploration:
                enableIfInactive(explorationHUDGameObject);
                disableIfActive(conversationHUDGameObject);
                break;
        }//End switch
    }//End Update

    private void enableIfInactive(GameObject objectToCheck)
    {
        if(!objectToCheck.activeSelf)
        {
            objectToCheck.SetActive(true);
        }//End if
    }//End enableIfInactive

    private void disableIfActive(GameObject objectToCheck)
    {
        if (objectToCheck.activeSelf)
        {
            objectToCheck.SetActive(false);
        }//End if
    }//End disableIfActive
}
