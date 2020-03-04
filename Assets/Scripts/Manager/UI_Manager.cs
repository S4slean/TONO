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
    public UI_Presets cursorPreset;

    /// <summary>
    /// Player Character actions
    /// </summary>
    public enum CursorIconMode
    {
        Invisible,
        Wait,
        MoveTo,
        Select,
        Fire,
        Pass,
        Reload,
        Throw
    }





    public void SetCursorIcon(CursorIconMode icon)
    {
        switch(icon)
        {
            case CursorIconMode.Select:
                Cursor.SetCursor(cursorPreset.cursorIcons[0], Vector2.zero, CursorMode.Auto);
                break;

            case CursorIconMode.MoveTo:
                Cursor.SetCursor(cursorPreset.cursorIcons[0], Vector2.zero, CursorMode.Auto);
                break;

            case CursorIconMode.Fire:
                Cursor.SetCursor(cursorPreset.cursorIcons[0], Vector2.zero, CursorMode.Auto);
                break;

            case CursorIconMode.Invisible:
                Cursor.SetCursor(Texture2D.whiteTexture, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    public void DisplayRoundPanel()
    {

    }

    public void SetNextPhase()
    {

    }
}
