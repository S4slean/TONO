﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "TONO/Skill/Buff")]
public class Buff : Skill
{
    public int movmentBuff;
    public int actionBuff;
    public int healthBuff;
    public int rageIncrease;
    public int buffDuration;

    public override void Activate(GamePawn user, Tile target)
    {
        if (user is EnemieBehaviour)
        {
            Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.Buff();
        }
        user.EndAction();
    }
}
