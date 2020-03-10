using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactive
{
    #region Attributes
    [SerializeField]
    private Scene levelToGoTo;
    #endregion

    #region Getters & Setters
    public void setLevel(Scene levelToGoTo)
    {
        this.levelToGoTo = levelToGoTo;
    }//End Level To Go To Setter
    #endregion

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        changeInteractionMode("portal");
        id = levelToGoTo.name;
        displayName = levelToGoTo.name;
    }//End Start

    public new void interact()
    {
        base.interact();
        SceneManager.SetActiveScene(levelToGoTo);
    }//End interact
}
