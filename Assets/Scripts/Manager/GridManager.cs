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

    public List<Tile> GetLineUntilObstacle(Direction dir, Tile startingTile, bool throughWater = false, bool includeHitTile = false)
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
                        if (!throughWater && IsWater(currentNeighbours))
                        {
                            break;
                        }
                        else if ((currentNeighbours.GetPawnOnTile() != null && currentNeighbours.GetPawnOnTile() is Barrel) || ((currentNeighbours.GetProjectileOnTile() != null)))
                        {
                            if(includeHitTile)
                                line.Add(currentNeighbours);
                            searching = false;
                            break;
                        }
                        else if ((!IsWater(currentNeighbours) && !IsWall(currentNeighbours)) || currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
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
                        if (throughWater && IsWater(currentNeighbours))
                        {
                            break;
                        }
                        else if (currentNeighbours.GetPawnOnTile() != null && currentNeighbours.GetPawnOnTile() is Barrel)
                        {
                            if (includeHitTile)
                                line.Add(currentNeighbours);
                            searching = false;
                            break;
                        }
                        else if (!IsWater(currentNeighbours) && currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
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
                    if (currentTile.neighbours.down != null && !IsWall(currentTile.neighbours.down))
                    {
                        currentNeighbours = currentTile.neighbours.down;
                        if (throughWater && IsWater(currentNeighbours))
                        {
                            break;
                        }
                        else if (currentNeighbours.GetPawnOnTile() != null && currentNeighbours.GetPawnOnTile() is Barrel)
                        {
                            if (includeHitTile)
                                line.Add(currentNeighbours);
                            searching = false;
                            break;
                        }
                        else if (!IsWater(currentNeighbours) && currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
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
                        if (throughWater && IsWater(currentNeighbours))
                        {
                            break;
                        }
                        else if (currentNeighbours.GetPawnOnTile() != null && currentNeighbours.GetPawnOnTile() is Barrel)
                        {
                            if (includeHitTile)
                                line.Add(currentNeighbours);
                            searching = false;
                            break;
                        }
                        else if (!IsWater(currentNeighbours) && currentNeighbours.GetPawnOnTile() == null || currentNeighbours.GetPawnOnTile() is EnemieBehaviour || currentNeighbours.GetPawnOnTile() is PlayerCharacter)
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
        /*Tile startingTile = user.GetTile();
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
            CheckForCombo(res, currentTile);*/

        //BOXCAST
        RaycastHit[] hits = Physics.BoxCastAll(user.GetTile().transform.position + (2 * Vector3.up), (Vector3.forward + Vector3.right) * 2 * range, Vector3.down, Quaternion.identity, 2f, LayerMask.GetMask("FreeTile"));

        foreach(RaycastHit hit in hits)
        {
            Tile tile = hit.transform.GetComponent<Free>();
            if (tile != null && !IsWater(tile))
            {
                CheckForCombo(res, tile, usingCombo);
            }
        }

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
            if (currentTile != null && !IsWall(currentTile))
            {
                if (!IsWater(currentTile))
                    res.Add(currentTile);
                if (currentTile.GetPawnOnTile() is Barrel)
                {
                    Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                    if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                    {
                        res.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel, usingCombo));
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
            if (currentTile != null && !IsWall(currentTile))
            {
                if (!IsWater(currentTile))
                    res.Add(currentTile);
                if (currentTile.GetPawnOnTile() is Barrel)
                {
                    Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                    if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                    {
                        res.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel, usingCombo));
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
            if (currentTile != null && !IsWall(currentTile))
            {
                if (!IsWater(currentTile))
                    res.Add(currentTile);
                if (currentTile.GetPawnOnTile() is Barrel)
                {
                    Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                    if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                    {
                        res.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel, usingCombo));
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
            if (currentTile != null && !IsWall(currentTile))
            {
                if(!IsWater(currentTile))
                    res.Add(currentTile);
                if (currentTile.GetPawnOnTile() is Barrel)
                {
                    Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                    if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                    {
                        res.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel, usingCombo));
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
        Tile currentUp;
        Tile currentRight;
        Tile currentDown;
        Tile currentLeft;

        //UP-RIGHT
        currentUp = startingTile.neighbours.up;
        currentRight = startingTile.neighbours.right;
        for (int i = 0; i < range; i++)
        {
            if(currentUp != null || currentRight != null)
            {
                if(!IsWall(currentUp) || !IsWall(currentRight))
                {
                    Tile checkTile = null;
                    if (currentUp == null || IsWall(currentUp))
                    {
                        checkTile = currentRight.neighbours.up;
                    }
                    else if(currentRight == null || IsWall(currentRight))
                    {
                        checkTile = currentUp.neighbours.right;
                    }
                    else
                    {
                        checkTile = currentUp.neighbours.right;
                    }
                    CheckForCombo(res, checkTile, usingCombo);
                    if(checkTile != null && !IsWall(checkTile))
                    {
                        currentUp = checkTile.neighbours.up;
                        currentRight = checkTile.neighbours.right;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }

        }

        //RIGHT-DOWN
        currentRight = startingTile.neighbours.right;
        currentDown = startingTile.neighbours.down;
        for (int i = 0; i < range; i++)
        {
            if (currentRight != null || currentDown != null)
            {
                if (!IsWall(currentRight) || !IsWall(currentDown))
                {
                    Tile checkTile = null;
                    if (currentRight == null || IsWall(currentRight))
                    {
                        checkTile = currentDown.neighbours.right;
                    }
                    else if (currentDown == null || IsWall(currentDown))
                    {
                        checkTile = currentRight.neighbours.down;
                    }
                    else
                    {
                        checkTile = currentRight.neighbours.down;
                    }
                    CheckForCombo(res, checkTile, usingCombo);
                    if (checkTile != null && !IsWall(checkTile))
                    {
                        currentRight = checkTile.neighbours.right;
                        currentDown = checkTile.neighbours.down;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        //DOWN-LEFT
        currentDown = startingTile.neighbours.down;
        currentLeft = startingTile.neighbours.left;
        for (int i = 0; i < range; i++)
        {
            if (currentDown != null || currentLeft != null)
            {
                if (!IsWall(currentDown) || !IsWall(currentLeft))
                {
                    Tile checkTile = null;
                    if (currentDown == null || IsWall(currentDown))
                    {
                        checkTile = currentLeft.neighbours.down;
                    }
                    else if (currentLeft == null || IsWall(currentLeft))
                    {
                        checkTile = currentDown.neighbours.left;
                    }
                    else
                    {
                        checkTile = currentDown.neighbours.left;
                    }
                    CheckForCombo(res, checkTile, usingCombo);
                    if (checkTile != null && !IsWall(checkTile))
                    {
                        currentDown = checkTile.neighbours.down;
                        currentLeft = checkTile.neighbours.left;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        //LEFT-UP
        currentLeft = startingTile.neighbours.left;
        currentUp = startingTile.neighbours.up;
        for (int i = 0; i < range; i++)
        {
            if (currentLeft != null || currentUp != null)
            {
                if (!IsWall(currentLeft) || !IsWall(currentUp))
                {
                    Tile checkTile = null;
                    if (currentLeft == null || IsWall(currentLeft))
                    {
                        checkTile = currentUp.neighbours.left;
                    }
                    else if (currentUp == null || IsWall(currentUp))
                    {
                        checkTile = currentLeft.neighbours.up;
                    }
                    else
                    {
                        checkTile = currentLeft.neighbours.up;
                    }
                    CheckForCombo(res, checkTile, usingCombo);
                    if (checkTile != null && !IsWall(checkTile))
                    {
                        currentLeft = checkTile.neighbours.left;
                        currentUp = checkTile.neighbours.up;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        //print("Cross Range : " + res.Count);
        return res;
    }

    public void CheckForCombo(List<Tile> comboTiles, Tile currentTile, bool usingCombo)
    {
        //print("CHECK FOR COMBO : " + currentTile);
        if (currentTile != null && !IsWall(currentTile) && !IsWater(currentTile))
        {
            comboTiles.Add(currentTile);
            if (currentTile.GetPawnOnTile() is Barrel)
            {
                Barrel barrel = currentTile.GetPawnOnTile() as Barrel;
                if (!ComboManager.instance.BarrelAlreadyInCombo(barrel))
                {
                    comboTiles.AddRange(ComboManager.instance.AddBarrelToComboPreview(barrel, usingCombo));
                }
            }
        }
    }

    public void AllTilesBecameNotClickable()
    {
        foreach(Tile tile in freeTiles)
        {
            tile.isClickable = false;
        }
    }
    public void TilesBecameNotClickableExceptMoveRangeTile()
    {
        foreach(Tile tile in freeTiles)
        {
            if(PlayerManager.instance.playerCharacter.moveRange.Contains(tile))
                tile.isClickable = true;
            else
                tile.isClickable = false;
        }
    }

    public void AllTilesBecameClickable()
    {
        foreach(Tile tile in freeTiles)
        {
            tile.isClickable = true;
        }
    }
}
