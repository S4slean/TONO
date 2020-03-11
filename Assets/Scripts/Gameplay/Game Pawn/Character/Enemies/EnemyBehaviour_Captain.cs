using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour_Captain : EnemieBehaviour
{
    public override void Buff()
    {

        _buffed = true;
        _currentRage += enemyStats.buff.rageIncrease;
        health += enemyStats.buff.healthBuff;
        _buffRoundTracker = enemyStats.buff.buffDuration;
        _buffedThisTurn = true;
        SwapVisual();

        //if (_currentRage >= rageThreshold)
        //{
        //    _buffed = true;
        //}
    }

    public override void EndTurn()
    {
        Debug.Log("EndTurn");
        _isDoingSomething = false;
        _isMyTurn = false;
        movementPoints = enemyStats.movement;
        actionPoints = enemyStats.action;
        _buffedThisTurn = false;

        if (_buffed)
        {
            _buffed = false;
            SwapVisual();
        }

        EnemyManager.instance.PlayNextEnemyTurn();

    }
}
