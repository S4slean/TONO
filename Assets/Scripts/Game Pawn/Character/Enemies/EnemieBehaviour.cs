using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBehaviour : GamePawn
{
    public EnemyData enemyStats;


    public int health = 1;
    public int movementPoints = 0;

    private bool _isMyTurn = false;
    private bool _isDoingSomething = false;
    private PlayerCharacter _player;

    protected override void Start()
    {
        base.Start();

        movementPoints = enemyStats.movement;
        health = enemyStats.health;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player = PlayerManager.instance.playerCharacter;
            Debug.Log(PlayerManager.instance.playerCharacter);
            Debug.Log(IsInLineSight(5));
        }

        if (_isMyTurn == false) return;

        if (!_isDoingSomething)
        {

        }
        else
        {

        }
    }



    public void PlayTurn()
    {
        _isMyTurn = true;
    }

    public virtual void DecideAction()
    {


        _isDoingSomething = true;
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
        Physics.Raycast(transform.position, dir,out hit, range * 2 );
        if (hit.transform == null || hit.transform.tag != "Player")
            return false;
        else
            return true;
    }

    public bool IsInMeleeRange()
    {
        Tile playerTile = _player.GetTile();
        if (GetTile().neighbours.up == playerTile || GetTile().neighbours.down == playerTile || GetTile().neighbours.left == playerTile || GetTile().neighbours.right == playerTile)
            return true;
        else
            return false;

    }

    public void GetClose(Tile tile)
    {
        SetDestination(tile);
    }

    public void GetInLineSight(Tile target)
    {
        Tile destinaton = GetTile();
        if(Mathf.Abs(GetTile().transform.position.x - target.transform.position.x) > Mathf.Abs(GetTile().transform.position.z - target.transform.position.z))
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

        SetDestination(destinaton);
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
