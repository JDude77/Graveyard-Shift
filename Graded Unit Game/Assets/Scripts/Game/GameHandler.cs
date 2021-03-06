﻿using UnityEngine;

public class GameHandler : MonoBehaviour
{
    /// <summary>
    /// The manager of managers for this whole game.
    /// References every other manager in the game and tries to keep ahold of it all.
    /// I have no idea what I'm doing so Janice help me through this.
    /// </summary>
    #region Attributes
    private UIManager uiManager;
    private bool playerCanMove;
    private bool uiIsVisible;
    private PlayerInteraction playerInteraction;
    #endregion

    #region Getters & Setters
    //Player Can Move Setter
    public void setPlayerCanMove(bool playerCanMove)
    {
        this.playerCanMove = playerCanMove;
    }//End Player Can Move Setter

    //Player Can Move Getter
    public bool getPlayerCanMove()
    {
        return playerCanMove;
    }//End Player Can Move Getter

    //UI Is Visible Setter
    public void setUIIsVisible(bool uiIsVisible)
    {
        this.uiIsVisible = uiIsVisible;
    }//End UI Is Visible Setter

    //UI Is Visible Getter
    public bool getUIIsVisible()
    {
        return uiIsVisible;
    }//End UI Is Visible Getter
    #endregion

    private void Awake()
    {
        GameState.initGameState();
    }//End Awake

    //Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(this);
        uiManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<UIManager>();
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        playerCanMove = true;
        uiIsVisible = true;
    }//End Start

    //Unlock a level
    public void unlockLevel(string levelToUnlock)
    {
        GameState.updateGameState(levelToUnlock, "unlock");
    }//End unlockLevel

    //Change player interaction state
    public void changePlayerInteractionState(bool newState)
    {
        playerCanMove = newState;
        playerInteraction.setIsInteracting(newState);
    }//End changePlayerInteractionState

    //Toggle the UI
    public void toggleUI(bool toggleState)
    {
        uiIsVisible = toggleState;
        uiManager.gameObject.SetActive(uiIsVisible);
    }//End toggleUI
}