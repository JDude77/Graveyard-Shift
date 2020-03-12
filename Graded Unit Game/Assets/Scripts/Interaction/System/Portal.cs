using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactive
{
    #region Attributes
    [SerializeField]
    private string levelToGoTo;
    #endregion

    #region Getters & Setters
    public void setLevel(string levelToGoTo)
    {
        this.levelToGoTo = levelToGoTo;
    }//End Level To Go To Setter
    #endregion

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        changeInteractionMode("portal");
        id = levelToGoTo;
        displayName = levelToGoTo;
    }//End Start

    public override void interact()
    {
        base.interact();
        SceneManager.LoadScene(levelToGoTo);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelToGoTo));
    }//End interact
}