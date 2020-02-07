using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    /// <summary>
    /// The manager of managers for this whole game.
    /// References every other manager in the game and tries to keep ahold of it all.
    /// I have no idea what I'm doing so Janice help me through this.
    /// </summary>
    #region Attributes
    private UIManager uiManager;
    private GameState gameState;
    private bool playerCanMove;
    private bool uiIsVisible;
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

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(this);
        uiManager = GameObject.Find("HUD").GetComponent<UIManager>();
        gameState = GetComponent<GameState>();
        playerCanMove = true;
        uiIsVisible = true;
    }//End Awake
}