using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public List<Tile> freeTiles = new List<Tile>();

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        GetAllTiles();
    }

    public void GetAllTiles()
    {
        freeTiles = new List<Tile>();
        Tile[] temp = GameObject.FindObjectsOfType<Tile>();
        foreach (Tile t in temp)
        {
            if (t is Water)
                t.isWalkable = false;
            else
                t.isWalkable = true;

            freeTiles.Add(t);
        }
    }

    public List<Tile> GetLineUntilObstacle(List<Tile> line, Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                if (line[line.Count -1].neighbours.up != null && line[line.Count - 1].neighbours.up.isWalkable && (line[line.Count - 1].neighbours.up.GetPawnOnTile() == null || line[line.Count - 1].neighbours.up.GetPawnOnTile() == PlayerManager.instance.playerCharacter))
                {
                    line.Add(line[line.Count - 1].neighbours.up);
                    GetLineUntilObstacle(line, dir);
                }
                    break;

            case Direction.Right:
                if (line[line.Count - 1].neighbours.right != null && line[line.Count - 1].neighbours.right.isWalkable && (line[line.Count - 1].neighbours.right.GetPawnOnTile() == null || line[line.Count - 1].neighbours.right.GetPawnOnTile() == PlayerManager.instance.playerCharacter))
                {
                    line.Add(line[line.Count - 1].neighbours.right);
                    GetLineUntilObstacle(line, dir);
                }
                    break;

            case Direction.Down:
                if (line[line.Count - 1].neighbours.down != null && line[line.Count - 1].neighbours.down.isWalkable && (line[line.Count - 1].neighbours.down.GetPawnOnTile() == null || line[line.Count - 1].neighbours.down.GetPawnOnTile() == PlayerManager.instance.playerCharacter))
                {
                    line.Add(line[line.Count - 1].neighbours.down);
                    GetLineUntilObstacle(line, dir);
                }
                    break;

            case Direction.Left:
                if (line[line.Count - 1].neighbours.left != null && line[line.Count - 1].neighbours.left.isWalkable && (line[line.Count - 1].neighbours.left.GetPawnOnTile() == null || line[line.Count - 1].neighbours.left.GetPawnOnTile() == PlayerManager.instance.playerCharacter))
                {
                    line.Add(line[line.Count - 1].neighbours.left);
                    GetLineUntilObstacle(line, dir);
                }
                    break;
        }
        return line;
    }
}
