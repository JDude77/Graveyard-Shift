using System.Collections.Generic;
using UnityEngine;
using JSONUtilityExtended;

public class GameState : MonoBehaviour
{
    public static GameStateShell currentGameState;
    public static GameState Instance { get; private set; }

    //Ensure there is only ever one instance of the game state object and that it stays persistent
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }//End if
        else
        {
            Instance = this;
        }//End else

        currentGameState = initGameState();
    }//End Awake

    //Get the current game state
    public GameStateShell getGameState()
    {
        return currentGameState;
    }//End game state getter

    public GameStateShell initGameState()
    {
        GameStateShell gameStateShell = new GameStateShell();
        gameStateShell.characterNameIsKnown = new Dictionary<string, bool>();
        gameStateShell.interactedWithAtLeastOnce = new Dictionary<string, bool>();
        gameStateShell.levelIsComplete = new Dictionary<string, bool>();
        gameStateShell.lineHasBeenSeen = new Dictionary<string, bool>();
        gameStateShell.levelIsUnlocked = new Dictionary<string, bool>();
        //Find the correct data sets
        TextAsset JSONDataTA = new TextAsset();
        Dictionary<string, SpeakingNPC> speakers = JSONUtility.getSpeakers(JSONDataTA);
        //Set up all NPC "interacted with at least once" and "character name is known" (except Janice) variables to false
        foreach (SpeakingNPC snpc in speakers.Values)
        {
            gameStateShell.interactedWithAtLeastOnce.Add(snpc.speakerID, false);
            if (!snpc.speakerID.Equals("Janice"))
            {
                gameStateShell.characterNameIsKnown.Add(snpc.speakerID, false);
            }//End if
            else
            {
                gameStateShell.characterNameIsKnown.Add(snpc.speakerID, true);
            }//End else
        }//End foreach
        Dictionary<string, Line> lines = JSONUtility.getLines(JSONDataTA);
        foreach(Line l in lines.Values)
        {
            gameStateShell.lineHasBeenSeen.Add(l.lineID, false);
        }//End foreach
        gameStateShell.levelIsUnlocked.Add("Musician", false);
        gameStateShell.levelIsComplete.Add("Musician", false);

        return gameStateShell;
    }//End initGameState
}

public struct GameStateShell
{
    public Dictionary<string, bool> interactedWithAtLeastOnce;
    public Dictionary<string, bool> characterNameIsKnown;
    public Dictionary<string, bool> levelIsComplete;
    public Dictionary<string, bool> lineHasBeenSeen;
    public Dictionary<string, bool> levelIsUnlocked;
    //Uncomment the below if/when items are added to the game
    //public Dictionary<string, bool> itemHasBeenInteractedWith;

    public void updateGameState(string keyForValue, string unlockShortcut)
    {
        if (characterNameIsKnown.ContainsKey(keyForValue) && unlockShortcut.Equals("name"))
        {
            Debug.Log("CharacterIsKnown dictionary of " + keyForValue + " entry boolean updated to true.");
            characterNameIsKnown[keyForValue] = true;
        }//End if
        else if (interactedWithAtLeastOnce.ContainsKey(keyForValue) && unlockShortcut.Equals("interacted"))
        {
            Debug.Log("InteractedWithAtLeastOnce dictionary of " + keyForValue + " entry boolean updated to true.");
            this.interactedWithAtLeastOnce[keyForValue] = true;
        }//End else if
        else if (levelIsComplete.ContainsKey(keyForValue) && unlockShortcut.Equals("complete"))
        {
            Debug.Log("LevelIsComplete dictionary of " + keyForValue + " entry boolean updated to true.");
            levelIsComplete[keyForValue] = true;
        }//End else if
        else if (lineHasBeenSeen.ContainsKey(keyForValue) && unlockShortcut.Equals("line"))
        {
            Debug.Log("LineHasBeenSeen dictionary of " + keyForValue + " entry boolean updated to true.");
            lineHasBeenSeen[keyForValue] = true;
        }//End else if
        else if(levelIsUnlocked.ContainsKey(keyForValue) && unlockShortcut.Equals("unlock"))
        {
            Debug.Log("LevelIsUnlocked dictionary of " + keyForValue + " entry boolean updated to true.");
            levelIsUnlocked[keyForValue] = true;
        }//End else if
        else
        {
            Debug.LogWarning("UpdateGameState was called to change " + keyForValue + ", but a corresponding key was not found.\nUnlock shortcut: " + unlockShortcut);
        }//End else
    }//End updateGameState
}