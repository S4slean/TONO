﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TONO/Enemy" )]
public class EnemyData : ScriptableObject
{

    public enum EnemyType { Moussaillon, Captain, Kamikaze, Hooker}


    public string enemyName = "Enemy";
    public EnemyType enemyType;
    public int health = 1;
    public int movement = 5;
    public int action = 7;
    public int rageThreshold = 5;

    public Skill meleeAttack;
    public Skill rangedAttack;
    public Buff buff;
}
