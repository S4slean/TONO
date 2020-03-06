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

    public Sprite pmImage = null;
    public Sprite unusedPM = null;

    [Header("Boat Infos")]
    public Sprite barrel_01 = null;
    public Sprite barrel_02 = null;
    public Sprite barrel_03 = null;
    public Sprite barrel_Mystery = null;

    [Header("Portaits")]
    public Sprite stick = null;

    public Sprite playerPortait = null;
    public Sprite boatPortait = null;
    public Sprite ennemy_01 = null;
    public Sprite ennemy_02 = null;
    public Sprite ennemy_03 = null;
    public Sprite ennemy_04 = null;

    [Header("Bullet")]
    public Sprite usedBullet = null;
    public Sprite unusedBullet = null;

    [Header("Skills images")]
    public Sprite moveImage = null;
    public Sprite shootImage = null;
    public Sprite reloadImage = null;
    public Sprite kickImage = null;
    public Sprite jumpImage = null;
}
