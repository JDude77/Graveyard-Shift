using JSONUtilityExtended;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameState
{
    public static Dictionary<string, bool> interactedWithAtLeastOnce;
    public static Dictionary<string, bool> characterNameIsKnown;
    public static Dictionary<string, bool> levelIsComplete;
    public static Dictionary<string, bool> lineHasBeenSeen;
    public static Dictionary<string, bool> levelIsUnlocked;

    public static void updateGameState(string keyForValue, string unlockShortcut)
    {
        unlockShortcut = unlockShortcut.ToLower();
        if (unlockShortcut.Equals("name"))
        {
            characterNameIsKnown = updateGameStateEntry(characterNameIsKnown, keyForValue);
        }//End if
        else if (unlockShortcut.Equals("interacted"))
        {
            interactedWithAtLeastOnce = updateGameStateEntry(interactedWithAtLeastOnce, keyForValue);
        }//End else if
        else if (unlockShortcut.Equals("complete"))
        {
            levelIsComplete = updateGameStateEntry(levelIsComplete, keyForValue);
        }//End else if
        else if (unlockShortcut.Equals("line"))
        {
            lineHasBeenSeen = updateGameStateEntry(lineHasBeenSeen, keyForValue);
        }//End else if
        else if (levelIsUnlocked.ContainsKey(keyForValue) && unlockShortcut.Equals("unlock"))
        {
            levelIsUnlocked = updateGameStateEntry(levelIsUnlocked, keyForValue);
        }//End else if
        else
        {
            Debug.LogWarning("UpdateGameState was called to change " + keyForValue + ", but a corresponding key was not found.\nUnlock shortcut: " + unlockShortcut);
        }//End else
    }//End updateGameState

    private static Dictionary<string, bool> updateGameStateEntry(Dictionary<string, bool> gameStateDictionary, string keyToUpdate)
    {
        if (gameStateDictionary.ContainsKey(keyToUpdate))
        {
            if (gameStateDictionary[keyToUpdate] == true)
            {
                Debug.Log(gameStateDictionary + " dictionary of " + keyToUpdate + " entry boolean already true.");
            }//End if
            else
            {
                Debug.Log(gameStateDictionary + " dictionary of " + keyToUpdate + " entry boolean made true.");
                gameStateDictionary[keyToUpdate] = true;
            }//End else
        }//End if
        else
        {
            Debug.Log(gameStateDictionary + " dictionary of " + keyToUpdate + " entry boolean added and made true.");
            gameStateDictionary.Add(keyToUpdate, true);
        }//End else
        return gameStateDictionary;
    }//End updateGameStateEntry

    public static void initGameState()
    {
        characterNameIsKnown = new Dictionary<string, bool>();
        interactedWithAtLeastOnce = new Dictionary<string, bool>();
        levelIsComplete = new Dictionary<string, bool>();
        lineHasBeenSeen = new Dictionary<string, bool>();
        levelIsUnlocked = new Dictionary<string, bool>();
        //Find the correct data sets
        TextAsset JSONDataTA = new TextAsset();
        Dictionary<string, SpeakingNPC> speakers = JSONUtility.getSpeakers(JSONDataTA);
        //Set up all NPC "interacted with at least once" and "character name is known" (except Janice) variables to false
        foreach (SpeakingNPC snpc in speakers.Values)
        {
            interactedWithAtLeastOnce.Add(snpc.speakerID, false);
            if (!snpc.speakerID.Equals("Janice"))
            {
                characterNameIsKnown.Add(snpc.speakerID, false);
            }//End if
            else
            {
                characterNameIsKnown.Add(snpc.speakerID, true);
            }//End else
        }//End foreach
        levelIsUnlocked.Add("Musician", false);
        levelIsComplete.Add("Musician", false);
    }//End initGameState
}