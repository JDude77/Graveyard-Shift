using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    #region Attributes
    private GameObject player, npc;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }//End Start

    #region Behaviours
    public void converse(GameObject npc)
    {
        Debug.Log("Starting conversation with " + npc.name);
        Debug.Log("Ending conversation with " + npc.name);
        player.GetComponent<PlayerInteraction>().setIsInteracting(false);
    }
    #endregion
}
