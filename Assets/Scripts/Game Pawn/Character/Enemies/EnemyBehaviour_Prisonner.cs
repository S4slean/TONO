using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour_Prisonner : EnemieBehaviour
{
    public override void DecideAction()
    {
        _isDoingSomething = true;

        if (enemyStats.meleeAttack != null && IsInMeleeRange() && actionPoints >= enemyStats.meleeAttack.cost)
        {
            enemyStats.meleeAttack.Activate(this, _player.GetTile());
        }
        else if (enemyStats.rangedAttack != null && IsInLineSight(enemyStats.rangedAttack.range) && actionPoints >= enemyStats.rangedAttack.cost)
        {
            enemyStats.rangedAttack.Activate(this, _player.GetTile());
        }
        else if (enemyStats.buff != null && !_buffedThisTurn && actionPoints >= enemyStats.buff.cost && !_buffed)
        {
            enemyStats.buff.Activate(this, GetTile());
        }
        else if (movementPoints > 0)
        {
            if (IsInMeleeRange())
            {
                _isDoingSomething = false;
                EndTurn();
            }
            else if (!IsInLineSight(30) && DiceDecision(50))
            {
                GetInLineSight(_player.GetTile());
            }
            else
            {
                GetClose(_player.GetTile());
            }
        }
        else
        {
            _isDoingSomething = false;
            EndTurn();
        }

    }
}
