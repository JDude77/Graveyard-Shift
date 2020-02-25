using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueOption : MonoBehaviour
{
    #region Attributes
    private SetLink setLink;
    private TextMeshProUGUI text;
    #endregion

    #region Constructor
    public DialogueOption(SetLink setLink)
    {
        JSONHolder jsonHolder = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<JSONHolder>();
        this.setLink = setLink;
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = jsonHolder.getLine(setLink.lineID).text;
    }//End Constructor
    #endregion
}
