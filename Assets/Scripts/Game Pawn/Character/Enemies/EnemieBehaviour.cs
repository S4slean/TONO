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
    private int rageThreshold = 5;
    public Skill meleeAttack;
    public Skill rangedAttack;
    public Buff buff;


    private PlayerCharacter _player;
    private bool _enraged = false;
    private int _currentRage = 0;
    private bool _buffed = false;
    private int _buffRoundTracker = 0;
    private bool _useBuffedThisTurn = false;
    

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayTurn();
        }

        if (_isMyTurn == false) return;

        if (!_isDoingSomething)
        {
            DecideAction();
        }
    }


    public void PlayTurn()
    {
        Debug.Log(transform.name + "'s Turn");
        _isMyTurn = true;

    }
    public void EndTurn()
    {
        Debug.Log("EndTurn");
        _isDoingSomething = false;
        _isMyTurn = false;
        movementPoints = enemyStats.movement;
        actionPoints = enemyStats.action;
        _useBuffedThisTurn = false; 

        if (_buffed)
        {
            _buffRoundTracker--;
            if(_buffRoundTracker <= 0)
            {
                _buffed = false;
            }
            movementPoints += buff.movmentBuff;
            actionPoints += buff.actionBuff;
        }
        
    }
    public virtual void DecideAction()
    {
        _isDoingSomething = true;

        if (meleeAttack != null && IsInMeleeRange() && actionPoints >= meleeAttack.cost)
        {
            meleeAttack.Activate(this, _player.GetTile());
        }
        else if (rangedAttack != null && IsInLineSight(rangedAttack.range) && actionPoints >= rangedAttack.cost)
        {
            rangedAttack.Activate(this, _player.GetTile());
        }
        else if(buff != null && !_useBuffedThisTurn && actionPoints >= buff.cost && !_buffed)
        {
            buff.Activate(this, GetTile());
        }
        else if (movementPoints > 0)
        {
            if (IsInMeleeRange())
            {
                _isDoingSomething = false;
                EndTurn();
            }
            else if ( !IsInLineSight(30) && DiceDecision(50))
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

    public virtual void Buff()
    {
        //_buffed = true;
        _currentRage += buff.rageIncrease;
        health += buff.healthBuff;
        _buffRoundTracker = buff.buffDuration;
        _useBuffedThisTurn = true;

        if(_currentRage >= rageThreshold)
        {
            _buffed = true;
        }
    }

    public float GetFlyDistanceFromPlayer()
    {
        Vector3 dir = _player.transform.position - transform.position;
        return dir.magnitude / 2;
    }
    public float GetCaseDistanceFromPlayer()
    {
        return Pathfinder_AStar.instance.SearchForShortestPath(GetTile(), _player.GetTile()).Count;
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
        Debug.Log("Get Close");
        List<Tile> adjacentTile = _player.GetTile().GetFreeNeighbours();
        if (adjacentTile.Count > 0)
        {
            Tile destination;
            List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(GetTile(), tile);
            destination = path[Mathf.Clamp(movementPoints - 1, 0, Mathf.Clamp(path.Count - 2, 0, path.Count - 2))];
            SetDestination(destination);
        }
        else
        {
            _isDoingSomething = false;
        }
    }
    public void GetInLineSight(Tile target)
    {
        Debug.Log("Get In Line");
        Tile destination = GetTile();
        if (Mathf.Abs(GetTile().transform.position.x - target.transform.position.x) > Mathf.Abs(GetTile().transform.position.z - target.transform.position.z))
        {
            while (destination.transform.position.z != target.transform.position.z)
            {
                if (destination.transform.position.z - target.transform.position.z > 0)
                {
                    if(destination.neighbours.down != null)
                        destination = destination.neighbours.down;
                }
                else if ((destination.transform.position.z - target.transform.position.z < 0))
                {
                    if (destination.neighbours.up != null)
                        destination = destination.neighbours.up;
                }
            }
        }
        else if (Mathf.Abs(GetTile().transform.position.x - target.transform.position.x) < Mathf.Abs(GetTile().transform.position.z - target.transform.position.z))
        {
            while (destination.transform.position.x != target.transform.position.x)
            {
                if (destination.transform.position.x - target.transform.position.x > 0)
                {
                    if (destination.neighbours.left != null)
                        destination = destination.neighbours.left;
                }
                else if ((destination.transform.position.x - target.transform.position.x < 0))
                {
                    if (destination.neighbours.right != null)
                        destination = destination.neighbours.right;
                }
            }
        }

        List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(GetTile(), destination);
        destination = path[Mathf.Clamp(movementPoints - 1, 0, path.Count - 1)];
        SetRangedDestination(destination);

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

                    movementPoints--;
                }));

        }

        s.OnComplete(() =>
        {
            _isDoingSomething = false;
        });
    }
    public virtual void SetRangedDestination(Tile destination, bool showHighlight = false)
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

                    if (IsInLineSight(rangedAttack.range))
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
