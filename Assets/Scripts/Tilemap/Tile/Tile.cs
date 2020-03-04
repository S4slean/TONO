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
    public Material defaultMaterial;
    public bool highlighted;

    //LOGIC
    public Neighbours neighbours;
    public float StraightLineDistanceToEnd, MinCostToStart;
    public bool Visited = false;
    public Tile previous;

    public object Connections { get; internal set; }

    void Start()
    {
        ScanNeighbours();

        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    public void ScanNeighbours()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit, 2f, LayerMask.GetMask("FreeTile"));
        if(hit.transform != null)
            neighbours.up = hit.transform.GetComponent<Tile>();
        Physics.Raycast(transform.position, Vector3.right, out hit, 2f, LayerMask.GetMask("FreeTile"));
        if(hit.transform != null)
            neighbours.right = hit.transform.GetComponent<Tile>();
        Physics.Raycast(transform.position, Vector3.back, out hit, 2f, LayerMask.GetMask("FreeTile"));
        if(hit.transform != null)
            neighbours.down = hit.transform.GetComponent<Tile>();
        Physics.Raycast(transform.position, Vector3.left, out hit, 2f, LayerMask.GetMask("FreeTile"));
        if(hit.transform != null)
            neighbours.left = hit.transform.GetComponent<Tile>();
    }

    public float StraightLineDistanceTo(Tile end)
    {
        //print((end.transform.position - transform.position).magnitude);
        return (end.transform.position - transform.position).magnitude;
    }

    public List<Tile> GetFreeNeighbours()
    {
        List<Tile> res = new List<Tile>();

        if(neighbours.up != null)
        {
            res.Add(neighbours.up);
        }

        if(neighbours.right != null)
        {
            res.Add(neighbours.right);
        }

        if(neighbours.down != null)
        {
            res.Add(neighbours.down);
        }

        if(neighbours.left != null)
        {
            res.Add(neighbours.left);
        }

        return res;
    }

    public void Reset()
    {
        Visited = false;
        StraightLineDistanceToEnd = 0f;
        MinCostToStart = 0f;
        previous = null;
    }

    public virtual void SetPawnOnTile(GamePawn pawn) { }
}