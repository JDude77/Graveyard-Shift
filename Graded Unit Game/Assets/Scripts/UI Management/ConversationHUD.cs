using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationHUD : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private GameObject convoHUDObject, playerSpeakingDisplay, npcSpeakingDisplay;
    private SpeakingNPC playerData, npcData;
    private CurrentDialogue currentDialogue;
    private Image portraitNPCDisplay, portraitPlayerDisplay;
    private TextMeshProUGUI speakerNameNPCDisplay, lineInBoxNPCDisplay, speakerNamePlayerDisplay, lineInBoxPlayerDisplay;
    #endregion

    #region Getters & Setters
    //NPC Portrait Getter
    public Image getNPCPortrait()
    {
        return portraitNPCDisplay;
    }//End Portrait Getter

    //NPC Portrait Setter
    public void setNPCPortrait(Image portrait)
    {
        this.portraitNPCDisplay = portrait;
    }//End Portrait Setter

    //Just-In-Case NPC Portrait Sprite Setter
    public void setNPCPortrait(Sprite portrait)
    {
        Debug.LogWarning("You asked setNPCPortrait to take in a sprite. It needs an image.");
        this.portraitNPCDisplay.sprite = portrait;
    }//End Portrait Setter

    //Just-In-Case Player Portrait Sprite Setter
    public void setPlayerPortrait(Sprite portrait)
    {
        Debug.LogWarning("You asked setPlayerPortrait to take in a sprite. It needs an image.");
        this.portraitPlayerDisplay.sprite = portrait;
    }//End Portrait Setter

    //Player Portrait Getter
    public Image getPlayerPortrait()
    {
        return portraitPlayerDisplay;
    }//End Portrait Getter

    //Player Portrait Setter
    public void setPlayerPortrait(Image portrait)
    {
        this.portraitPlayerDisplay = portrait;
    }//End Portrait Setter

    //NPC Name Getter
    public TextMeshProUGUI getNPCName()
    {
        return speakerNameNPCDisplay;
    }//End Name Getter

    //NPC Name Setter
    public void setNPCName(TextMeshProUGUI name)
    {
        this.speakerNameNPCDisplay = name;
    }//End Name Setter

    //Just-In-Case NPC Name String Setter
    public void setNPCName(string name)
    {
        Debug.LogWarning("You asked setNPCName to take in a string. It needs a Text asset.");
        this.speakerNameNPCDisplay.text = name;
    }//End Name Setter

    //Player Name Getter
    public TextMeshProUGUI getPlayerName()
    {
        return speakerNamePlayerDisplay;
    }//End Name Getter

    //Player Name Setter
    public void setPlayerName(TextMeshProUGUI name)
    {
        this.speakerNamePlayerDisplay = name;
    }//End Name Setter

    //Just-In-Case Player Name String Setter
    public void setPlayerName(string name)
    {
        Debug.LogWarning("You asked setPlayerName to take in a string. It needs a Text asset.");
        this.speakerNamePlayerDisplay.text = name;
    }//End Name Setter

    //NPC LineInBox Getter
    public TextMeshProUGUI getNPCLineInBox()
    {
        return lineInBoxNPCDisplay;
    }//End LineInBox Getter

    //NPC LineInBox Setter
    public void setNPCLineInBox(TextMeshProUGUI lineInBox)
    {
        this.lineInBoxNPCDisplay = lineInBox;
    }//End LineInBox Setter

    //Just-In-Case NPC LineInBox String Setter
    public void setNPCLineInBox(string lineInBox)
    {
        Debug.LogWarning("You asked setNPCLineInBox to take in a string. It needs a Text asset.");
        this.lineInBoxNPCDisplay.text = lineInBox;
    }//End LineInBox Setter

    //Player LineInBox Getter
    public TextMeshProUGUI getPlayerLineInBox()
    {
        return lineInBoxPlayerDisplay;
    }//End LineInBox Getter

    //Player LineInBox Setter
    public void setPlayerLineInBox(TextMeshProUGUI lineInBox)
    {
        this.lineInBoxPlayerDisplay = lineInBox;
    }//End LineInBox Setter

    //Just-In-Case Player LineInBox String Setter
    public void setPlayerLineInBox(string lineInBox)
    {
        Debug.LogWarning("You asked setPlayerLineInBox to take in a string. It needs a Text asset.");
        this.lineInBoxPlayerDisplay.text = lineInBox;
    }//End LineInBox Setter

    //NPC Speaker Data Setter
    public void setNPCData(SpeakingNPC npcData)
    {
        this.npcData = npcData;
    }//End NPC Data Setter

    //Current Dialogue Getter
    public CurrentDialogue getCurrentDialogue()
    {
        return currentDialogue;
    }//End Current Dialogue Getter

    //Current Dialogue Setter
    public void setCurrentDialogue(CurrentDialogue currentDialogue)
    {
        this.currentDialogue = currentDialogue;
    }//End Current Dialogue Setter

    //Player Speaking Display Getter
    public GameObject getPlayerSpeakingDisplay()
    {
        return playerSpeakingDisplay;
    }//End Player Speaking Display Getter
    #endregion

    private void Start()
    {
        //Create blank currentDialogue
        currentDialogue = null;
        //Get the player speaker data
        playerData = JSONHolder.getSpeaker("Player");
        //Get the portrait, name, and line objects from the UI
        while (portraitNPCDisplay == null || speakerNameNPCDisplay == null || lineInBoxNPCDisplay == null || portraitPlayerDisplay == null || speakerNamePlayerDisplay == null || lineInBoxPlayerDisplay == null)
        {
            for(int i = 0; i < convoHUDObject.transform.childCount; i++)
            {
                var child = convoHUDObject.transform.GetChild(i);
                switch(child.name)
                {
                    case "NPC Dialogue Box":
                        npcSpeakingDisplay = child.gameObject;
                        for (int j = 0; j < npcSpeakingDisplay.transform.childCount; j++)
                        {
                            var npcChild = npcSpeakingDisplay.transform.GetChild(j);
                            switch (npcChild.name)
                            {
                                case "Portrait": portraitNPCDisplay = npcChild.gameObject.GetComponent<Image>(); break;
                                case "Name": speakerNameNPCDisplay = npcChild.gameObject.GetComponent<TextMeshProUGUI>(); break;
                                case "Line": lineInBoxNPCDisplay = npcChild.gameObject.GetComponent<TextMeshProUGUI>(); break;
                            }//End switch
                        }//End for
                        break;
                    case "Player Choices":
                        playerSpeakingDisplay = child.gameObject;
                        var playerDialogueBox = playerSpeakingDisplay.transform.GetChild(0);
                        for (int j = 0; j < playerDialogueBox.transform.childCount; j++)
                        {
                            var playerChild = playerDialogueBox.transform.GetChild(j);
                            switch (playerChild.name)
                            {
                                case "Portrait":
                                    portraitPlayerDisplay = playerChild.gameObject.GetComponent<Image>();
                                    setPlayerPortrait(playerData.portrait);
                                    break;
                                case "Name":
                                    speakerNamePlayerDisplay = playerChild.gameObject.GetComponent<TextMeshProUGUI>();
                                    setPlayerName(playerData.speakerName);
                                    break;
                                case "Line":
                                    lineInBoxPlayerDisplay = playerChild.gameObject.GetComponent<TextMeshProUGUI>();
                                    setPlayerLineInBox("");
                                    break;
                            }//End switch
                        }//End for
                        break;
                }//End switch
            }//End for
        }//End while
        playerSpeakingDisplay.SetActive(false);
        npcSpeakingDisplay.SetActive(false);
    }//End Start

    private void Update()
    {
        //If the HUD as a whole is active
        if(convoHUDObject.activeSelf)
        {
            //If there even is a current dialogue to worry about displaying
            if (currentDialogue != null)
            {
                //If NPC is talking
                if (!currentDialogue.getCurrentName().Equals(playerData.speakerName))
                {
                    playerSpeakingDisplay.SetActive(false);
                    npcSpeakingDisplay.SetActive(true);
                }//End if
                 //If it's a player choice scenario
                else
                {
                    npcSpeakingDisplay.SetActive(false);
                    playerSpeakingDisplay.SetActive(true);
                }//End else
            }//End if
        }//End if
    }//End Update
}