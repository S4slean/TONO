using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIMode
{
    onSelection,
    onTarget,
}



public class UI_Manager : MonoBehaviour
{
    [Header("Cursor Variables")]
    public 

    /// <summary>
    /// Player Character actions
    /// </summary>
    public enum CursorIcon
    {
        Invisible,
        Wait,
        MoveTo,
        Select,
        Attack,
        Hold,
        Overthrow,
        Throw
    }

    int currentNumberOfTurn = 0;





    /// <summary>
    /// Display character Info UI
    /// </summary>
    /// <param name="characterPosition">Player Character concerned</param>
    public void DisplayCharacterInfo(/*PlayerCharacter*/)
    {

    }

    /// <summary>
    /// Hide character info UI
    /// </summary>
    public void HideCharacterInfo(/*PlayerCharacter*/)
    {

    }

    public void SetCursorIcon(CursorIcon icon)
    {

    }



}
