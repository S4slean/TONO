using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBehaviour : GamePawn
{
    public EnemyData enemyStats;

    public int health = 1;
    public int movementPoints = 0;

    protected override void Start()
    {
        base.Start();

        movementPoints = enemyStats.movement;
        health = enemyStats.health;
    }

    public void PlayTurn()
    {

    }

    public void GetClose(Tile tile)
    {

    }

    public void GetInRange()
    {

    }

    public void Drink()
    {

    }

    public void Attack()
    {

    }

    public void ThrowAlcohol()
    {

    }
}
