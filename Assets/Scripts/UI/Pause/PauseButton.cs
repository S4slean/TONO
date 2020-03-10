using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : Panel_Behaviour
{
    [Header("Pause References")]
    public Image pauseImage;



    private void Update()
    {
        MovePanel();
    }


    public void Click()
    {
        PauseManager.Instance.TogglePause();
    }

    public void SetUI()
    {
        pauseImage.sprite = UI_Manager.instance.uiPreset.pauseImage;
    }


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void MovePanel()
    {
        base.MovePanel();
    }
}
