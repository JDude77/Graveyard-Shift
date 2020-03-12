using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntrance : Interactive
{
    [SerializeField]
    private GameObject portalPrefab;
    private GameObject portal;
    
    [SerializeField]
    private string levelThePortalLeadsTo;
    private new void Start()
    {
        base.Start();
        portalPrefab = (GameObject) Resources.Load("Prefabs/Portal");
        interactionMode = modes[3];
        displayVerb = "Open Portal";
    }//End Start

    private void Update()
    {
        if (GameState.currentGameState.levelIsUnlocked[id])
        {
            isInteractible = true;
        }//End if
        else
        {
            isInteractible = false;
        }//End else
    }//End Update

    public override void interact()
    {
        base.interact();
        if(portal == null)
        {
            portal = Instantiate(portalPrefab, transform);
            portal.transform.position = new Vector3(portal.transform.position.x + 1, portal.transform.position.y + 1, portal.transform.position.z);
            portal.GetComponent<Portal>().setLevel(levelThePortalLeadsTo);
            portal.GetComponent<Portal>().setIsInteractible(true);
            portal.transform.parent = null;
        }//End if
        else
        {
            Destroy(portal);
        }//End else
        StartCoroutine(WaitForPortal());
    }//End Interact

    private IEnumerator WaitForPortal()
    {
        yield return new WaitForSeconds(1f);
        gameHandler.changePlayerInteractionState(false);
    }//End WaitForPortal
}