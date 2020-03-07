using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIDisplayMode
{
    None,
    PlayerTurn,
    EnemyTurn,
    Boat,
    Pause,
    End,
    Start,
}


public class UI_Manager : MonoBehaviour
{
    [Header("UI Preset")]
    public UI_Presets uiPreset;
    public UIDisplayMode currentDisplayMode;

    [Header("UI Panel References")]
    public UI_Gun gunPanel;
    public UI_OrderPanel orderPanel;
    public UI_BoatInfo boatPanel;
    public UI_ActionPanelBehaviour actionPanel;
    public UI_RoundCounter roundPanel;
    public UI_SelectedCharacterInfo characterInfoPanel;


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

    public void SetUIDisplayModeOn(UIDisplayMode displayMode)
    {
        switch (currentDisplayMode)
        {
            case UIDisplayMode.Boat:

            break;
        }
    }
}
