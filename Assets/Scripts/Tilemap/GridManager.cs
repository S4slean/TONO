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
        bool searching = true;
        Tile currentTile = startingTile;
        Tile currentNeighbours = null;

        /*print("Up : " + currentTile.neighbours.up);
        print("Right : " + currentTile.neighbours.right);
        print("Down : " + currentTile.neighbours.down);
        print("Left : " + currentTile.neighbours.left);*/

        while (searching)
        {
            switch (dir)
            {
                case Direction.Up:
                    if(currentTile.neighbours.up != null)
                    {
                        currentNeighbours = currentTile.neighbours.up;
                        if(currentNeighbours is Water)
                        {
                            if (throughWater)
                            {
                                break;
                            }
                            else
                            {
                                searching = false;
                                break;
                            }
                        }
                        else if(currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
                        {
                            line.Add(currentNeighbours);
                            break;
                        }
                        else
                        {
                            searching = false;
                        }
                    }
                    else
                    {
                        searching = false;
                    }
                    break;
                case Direction.Right:
                    if(currentTile.neighbours.right != null)
                    {
                        currentNeighbours = currentTile.neighbours.right;
                        if(currentNeighbours is Water)
                        {
                            if (throughWater)
                            {
                                break;
                            }
                            else
                            {
                                searching = false;
                                break;
                            }
                        }
                        else if(currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
                        {
                            line.Add(currentNeighbours);
                            break;
                        }
                        else
                        {
                            searching = false;
                        }
                    }
                    else
                    {
                        searching = false;
                    }
                    break;
                case Direction.Down:
                    if(currentTile.neighbours.down != null)
                    {
                        currentNeighbours = currentTile.neighbours.down;
                        if(currentNeighbours is Water)
                        {
                            if (throughWater)
                            {
                                break;
                            }
                            else
                            {
                                searching = false;
                                break;
                            }
                        }
                        else if(currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
                        {
                            line.Add(currentNeighbours);
                            break;
                        }
                        else
                        {
                            searching = false;
                        }
                    }
                    else
                    {
                        searching = false;
                    }
                    break;
                case Direction.Left:
                    if(currentTile.neighbours.left != null)
                    {
                        currentNeighbours = currentTile.neighbours.left;
                        if(currentNeighbours is Water)
                        {
                            if (throughWater)
                            {
                                break;
                            }
                            else
                            {
                                searching = false;
                                break;
                            }
                        }
                        else if(currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
                        {
                            line.Add(currentNeighbours);
                            break;
                        }
                        else
                        {
                            searching = false;
                        }
                    }
                    else
                    {
                        searching = false;
                    }
                    break;
                default:
                    searching = false;
                    break;
            }
            currentTile = currentNeighbours;
        }
        return line;
    }
}
