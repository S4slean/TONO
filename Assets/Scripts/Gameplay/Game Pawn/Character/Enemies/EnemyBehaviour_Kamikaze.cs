using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBehaviour_Kamikaze : EnemieBehaviour
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

    public override void Buff()
    {
        //_buffed = true;
        _currentRage += enemyStats.buff.rageIncrease;
        health += enemyStats.buff.healthBuff;
        _buffRoundTracker = enemyStats.buff.buffDuration;
        _buffedThisTurn = true;

        if (_currentRage >= rageThreshold)
        {
            _buffed = true;
            SwapVisual();
        }
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
            movementPoints += enemyStats.buff.movmentBuff;
            actionPoints += enemyStats.buff.actionBuff;
        }

    }

    public override void SetDestination(Tile destination, bool showHighlight = false)
    {
        //print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(associatedTile, destination);


        Sequence s = DOTween.Sequence();
        foreach (Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    associatedTile.SetPawnOnTile(null);
                    associatedTile = tile;
                    associatedTile.SetPawnOnTile(this);
                    associatedTile.rend.material = associatedTile.defaultMaterial;
                    associatedTile.highlighted = false;

                    //DROP ALCOHOL !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    movementPoints--;
                }));

        }

        s.OnComplete(() =>
        {
            _isDoingSomething = false;
        });
    }
    public override void SetRangedDestination(Tile destination, bool showHighlight = false)
    {
        //print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(associatedTile, destination);

        Sequence s = DOTween.Sequence();
        foreach (Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    associatedTile.SetPawnOnTile(null);
                    associatedTile = tile;
                    associatedTile.SetPawnOnTile(this);
                    associatedTile.rend.material = associatedTile.defaultMaterial;
                    associatedTile.highlighted = false;
                    movementPoints--;

                    //DROP ALCOHOL !!!!!!!!!!!!!!!!!!!!!!!!

                    if (IsInLineSight(enemyStats.rangedAttack.range))
                    {
                        s.Kill();
                    }
                }));
        }

        s.OnComplete(() =>
        {
            _isDoingSomething = false;

        });

        s.OnKill(() =>
        {
            _isDoingSomething = false;
        });
    }


}
