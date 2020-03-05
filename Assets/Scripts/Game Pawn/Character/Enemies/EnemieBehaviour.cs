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

    public virtual void PlayTurn()
    {

    }

    public int GetDistanceFromPlayer()
    {
        int dist = 2;
        return dist;
    }

    public bool IsInLineSight()
    {
        return true;
    }

    public bool IsInMeleeRange()
    {
        return true;
    }

    public void GetClose(Tile tile)
    {

    }

    public void GetInLineSight(Tile tile)
    {
        
    }

    public void DisplayMovementRange()
    {

    }

    public void DisplayAttackRange()
    {
        
    }

    



}
