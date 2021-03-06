﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SC_UI_Preset", menuName = "UI/UI_Preset", order = 100)]
public class UI_Presets : ScriptableObject
{
    [Header("Character Infos")]
    public Sprite lifeBarImage = null;
    public Sprite lifeBarBackgroundImage = null;
    public Sprite lifeBarLimitImage = null;

    public Sprite paImage = null;
    public Sprite unusedPA = null;
    public Sprite usedPA = null;

    public Sprite pmImage = null;
    public Sprite unusedPM = null;
    public Sprite usedPM = null;

    [Header("Boat Infos")]
    public Sprite barrel_Round = null;
    public Sprite barrel_Default = null;
    public Sprite barrel_Plus = null;
    public Sprite barrel_Cross = null;
    public Sprite barrel_Mystery = null;

    [Header("Portaits")]
    public Sprite stick = null;

    public Sprite playerPortait = null;
    public Sprite boatPortait = null;
    public Sprite moussaillonImage = null;
    public Sprite captainImage = null;
    public Sprite kamikazeImage = null;
    public Sprite hookerImage = null;

    [Header("Bullet")]
    public Sprite usedBullet = null;
    public Sprite unusedBullet = null;

    [Header("Skills images")]
    public Sprite moveImage = null;
    public Sprite moveImageDeactivated = null;
    public Sprite shootImage = null;
    public Sprite shootImageDeactivated = null;
    public Sprite reloadImage = null;
    public Sprite reloadImageDeactivated = null;
    public Sprite kickImage = null;
    public Sprite kickImageDeactivated = null;
    public Sprite jumpImage = null;
    public Sprite jumpImageDeactivated = null;

    public Sprite skillBackgroundImage = null;

    [Header("Pause image")]
    public Sprite pauseImage = null;

    [Header("Round Counter images")]
    public Sprite wheelImage = null;
    public Sprite indicatorImage = null;

    [Header("End Turn images")]
    public Sprite endTurnImage = null;

    [Header("Message images")]
    public Sprite messagePanelImage = null;
    public Sprite leftStringImage = null;
    public Sprite rightStringImage = null;

    public UI_Presets()
    {
        lifeBarImage = null;
        lifeBarBackgroundImage = null;
        lifeBarLimitImage = null;

        paImage = null;
        unusedPA = null;
        usedPA = null;

        pmImage = null;
        unusedPM = null;
        usedPM = null;

        barrel_Round = null;
        barrel_Default = null;
        barrel_Plus = null;
        barrel_Cross = null;
        barrel_Mystery = null;

        stick = null;

        playerPortait = null;
        boatPortait = null;
        moussaillonImage = null;
        captainImage = null;
        kamikazeImage = null;
        hookerImage = null;

        usedBullet = null;
        unusedBullet = null;

        moveImage = null;
        moveImageDeactivated = null;
        shootImage = null;
        shootImageDeactivated = null;
        reloadImage = null;
        reloadImageDeactivated = null;
        kickImage = null;
        kickImageDeactivated = null;
        jumpImage = null;
        jumpImageDeactivated = null;

        skillBackgroundImage = null;

        pauseImage = null;

        wheelImage = null;
        indicatorImage = null;

        endTurnImage = null;

        messagePanelImage = null;
        leftStringImage = null;
        rightStringImage = null;
    }
}
