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
            if (IsWater(t) || IsWall(t))
                t.isWalkable = false;
            else
                t.isWalkable = true;

            freeTiles.Add(t);
        }
    }

    public bool IsWall(Tile tile)
    {
        return tile is Wall;
    }

    public bool IsWater(Tile tile)
    {
        return tile is Water;
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
                    if (currentTile.neighbours.up != null && !IsWall(currentTile.neighbours.up))
                    {
                        currentNeighbours = currentTile.neighbours.up;
                        if (IsWater(currentNeighbours))
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
                        else if (currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
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
                    if (currentTile.neighbours.right != null && !IsWall(currentTile.neighbours.right))
                    {
                        currentNeighbours = currentTile.neighbours.right;
                        if (IsWater(currentNeighbours))
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
                        else if (currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
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
                    if (currentTile.neighbours.down != null && !IsWall(currentTile.neighbours.right))
                    {
                        currentNeighbours = currentTile.neighbours.down;
                        if (IsWater(currentNeighbours))
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
                        else if (currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
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
                    if (currentTile.neighbours.left != null && !IsWall(currentTile.neighbours.left))
                    {
                        currentNeighbours = currentTile.neighbours.left;
                        if (IsWater(currentNeighbours))
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
                        else if (currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
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
            }
            //print(currentNeighbours);
            currentTile = currentNeighbours;
        }
        //print(startingTile.transform.localPosition);
        return line;
    }

    public List<Tile> GetRoundRange(GamePawn user, int range, bool usingCombo = false)
    {
        List<Tile> res = new List<Tile>();
        Tile startingTile = user.GetTile();
        res.Add(startingTile);

        //UP
        Tile currentTile = startingTile.neighbours.up;
        if (usingCombo)
            CheckForCombo(res, currentTile);
        currentTile = currentTile.neighbours.right;
        if (usingCombo)
            CheckForCombo(res, currentTile);

        //RIGHT
        currentTile = startingTile.neighbours.right;
        if (usingCombo)
            CheckForCombo(res, currentTile);
        currentTile = currentTile.neighbours.down;
        if (usingCombo)
            CheckForCombo(res, currentTile);

        //DOWN
        currentTile = startingTile.neighbours.down;
        if (usingCombo)
            CheckForCombo(res, currentTile);
        currentTile = currentTile.neighbours.left;
        if (usingCombo)
            CheckForCombo(res, currentTile);

        //LEFT
        currentTile = startingTile.neighbours.left;
        if (usingCombo)
            CheckForCombo(res, currentTile);
        currentTile = currentTile.neighbours.up;
        if (usingCombo)
            CheckForCombo(res, currentTile);
        return res;
    }

    public List<Tile> GetPlusRange(GamePawn user, int range, bool usingCombo = false)
    {
        Tile startingTile = user.GetTile();
        List<Tile> res = new List<Tile>();
        res.Add(startingTile);
        Tile currentTile;

        //UP
        currentTile = startingTile.neighbours.up;
        for (int i = 0; i < range; i++)
        {
            if (!(IsWall(currentTile) || currentTile == null || IsWall(currentTile)))
            {
                res.Add(currentTile);
                if (currentTile.GetPawnOnTile() is Barrel)
                {
                    Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                    if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                    {
                        res.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel));
                    }
                }
            }
            else
            {
                break;
            }
            currentTile = currentTile.neighbours.up;
        }

        //RIGHT
        currentTile = startingTile.neighbours.right;
        for (int i = 0; i < range; i++)
        {
            if (!(IsWall(currentTile) || currentTile == null || IsWall(currentTile)))
            {
                res.Add(currentTile);
                if (currentTile.GetPawnOnTile() is Barrel)
                {
                    Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                    if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                    {
                        res.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel));
                    }
                }
            }
            else
            {
                break;
            }
            currentTile = currentTile.neighbours.right;
        }

        //DOWN
        currentTile = startingTile.neighbours.down;
        for (int i = 0; i < range; i++)
        {
            if (!(IsWall(currentTile) || currentTile == null || IsWall(currentTile)))
            {
                res.Add(currentTile);
                if (currentTile.GetPawnOnTile() is Barrel)
                {
                    Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                    if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                    {
                        res.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel));
                    }
                }
            }
            else
            {
                break;
            }
            currentTile = currentTile.neighbours.down;
        }

        //LEFT
        currentTile = startingTile.neighbours.left;
        for (int i = 0; i < range; i++)
        {
            if (!(IsWall(currentTile) || currentTile == null || IsWall(currentTile)))
            {
                res.Add(currentTile);
                if (currentTile.GetPawnOnTile() is Barrel)
                {
                    Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                    if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                    {
                        res.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel));
                    }
                }
            }
            else
            {
                break;
            }
            currentTile = currentTile.neighbours.left;
        }

        return res;
    }

    public List<Tile> GetCrossRange(GamePawn user, int range, bool usingCombo = false)
    {
        Tile startingTile = user.GetTile();
        List<Tile> res = new List<Tile>();
        res.Add(startingTile);
        Tile currentTile;

        //UP-RIGHT
        currentTile = startingTile.neighbours.up.neighbours.right;
        if(usingCombo)
            CheckForCombo(res, currentTile);
        currentTile = currentTile.neighbours.up.neighbours.right;
        if (usingCombo)
            CheckForCombo(res, currentTile);

        //RIGHT-DOWN
        currentTile = startingTile.neighbours.right.neighbours.down;
        if (usingCombo)
            CheckForCombo(res, currentTile);
        currentTile = currentTile.neighbours.right.neighbours.down;
        if (usingCombo)
            CheckForCombo(res, currentTile);

        //DOWN-LEFT
        currentTile = startingTile.neighbours.down.neighbours.left;
        if (usingCombo)
            CheckForCombo(res, currentTile);
        currentTile = currentTile.neighbours.down.neighbours.left;
        if (usingCombo)
            CheckForCombo(res, currentTile);

        //LEFT-UP
        currentTile = startingTile.neighbours.left.neighbours.up;
        if (usingCombo)
            CheckForCombo(res, currentTile);
        currentTile = currentTile.neighbours.left.neighbours.up;
        if (usingCombo)
            CheckForCombo(res, currentTile);

        return res;
    }

    public void CheckForCombo(List<Tile> comboTiles, Tile currentTile)
    {
        if (currentTile != null && !IsWall(currentTile) && !IsWater(currentTile))
        {
            comboTiles.Add(currentTile);
            if (currentTile.GetPawnOnTile() is Barrel)
            {
                Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                {
                    comboTiles.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel));
                }
            }
        }
    }

}
