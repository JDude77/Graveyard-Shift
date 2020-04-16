using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private SetLine setLine;
    private string displayLine;
    [SerializeField]
    private TextMeshProUGUI text;
    private bool finishedTyping = false;
    private AudioSource playerSource;
    #endregion

    #region Getters & Setters
    public void setDialogueOption(SetLine setLine)
    {
        gameObject.GetComponent<Button>().interactable = false;
        this.setLine = setLine;
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = JSONHolder.getLine(setLine.lineID).text;
    }//End Dialogue Option Setter

    public SetLine getSetLine()
    {
        return setLine;
    }//End Set Line Getter
    
    public bool getFinishedTyping()
    {
        return finishedTyping;
    }//End Finished Typing Getter
    #endregion

    #region Behaviours
    private void Start()
    {
        StartCoroutine(TypeLine());
    }//End Start

    public void thisOptionWasChosen()
    {
        if (finishedTyping)
        {
            CurrentDialogue currentDialogue = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<CurrentDialogue>();
            if (!currentDialogue.getOptionChosen())
            {
                StartCoroutine(TypeLineIntoBox());
            }//End if
            currentDialogue.setOptionChosen(true);
        }//End if
    }//End thisOptionWasChosen

    private IEnumerator TypeLine()
    {
        displayLine = "";
        float typingSpeed;
        //For every letter in the line of text
        foreach (char letter in JSONHolder.getLine(setLine.lineID).text)
        {
            //Add a character from it to the displayed text
            displayLine += letter;
            typingSpeed = setTypingSpeedByChar(letter);
            text.text = displayLine;
            //Wait before adding the next one
            yield return new WaitForSeconds(typingSpeed/2);
        }//End foreach
        finishedTyping = true;
    }//End Type enumerator

    private IEnumerator TypeLineIntoBox()
    {
        playerSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        float typingSpeed;
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        stringBuilder.Insert(0, displayLine);
        ConversationHUD conversationHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<ConversationHUD>();
        for (int i = 0; i < displayLine.Length; i++)
        {
            playerSource.PlayOneShot(playerSource.clip);
            conversationHUD.setPlayerLineInBox(conversationHUD.getPlayerLineInBox().text + displayLine[i]);
            typingSpeed = setTypingSpeedByChar(displayLine[i]);
            stringBuilder.Replace(stringBuilder.ToString()[i], ' ', i, 1);
            text.text = stringBuilder.ToString();
            yield return new WaitForSeconds(typingSpeed);
        }//End for
        yield return new WaitForSeconds(0.5f);
        DialogueManager dialogueManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<DialogueManager>();
        dialogueManager.runDialogue(setLine);
    }//End UntypeLine

    private float setTypingSpeedByChar(char currentCharacter)
    {
        float typingSpeed;
        switch (currentCharacter)
        {
            case ',': case '-': case ':': typingSpeed = DialogueScrollSpeeds.Comma; break;
            case '.': case '?': case '!': case ';': typingSpeed = DialogueScrollSpeeds.Stop; break;
            default: typingSpeed = DialogueScrollSpeeds.Regular; break;
        }//End switch
        return typingSpeed;
    }//End setTypingSpeedByChar
    #endregion
}
