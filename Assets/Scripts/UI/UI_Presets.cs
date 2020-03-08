using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SC_UI_Preset", menuName = "UI/UI_Preset", order = 100)]
public class UI_Presets : ScriptableObject
{
    [Header("Character Infos")]
    public Sprite lifeBarImage = null;

    public Sprite paImage = null;
    public Sprite unusedPA = null;
    public Sprite usedPA = null;

    public Sprite pmImage = null;
    public Sprite unusedPM = null;
    public Sprite usedPM = null;

    [Header("Boat Infos")]
    public Sprite barrel_01 = null;
    public Sprite barrel_02 = null;
    public Sprite barrel_03 = null;
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

    public UI_Presets()
    {
        lifeBarImage = null;

        paImage = null;
        unusedPA = null;
        usedPA = null;

        pmImage = null;
        unusedPM = null;
        usedPM = null;

        barrel_01 = null;
        barrel_02 = null;
        barrel_03 = null;
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
    }
}
