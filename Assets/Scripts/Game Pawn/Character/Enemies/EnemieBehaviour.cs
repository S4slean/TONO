using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBehaviour : GamePawn
{
    public EnemyData enemyStats;


    public int health = 1;
    public int movementPoints = 0;
    public int actionPoints = 0;
    public Skill meleeAttack;
    public Skill rangedAttack;
    public Skill buff;


    private PlayerCharacter _player;

    protected override void Start()
    {
        base.Start();


        health = enemyStats.health;
        movementPoints = enemyStats.movement;
        actionPoints = enemyStats.action;
        StartCoroutine(Initialisation());
    }
    IEnumerator Initialisation()
    {
        yield return new WaitForEndOfFrame();
        _player = PlayerManager.instance.playerCharacter;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isMyTurn = !_isMyTurn;
        }

        if (_isMyTurn == false) return;

        if (!_isDoingSomething)
        {
            DecideAction();
        }
    }


    public void PlayTurn()
    {
        _isMyTurn = true;
        movementPoints = enemyStats.movement;
        actionPoints = enemyStats.action;
    }
    public void EndTurn()
    {
        Debug.Log("EndTurn");
        _isDoingSomething = false;
        _isMyTurn = false;
    }

    public float GetDistanceFromPlayer()
    {
        Vector3 dir = _player.transform.position - transform.position;
        return dir.magnitude / 2;
    }
    public bool IsInLineSight(int range)
    {
        if (_player.GetTile().transform.position.x != GetTile().transform.position.x && _player.GetTile().transform.position.z != GetTile().transform.position.z)
            return false;

        Vector3 dir = _player.transform.position - transform.position;
        dir.Normalize();
        RaycastHit hit;
        Physics.Raycast(transform.position, dir, out hit, range * 2);
        if (hit.transform == null || hit.transform.tag != "Player")
            return false;
        else
        {
            return true;
        }
    }
    public bool IsInMeleeRange()
    {
        Tile playerTile = _player.GetTile();
        if (GetTile().neighbours.up == playerTile || GetTile().neighbours.down == playerTile || GetTile().neighbours.left == playerTile || GetTile().neighbours.right == playerTile)
        {
            return true;
        }
        else
            return false;

    }
    public void GetClose(Tile tile)
    {
        List<Tile> adjacentTile = _player.GetTile().GetFreeNeighbours();
        if (adjacentTile.Count > 0)
        {
            Tile destination;
            destination = adjacentTile[Random.Range(0, adjacentTile.Count)];
            SetDestination(destination);
        }
        else
        {
            _isDoingSomething = false;
        }
    }
    public void GetInLineSight(Tile target)
    {
        Tile destinaton = GetTile();
        if (Mathf.Abs(GetTile().transform.position.x - target.transform.position.x) > Mathf.Abs(GetTile().transform.position.z - target.transform.position.z))
        {
            while (destinaton.transform.position.z != target.transform.position.z)
            {
                if (destinaton.transform.position.z - target.transform.position.z > 0)
                    destinaton = destinaton.neighbours.down;
                else if ((destinaton.transform.position.z - target.transform.position.z < 0))
                    destinaton = destinaton.neighbours.up;
            }
        }
        else if (Mathf.Abs(GetTile().transform.position.x - target.transform.position.x) < Mathf.Abs(GetTile().transform.position.z - target.transform.position.z))
        {
            while (destinaton.transform.position.x != target.transform.position.x)
            {
                if (destinaton.transform.position.x - target.transform.position.x > 0)
                    destinaton = destinaton.neighbours.left;
                else if ((destinaton.transform.position.x - target.transform.position.x < 0))
                    destinaton = destinaton.neighbours.right;
            }
        }

        SetRangedDestination(destinaton);

    }
    public virtual void SetRangedDestination(Tile destination, bool showHighlight = false)
    {
        //print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(associatedTile, destination);

        if (showHighlight)
            Highlight_Manager.instance.ShowHighlight(path, HighlightMode.MoveHighlight);

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

                    if (IsInLineSight(rangedAttack.range))
                    {
                        s.Kill();
                    }
                }));

        }


        s.OnComplete(() =>
        {
            _isDoingSomething = false;
            Debug.Log("Completed");
        });

        s.OnKill(() =>
        {
            _isDoingSomething = false;
            Debug.Log("Completed");
        });
    }

    public virtual void DecideAction()
    {
        _isDoingSomething = true;

        if (IsInMeleeRange() && actionPoints >= meleeAttack.cost)
        {
            meleeAttack.Activate(this, _player.GetTile());
        }
        else if (IsInLineSight(rangedAttack.range) && actionPoints >= rangedAttack.cost)
        {
            rangedAttack.Activate(this, _player.GetTile());
        }
        else if (movementPoints > 0)
        {
            if (DiceDecision(50))
            {
                GetInLineSight(_player.GetTile());
            }
            else
            {
                GetClose(_player.GetTile());
            }

            movementPoints = 0;
        }
        else
        {
            EndTurn();
        }

    }

    public void DisplayMovementRange()
    {

    }
    public void DiplaySkillRange(Skill skill)
    {
        skill.Preview(this);
    }

    public bool DiceDecision(int threshold)
    {
        return Random.Range(0, 100) < threshold;
    }














}
