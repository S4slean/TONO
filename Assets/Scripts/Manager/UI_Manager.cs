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
    public UI_Presets uiPreset;

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

    public static UI_Manager instance;



    private void Awake()
    {
        instance = this;
    }

    public void SetCursorIcon(CursorIconMode icon)
    {
        //switch(icon)
        //{
        //    case CursorIconMode.Select:
        //        Cursor.SetCursor(uiPreset.cursorIcons[0], Vector2.zero, CursorMode.Auto);
        //        break;

        //    case CursorIconMode.MoveTo:
        //        Cursor.SetCursor(uiPreset.cursorIcons[0], Vector2.zero, CursorMode.Auto);
        //        break;

        //    case CursorIconMode.Fire:
        //        Cursor.SetCursor(uiPreset.cursorIcons[0], Vector2.zero, CursorMode.Auto);
        //        break;

        //    case CursorIconMode.Invisible:
        //        Cursor.SetCursor(Texture2D.whiteTexture, Vector2.zero, CursorMode.Auto);
        //        break;
        //}
    }


    public void SetNextPhase()
    {

    }
}
