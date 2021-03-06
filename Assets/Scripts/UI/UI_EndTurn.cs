﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndTurn : Panel_Behaviour
{
    [Header("End Turn References")]
    public Image endTurnImage;
    public bool canEndTurn;
    public Animator animator;


    private void Update()
    {
        MovePanel();
    }


    public void Highlighting()
    {
        animator.Play("Highlighted");
    }

    public void Clicking()
    {
        animator.Play("Clicking");
    }

    public void BackToNormal()
    {
        animator.Play("Normal");
    }

    public void EndTurn()
    {
        PlayerManager.instance.EndPlayerTurn();
        canEndTurn = false;
    }

    public void SetUI()
    {
        endTurnImage.sprite = UI_Manager.instance.uiPreset.endTurnImage;
    }


    public override void HidePanel()
    {
        canEndTurn = false;
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void MovePanel()
    {
        canEndTurn = true;
        base.MovePanel();
    }
}
