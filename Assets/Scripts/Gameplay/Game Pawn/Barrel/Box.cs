using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : GamePawn
{
    public override void OnKicked(GamePawn user, int dmg, Direction dir)
    {
        switch (dir)
        {
            case Direction.Down:
                SetDestination(GetTile().neighbours.down);
                break;

            case Direction.Left:
                SetDestination(GetTile().neighbours.left);
                break;

            case Direction.Right:
                SetDestination(GetTile().neighbours.right);
                break;

            case Direction.Up:
                SetDestination(GetTile().neighbours.up);
                break;
        }
    }

}
