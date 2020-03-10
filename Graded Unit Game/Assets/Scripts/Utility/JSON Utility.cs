using System.Collections.Generic;
using UnityEngine;

//Sort out extra JSON functionality that was not natively supported in Unity that I wanted easy access to
namespace JSONUtilityExtended
{
    public static class JSONUtility
    {
        public static TextAsset[] getConversationData()
        {
            Debug.Log("Trying to retreive conversation JSON data...");

            TextAsset conversationJSON = getJSON("Conversations");
            TextAsset setJSON = getJSON("Sets");
            TextAsset lineJSON = getJSON("Lines");
            TextAsset speakersJSON = getJSON("Speakers");

            Debug.Log("Conversation data retrieved.");

            TextAsset[] JSONs = { conversationJSON, setJSON, lineJSON, speakersJSON };
            return JSONs;
        }

        public static TextAsset getJSON(string JSONToGet)
        {
            if (Resources.Load<TextAsset>("JSON/" + JSONToGet) != null)
            {
                Debug.Log(JSONToGet + " JSON file retrieved.");
                return Resources.Load<TextAsset>("JSON/" + JSONToGet);
            }
            else
            {
                Debug.LogError(JSONToGet + " JSON file retreival failed.");
                return null;
            }
        }

        public static TextAsset checkDataIsThere(TextAsset givenData, string type)
        {
            if (givenData.ToString().Equals(""))
            {
                Debug.LogWarning(type + " Data passed in was empty. Attempting to fill Text Asset with data...");
                if (Resources.Load<TextAsset>("JSON/" + type) != null)
                {
                    Debug.Log(type + " Data was filled successfully.");
                    givenData = Resources.Load<TextAsset>("JSON/" + type);
                }//End if
                else
                {
                    Debug.LogError(type + " Data filling failed - JSON file not found.");
                    return null;
                }//End else
            }//End if
            return givenData;
        }//End checkDataIsThere

        public static Dictionary<GameStateShell, Conversation> getConversations(TextAsset conversationData)
        {
            Debug.Log("Trying to convert conversation JSON data to conversation objects...");

            conversationData = checkDataIsThere(conversationData, "Conversations");

            ConversationList conversationList = JsonUtility.FromJson<ConversationList>(conversationData.text);
            Dictionary<GameStateShell, Conversation> convos = new Dictionary<GameStateShell, Conversation>();

            int index = 0;
            foreach (Conversation c in conversationList.conversations)
            {
                Debug.Log("Coversation Found! ID: " + c.conversationID);
                convos.Add(GameState.currentGameState, c);
                //convos.Add(getConversationGameState(out GameStateShell newGameState), c);
                index++;
                Debug.Log("Conversation Added To Conversation Dictionary (" + index + " of " + conversationList.conversations.Count + ")");
            }//End foreach

            return convos;
        }

        public static Dictionary<string, Set> getSets(TextAsset setData)
        {
            Debug.Log("Trying convert set JSON data to set objects...");

            setData = checkDataIsThere(setData, "Sets");

            SetList setList = JsonUtility.FromJson<SetList>(setData.text);
            Dictionary<string, Set> sets = new Dictionary<string, Set>();

            int index = 0;
            foreach (Set s in setList.sets)
            {
                Debug.Log("Set Found! ID: " + s.setID);
                sets.Add(s.setID, s);
                index++;
                Debug.Log("Set Added To Set Dictionary (" + index + " of " + setList.sets.Count + ")");
            }//End foreach

            return sets;
        }

        public static Dictionary<string, Line> getLines(TextAsset lineData)
        {
            Debug.Log("Trying to convert line JSON data to line objects...");

            lineData = checkDataIsThere(lineData, "Lines");

            LineList lineList = JsonUtility.FromJson<LineList>(lineData.text);
            Dictionary<string, Line> lines = new Dictionary<string, Line>();

            int index = 0;
            foreach (Line l in lineList.lines)
            {
                Debug.Log("Line Found! ID: " + l.lineID);
                l.audioClip = (AudioClip)Resources.Load("Audio Clips/" + l.audioClipID);
                l.animationToPlay = (AnimationClip)Resources.Load("Animations/" + l.animationToPlayID);
                lines.Add(l.lineID, l);
                index++;
                Debug.Log("Line Added To Line Dictionary (" + index + " of " + lineList.lines.Count + ")");
            }//End foreach

            return lines;
        }

        public static Dictionary<string, SpeakingNPC> getSpeakers(TextAsset speakerData)
        {
            Debug.Log("Trying to convert speaker JSON data to speaking NPC objects...");

            speakerData = checkDataIsThere(speakerData, "Speakers");

            SpeakingNPCList speakerList = JsonUtility.FromJson<SpeakingNPCList>(speakerData.text);
            Dictionary<string, SpeakingNPC> speakers = new Dictionary<string, SpeakingNPC>();

            int index = 0;
            foreach (SpeakingNPC s in speakerList.speakers)
            {
                Debug.Log("Speaking NPC Found! ID: " + s.speakerID);
                s.portrait = (Sprite) Resources.Load("Portraits/" + s.portraitID);
                s.voice = (AudioClip) Resources.Load("Voices/" + s.voiceID);
                speakers.Add(s.speakerID, s);
                index++;
                Debug.Log("Speaking NPC Added To Speaking NPC Dictionary (" + index + " of " + speakerList.speakers.Count + ")");
            }//End foreach

            return speakers;
        }
    }
}