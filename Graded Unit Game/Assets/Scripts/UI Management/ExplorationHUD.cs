using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationHUD : MonoBehaviour
{
    #region Attributes
    private GameObject explorationHUDHolder;
    [SerializeField]
    private GameObject nameObject;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private GameObject verbObject;
    [SerializeField]
    private Text verbText;
    [SerializeField]
    private GameObject cursorObject;
    private Image cursorImage;
    private Color cursorDefault, cursorHoverActive, cursorHoverInactive;
    private bool isHoveringOverInteractible, hoverInteractibleIsEnabled;
    #endregion

    #region Behaviours
    //Initialise starting variables
    private void Start()
    {
        //Set initial hover value to false
        isHoveringOverInteractible = false;
        //Get the exploration HUD holder
        explorationHUDHolder = GameObject.FindGameObjectWithTag("Exploration HUD");
        //Get the name object and the text within it
        while(nameObject == null || verbObject == null || cursorObject == null)
        {
            for(int i = 0; i < explorationHUDHolder.transform.childCount; i++)
            {
                var child = explorationHUDHolder.transform.GetChild(i);
                switch(child.name)
                {
                    case "Name": nameObject = child.gameObject; break;
                    case "Cursor": cursorObject = child.gameObject; break;
                    case "Verb": verbObject = child.gameObject; break;
                }//End switch
            }//End for
        }//End while
        nameText = nameObject.GetComponent<Text>();
        //Get the verb object and the text within it
        
        verbText = verbObject.GetComponent<Text>();
        //Get the cursor object and the image within it
        
        cursorImage = cursorObject.GetComponent<Image>();
        //Cursor colour set-up
        cursorDefault = new Color(1, 1, 1, 0.75f);
        cursorHoverActive = new Color(1, 1, 1, 1f);
        cursorHoverInactive = new Color(0.5f, 0.5f, 0.5f, 0.75f);
    }//End Start


    #region Getters & Setters
    //Name Text Setter
    public void setNameText(string nameText)
    {
        this.nameText.text = nameText;
    }//End nameText setter

    //Verb Text Setter
    public void setVerbText(string verbText)
    {
        this.verbText.text = verbText;
    }//End verbText setter

    //Hovering Setter
    public void setHovering(bool isHovering, bool objectActive)
    {
        this.isHoveringOverInteractible = isHovering;
        this.hoverInteractibleIsEnabled = objectActive;
    }//End Hovering Setter
    #endregion

    private void Update()
    {
        //If the HUD as a whole is active
        if (explorationHUDHolder.activeSelf)
        {
            //If hovering over an interactive object
            if(isHoveringOverInteractible)
            {
                //Activate the name and verb things
                nameObject.SetActive(true);
                verbObject.SetActive(true);
                //If the thing that's being hovered over is actually interactible
                if(hoverInteractibleIsEnabled)
                {
                    //Set the cursor to the active colour
                    cursorImage.color = Color.Lerp(cursorImage.color, cursorHoverActive, 0.5f);
                }//End if
                //If the thing that's being hovered over is not interactible
                else
                {
                    //Set the cursor to the inactive colour
                    cursorImage.color = Color.Lerp(cursorImage.color, cursorHoverInactive, 0.5f);
                }//End else
            }//End if
            //If not hovering over an interactive object
            else
            {
                //Set the cursor colour to the default colour
                cursorImage.color = Color.Lerp(cursorImage.color, cursorDefault, 0.5f);
                //Deactivate the name and verb things
                nameObject.SetActive(false);
                verbObject.SetActive(false);
            }//End else
        }//End if
    }//End Update

    //Activate the exploration HUD as a whole
    public void enableHUD()
    {
        explorationHUDHolder.SetActive(true);
    }//End enableExplorationHUD

    //Deactivate the exploration HUD as a whole
    public void disableHUD()
    {
        explorationHUDHolder.SetActive(false);
    }//End disableExplorationHUD
    #endregion
}