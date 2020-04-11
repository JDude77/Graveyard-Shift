using JSONUtilityExtended;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public static Dictionary<string, bool> interactedWithAtLeastOnce;
    public static Dictionary<string, bool> characterNameIsKnown;
    public static Dictionary<string, bool> levelIsComplete;
    public static Dictionary<string, bool> lineHasBeenSeen;
    public static Dictionary<string, bool> levelIsUnlocked;
    //Uncomment the below if/when items are added to the game
    //public Dictionary<string, bool> itemHasBeenInteractedWith;

    public static void updateGameState(string keyForValue, string unlockShortcut)
    {
        if (characterNameIsKnown.ContainsKey(keyForValue) && unlockShortcut.Equals("name"))
        {
            Debug.Log("CharacterIsKnown dictionary of " + keyForValue + " entry boolean updated to true.");
            characterNameIsKnown[keyForValue] = true;
        }//End if
        else if (interactedWithAtLeastOnce.ContainsKey(keyForValue) && unlockShortcut.Equals("interacted"))
        {
            Debug.Log("InteractedWithAtLeastOnce dictionary of " + keyForValue + " entry boolean updated to true.");
            interactedWithAtLeastOnce[keyForValue] = true;
        }//End else if
        else if (levelIsComplete.ContainsKey(keyForValue) && unlockShortcut.Equals("complete"))
        {
            Debug.Log("LevelIsComplete dictionary of " + keyForValue + " entry boolean updated to true.");
            levelIsComplete[keyForValue] = true;
        }//End else if
        else if (unlockShortcut.Equals("line"))
        {
            if (lineHasBeenSeen.ContainsKey(keyForValue))
            {
                Debug.Log("LineHasBeenSeen dictionary of " + keyForValue + " entry boolean already true.");
            }//End if
            else
            {
                Debug.Log("LineHasBeenSeen dictionary of " + keyForValue + " entry boolean added and made true.");
                lineHasBeenSeen.Add(keyForValue, true);
            }//End else
        }//End else if
        else if (levelIsUnlocked.ContainsKey(keyForValue) && unlockShortcut.Equals("unlock"))
        {
            Debug.Log("LevelIsUnlocked dictionary of " + keyForValue + " entry boolean updated to true.");
            levelIsUnlocked[keyForValue] = true;
        }//End else if
        else
        {
            Debug.LogWarning("UpdateGameState was called to change " + keyForValue + ", but a corresponding key was not found.\nUnlock shortcut: " + unlockShortcut);
        }//End else
    }//End updateGameState

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