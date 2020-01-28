using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONUtilityExtended;

public class GameState : MonoBehaviour
{
    private static GameState _instance;
    public GameStateShell currentGameState;
    public static GameState Instance { get { return _instance; } }

    //Ensure there is only ever one instance of the game state object and that it stays persistent
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }//End if
        else
        {
            _instance = this;
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
        JSONUtility json = new JSONUtility();
        //Find the correct data sets
        TextAsset JSONDataTA = new TextAsset();
        Dictionary<string, SpeakingNPC> speakers = json.getSpeakers(JSONDataTA);
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
        Dictionary<string, Line> lines = json.getLines(JSONDataTA);
        foreach(Line l in lines.Values)
        {
            gameStateShell.lineHasBeenSeen.Add(l.lineID, false);
        }//End foreach
        gameStateShell.levelIsComplete.Add("Writer", false);

        return gameStateShell;
    }//End initGameState

    public void updateGameState(string keyForValue)
    {
        if(currentGameState.characterNameIsKnown.ContainsKey(keyForValue))
        {
            Debug.Log("CharacterIsKnown dictionary of " + keyForValue + " entry boolean updated to true.");
            currentGameState.characterNameIsKnown[keyForValue] = true;
        }//End if
        else if(currentGameState.interactedWithAtLeastOnce.ContainsKey(keyForValue))
        {
            Debug.Log("InteractedWithAtLeastOnce dictionary of " + keyForValue + " entry boolean updated to true.");
            currentGameState.interactedWithAtLeastOnce[keyForValue] = true;
        }//End else if
        else if(currentGameState.levelIsComplete.ContainsKey(keyForValue))
        {
            Debug.Log("LevelIsComplete dictionary of " + keyForValue + " entry boolean updated to true.");
            currentGameState.levelIsComplete[keyForValue] = true;
        }//End else if
        else if(currentGameState.lineHasBeenSeen.ContainsKey(keyForValue))
        {
            Debug.Log("LineHasBeenSeen dictionary of " + keyForValue + " entry boolean updated to true.");
            currentGameState.lineHasBeenSeen[keyForValue] = true;
        }//End else if
        else
        {
            Debug.LogWarning("UpdateGameState was called to change " + keyForValue + ", but a corresponding key was not found.");
        }//End else
    }//End updateGameState
}

public struct GameStateShell
{
    public Dictionary<string, bool> interactedWithAtLeastOnce;
    public Dictionary<string, bool> characterNameIsKnown;
    public Dictionary<string, bool> levelIsComplete;
    public Dictionary<string, bool> lineHasBeenSeen;
    //Uncomment the below if/when items are added to the game
    //public Dictionary<string, bool> itemHasBeenInteractedWith;
}