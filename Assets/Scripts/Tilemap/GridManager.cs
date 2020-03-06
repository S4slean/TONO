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

    public List<Tile> GetLineUntilObstacle(Direction dir, Tile startingTile, bool throughWater = false)
    {
        List<Tile> line = new List<Tile>();
        bool finding = true;
        Tile currentTile = startingTile;

        /*print("Up : " + currentTile.neighbours.up);
        print("Right : " + currentTile.neighbours.right);
        print("Down : " + currentTile.neighbours.down);
        print("Left : " + currentTile.neighbours.left);*/

        while (finding)
        {
            switch (dir)
            {
                case Direction.Up:
                    if(currentTile.neighbours.up != null)
                    {
                        if(currentTile.neighbours.up is Water)
                        {
                            if (throughWater)
                            {
                                currentTile = currentTile.neighbours.up;
                                continue;
                            }
                            else
                            {
                                finding = false;
                            }
                        }
                        else if(currentTile.neighbours.up.GetPawnOnTile() == null || currentTile.neighbours.up.GetPawnOnTile() is EnemieBehaviour)
                        {
                            line.Add(currentTile);
                            currentTile = currentTile.neighbours.up;
                            continue;
                        }
                        else
                        {
                            finding = false;
                        }
                    }
                    else
                    {
                        finding = false;
                    }
                    break;

                case Direction.Right:
                    if(currentTile.neighbours.right != null)
                    {
                        if(currentTile.neighbours.right is Water)
                        {
                            if (throughWater)
                            {
                                currentTile = currentTile.neighbours.right;
                                continue;
                            }
                            else
                            {
                                finding = false;
                            }
                        }
                        else if(currentTile.neighbours.right.GetPawnOnTile() == null || currentTile.neighbours.right.GetPawnOnTile() is EnemieBehaviour)
                        {
                            line.Add(currentTile);
                            currentTile = currentTile.neighbours.right;
                            continue;
                        }
                        else
                        {
                            finding = false;
                        }
                    }
                    else
                    {
                        finding = false;
                    }
                    break;

                case Direction.Down:
                    if(currentTile.neighbours.down != null)
                    {
                        if(currentTile.neighbours.down is Water)
                        {
                            if (throughWater)
                            {
                                currentTile = currentTile.neighbours.down;
                                continue;
                            }
                            else
                            {
                                finding = false;
                            }
                        }
                        else if(currentTile.neighbours.down.GetPawnOnTile() == null || currentTile.neighbours.down.GetPawnOnTile() is EnemieBehaviour)
                        {
                            line.Add(currentTile);
                            currentTile = currentTile.neighbours.down;
                            continue;
                        }
                        else
                        {
                            finding = false;
                        }
                    }
                    else
                    {
                        finding = false;
                    }
                    break;

                case Direction.Left:
                    if(currentTile.neighbours.left != null)
                    {
                        if(currentTile.neighbours.left is Water)
                        {
                            if (throughWater)
                            {
                                currentTile = currentTile.neighbours.left;
                                continue;
                            }
                            else
                            {
                                finding = false;
                            }
                        }
                        else if(currentTile.neighbours.left.GetPawnOnTile() == null || currentTile.neighbours.left.GetPawnOnTile() is EnemieBehaviour)
                        {
                            line.Add(currentTile);
                            currentTile = currentTile.neighbours.left;
                            continue;
                        }
                        else
                        {
                            finding = false;
                        }
                    }
                    else
                    {
                        finding = false;
                    }
                    break;

            }
        }
        return line;
    }
}
