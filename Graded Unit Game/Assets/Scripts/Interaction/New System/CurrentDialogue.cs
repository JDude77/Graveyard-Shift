using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is what will pass on the information to the dialogue HUD
public class CurrentDialogue : MonoBehaviour
{
    #region Attributes
    #region Data To Pass
    //The line to display now
    [SerializeField]
    private string currentLine;
    //What is currently actually displayed of the chosen dialogue
    [SerializeField]
    private string displayLine = "";
    [SerializeField]
    //The name to display now
    private string currentName;
    //The portrait to display now
    [SerializeField]
    private Sprite currentImage;
    //The sound to play while text scrolls in
    [SerializeField]
    private AudioClip textBlip;
    //Audio source to hold the blips
    [SerializeField]
    private static AudioSource blipSource;
    //Typing speed
    [SerializeField]
    private float typingSpeed = 0.01f;
    [SerializeField]
    //Dialogue options for a player
    private List<SetLine> dialogueOptions;
    //Tracking whether an option has been chosen
    private bool optionChosen;
    //Coroutine for typing letter by letter
    [SerializeField]
    private IEnumerator typing;
    //Variable to tell the Dialogue Manager when it's finished typing
    private bool finishedTyping = false;
    #endregion
    #region Data Pass Locations
    //Player Choice Prefab
    private GameObject dialogueChoicePrefab;
    //The Conversation HUD Manager
    private ConversationHUD conversationHUD;
    #endregion
    #endregion

    #region Getters & Setters
    public bool getFinishedTyping()
    {
        return finishedTyping;
    }//End Finished Typing Getter

    public string getCurrentLine()
    {
        return currentLine;
    }//End Current Line Getter

    public void setCurrentLine(string currentLine)
    {
        this.currentLine = currentLine;
    }//End Current Line Setter

    public string getCurrentName()
    {
        return currentName;
    }//End Current Name Getter

    public void setCurrentName(string currentName)
    {
        this.currentName = currentName;
    }//End Current Name Setter

    public Sprite getCurrentImage()
    {
        return currentImage;
    }//End Current Image Getter

    public void setCurrentImage(Sprite currentImage)
    {
        this.currentImage = currentImage;
    }//End Current Image Setter

    public List<SetLine> getDialogueOptions()
    {
        return dialogueOptions;
    }//End Dialogue Options Getter

    public void setDialogueOptions(List<SetLine> dialogueOptions)
    {
        this.dialogueOptions = dialogueOptions;
    }//End Dialogue Options Setter

    public string getDisplayLine()
    {
        return displayLine;
    }//End Display Line Getter

    public bool getOptionChosen()
    {
        return optionChosen;
    }//End Option Chosen Getter

    public void setOptionChosen(bool optionChosen)
    {
        this.optionChosen = optionChosen;
        Button[] buttons = GameObject.FindGameObjectWithTag("Player Choices").GetComponentsInChildren<Button>();
        foreach(Button button in buttons)
        {
            button.interactable = false;
        }//End foreach
    }//End Option Chosen Setter

    public void setBlipSource(AudioSource source)
    {
        blipSource = source;
    }//End Blip Source Setter

    public void setTextBlip(AudioClip clip)
    {
        textBlip = clip;
    }//End Text Blip Setter
    #endregion

    #region Behaviours
    private void Awake()
    {
        dialogueChoicePrefab = (GameObject) Resources.Load("Prefabs/Option");
        conversationHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<ConversationHUD>();
        conversationHUD.setNPCLineInBox("");
        conversationHUD.setPlayerLineInBox("");
    }//End Awake

    public CurrentDialogue(Line line, SpeakingNPC npc)
    {
        //Set up all CurrentDialogue variables with Line data
        currentLine = line.text;
        currentName = npc.speakerName;
        currentImage = npc.portrait;
        textBlip = npc.voice;
    }//End Constructor

    public void displayDialogueOptions()
    {
        if (dialogueOptions.Count != 0)
        {
            VerticalLayoutGroup choiceArea = conversationHUD.getPlayerSpeakingDisplay().GetComponentInChildren<VerticalLayoutGroup>();
            GameObject choiceAreaObject = choiceArea.gameObject;
            List<GameObject> dialogueOptionObjects = new List<GameObject>();
            for (int i = 0; i < dialogueOptions.Count; i++)
            {
                dialogueOptionObjects.Add(dialogueChoicePrefab);
                dialogueOptionObjects[i].GetComponent<DialogueOption>().setDialogueOption(dialogueOptions[i]);
                Instantiate(dialogueOptionObjects[i], choiceAreaObject.transform);
            }//End for
            conversationHUD.setCurrentDialogue(this);
        }//End if
        else
        {
            Debug.LogError("There was a call to set up dialogue options, but there were no dialogue options set.");
        }//End else
    }//End setUpDialogueOptions

    public void speakLine()
    {
        finishedTyping = false;
        typing = TypeLine();
        if (typing != null)
        {
            StopCoroutine(typing);
        }//End if
        conversationHUD.setNPCName(currentName);
        conversationHUD.setNPCPortrait(currentImage);
        conversationHUD.setCurrentDialogue(this);
        conversationHUD.setNPCLineInBox(currentLine);
        StartCoroutine(typing);
    }//End speakLine

    private IEnumerator TypeLine()
    {
        conversationHUD.setNPCLineInBox("");
        displayLine = "";
        //For every letter in the line of text
        foreach(char letter in currentLine)
        {
            //Add a character from it to the displayed text
            displayLine += letter;
            switch(letter)
            {
                case ',': typingSpeed = DialogueScrollSpeeds.Comma; break;
                case '.': typingSpeed = DialogueScrollSpeeds.Stop; break;
                default: typingSpeed = DialogueScrollSpeeds.Regular; break;
            }//End switch
            conversationHUD.setNPCLineInBox(displayLine);
            blipSource.volume = 0.5f;
            if (textBlip != null && letter != ' ')
            {
                blipSource.PlayOneShot(textBlip);
            }//End if
            //Wait before adding the next one
            yield return new WaitForSeconds(typingSpeed);
        }//End foreach
        finishedTyping = true;
    }//End Type enumerator

    public void speakerIsPlayer(SpeakingNPC playerData)
    {
        currentImage = playerData.portrait;
        currentName = playerData.speakerName;
        textBlip = playerData.voice;
    }//End speakerIsPlayer
    #endregion
}