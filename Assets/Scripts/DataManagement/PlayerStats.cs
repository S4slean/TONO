﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerStats
{
    public int startingAP;
    public int startingMP;
    public int startingLP;
    public bool isGunLoadedAtStart;


    public bool[] upgradeChoicesMade;
    public bool[] upgradesPossessed;
}
