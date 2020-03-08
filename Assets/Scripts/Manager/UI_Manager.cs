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
    public UI_Timeline timelinePanel;
    public UI_EndTurn endTurnPanel;
    public PauseManager pausePanel;
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



    void Awake()
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
        switch (displayMode)
        {
            case UIDisplayMode.Pause:
            case UIDisplayMode.End:
            case UIDisplayMode.None:
                gunPanel.HidePanel();
                timelinePanel.HidePanel();
                endTurnPanel.HidePanel();
                pausePanel.HidePanel();
                boatPanel.HidePanel();
                actionPanel.HidePanel();
                roundPanel.HidePanel();
                characterInfoPanel.HidePanel();
            break;

            case UIDisplayMode.Boat:
                gunPanel.HidePanel();
                timelinePanel.ShowPanel();
                endTurnPanel.HidePanel();
                pausePanel.ShowPanel();
                boatPanel.ShowPanel();
                actionPanel.HidePanel();
                roundPanel.ShowPanel();
                characterInfoPanel.HidePanel();
                break;

            case UIDisplayMode.Start:
            case UIDisplayMode.EnemyTurn:
                gunPanel.HidePanel();
                timelinePanel.ShowPanel();
                endTurnPanel.HidePanel();
                pausePanel.ShowPanel();
                boatPanel.HidePanel();
                actionPanel.HidePanel();
                roundPanel.ShowPanel();
                characterInfoPanel.HidePanel();
                break;

            case UIDisplayMode.PlayerTurn:
                gunPanel.ShowPanel();
                timelinePanel.ShowPanel();
                endTurnPanel.ShowPanel();
                pausePanel.ShowPanel();
                boatPanel.HidePanel();
                actionPanel.ShowPanel();
                roundPanel.ShowPanel();
                characterInfoPanel.ShowPanel();
                break;
        }

        currentDisplayMode = displayMode;
    }
}
