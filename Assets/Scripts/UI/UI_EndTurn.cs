using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndTurn : Panel_Behaviour
{
    [Header("End Turn References")]
    public Image endTurnImage;
    public Button endTurnButton;



    private void Update()
    {
        MovePanel();
        
    }


    public void EndTurn()
    {
        Debug.Log("Click");
        GameManager.Instance.CheckIfCompleted(true);
        Debug.Log("End AFTER");
        endTurnButton.interactable = false;
    }

    public void SetUI()
    {
        endTurnImage.sprite = UI_Manager.instance.uiPreset.endTurnImage;
    }


    public override void HidePanel()
    {
        endTurnButton.interactable = false;
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        endTurnButton.interactable = true;
        base.ShowPanel();
    }

    public override void MovePanel()
    {
        base.MovePanel();
    }
}
