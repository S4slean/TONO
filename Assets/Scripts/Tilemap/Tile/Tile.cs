using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public struct Neighbours
{
    public Tile up;
    public Tile right;
    public Tile down;
    public Tile left;
}
public class Tile : MonoBehaviour
{

    //public Color hoverColor;

    //GRAPHIC
     public Renderer rend;
    [HideInInspector] public Material oldMaterial;
    [HideInInspector] public Material defaultMaterial;
    public bool highlighted;

    //LOGIC
    public bool isWalkable;
    public bool isClickable;
    public bool hasAlcohol = false;
    public bool hasBarrelMarker = false;
    public bool hovered;
    public Neighbours neighbours;
    public float StraightLineDistanceToEnd, MinCostToStart;
    public bool Visited = false;
    public Tile previous;


    [Header("DEBUG")]
    public Tile up;
    public Tile right;
    public Tile down;
    public Tile left;

    public object Connections { get; internal set; }

    [SerializeField]protected GamePawn pawnOnTile;
    [SerializeField]protected Projectiles projectileOnTile;

    void Start()
    {
        ScanNeighbours();

        if (rend == null)
            rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    public virtual void OnMouseEnter() { }
    public virtual void OnMouseExit() { }

    public void ScanNeighbours()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit, 2f, 1 << 9 | 1 << 10);
        if (hit.transform != null)
        {
            neighbours.up = hit.transform.GetComponent<Tile>();
            up = hit.transform.GetComponent<Tile>();
        }
        Physics.Raycast(transform.position, Vector3.right, out hit, 2f, 1 << 9 | 1 << 10);
        if (hit.transform != null)
        {
            neighbours.right = hit.transform.GetComponent<Tile>();
            right = hit.transform.GetComponent<Tile>();
        }
        Physics.Raycast(transform.position, Vector3.back, out hit, 2f, 1 << 9 | 1 << 10);
        if (hit.transform != null)
        {
            neighbours.down = hit.transform.GetComponent<Tile>();
            down = hit.transform.GetComponent<Tile>();
        }
        Physics.Raycast(transform.position, Vector3.left, out hit, 2f, 1 << 9 | 1 << 10);
        if (hit.transform != null)
        {
            neighbours.left = hit.transform.GetComponent<Tile>();
            left = hit.transform.GetComponent<Tile>();
        }
    }

    public float StraightLineDistanceTo(Tile end)
    {
        //print((end.transform.position - transform.position).magnitude);
        return (end.transform.position - transform.position).magnitude;
    }

    public List<Tile> GetFreeNeighbours()
    {
        List<Tile> res = new List<Tile>();

        if (neighbours.up != null && neighbours.up.isWalkable && (neighbours.up.GetPawnOnTile() == null || neighbours.up.GetPawnOnTile() is PlayerCharacter))
        {
            res.Add(neighbours.up);
        }

        if (neighbours.right != null && neighbours.right.isWalkable && (neighbours.right.GetPawnOnTile() == null || neighbours.right.GetPawnOnTile() is PlayerCharacter))
        {
            res.Add(neighbours.right);
        }

        if (neighbours.down != null && neighbours.down.isWalkable && (neighbours.down.GetPawnOnTile() == null || neighbours.down.GetPawnOnTile() is PlayerCharacter))
        {
            res.Add(neighbours.down);
        }

        if (neighbours.left != null && neighbours.left.isWalkable && (neighbours.left.GetPawnOnTile() == null || neighbours.left.GetPawnOnTile() is PlayerCharacter))
        {
            res.Add(neighbours.left);
        }

        return res;
    }

    public Tile GetNeighbours(Direction dir)
    {
        switch (dir)
        {

            case Direction.Up:
                return neighbours.up;
            case Direction.Down:
                return neighbours.down;
            case Direction.Right:
                return neighbours.right;
            case Direction.Left:
                return neighbours.left;
        }

        return null;
    }

    public virtual void ActivateHighlight(HighlightMode highlightMode) { }

    public virtual void DeactivateHighlight(HighlightMode highlightModeAfter) { }

    public void Reset()
    {
        Visited = false;
        StraightLineDistanceToEnd = 0f;
        MinCostToStart = 0f;
        previous = null;
    }

    public virtual void SetPawnOnTile(GamePawn pawn)
    {
        pawnOnTile = pawn;
    }

    public virtual GamePawn GetPawnOnTile()
    {
        return pawnOnTile;
    }

    public virtual void SetProjectileOnTile(Projectiles projectiles)
    {
        projectileOnTile = projectiles;
    }

    public virtual Projectiles GetProjectileOnTile()
    {
        return projectileOnTile;
    }
}