using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{

    //public Color hoverColor;

    protected Renderer rend;
    protected Material defaultMaterial;

    public Vector3 coord;
    public float StraightLineDistanceToEnd, MinCostToStart;
    public bool Visited = false;
    public Tile previous;

    public bool hovered;

    public object Connections { get; internal set; }

    public Tile(Vector3 position, float row, float column)
    {
        coord.z = (int)row;
        coord.x = (int)column;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    public float StraightLineDistanceTo(Tile end)
    {
        //print((end.transform.position - transform.position).magnitude);
        return (end.transform.position - transform.position).magnitude;
    }

    public List<Tile> GetFreeNeighbours()
    {
        List<Tile> res = new List<Tile>();

        if(TileManager_PlaceHolder.instance.Grid.GetTile(coord.z-1,coord.x) is Free && coord.z !=0)
        {
            res.Add(TileManager_PlaceHolder.instance.Grid.GetTile(coord.z - 1, coord.x));
        }

        if(TileManager_PlaceHolder.instance.Grid.GetTile(coord.z+1, coord.x) is Free && coord.z != TileManager_PlaceHolder.instance.Grid.tilemap.GetLength(0))
        {
            res.Add(TileManager_PlaceHolder.instance.Grid.GetTile(coord.z + 1, coord.x));
        }

        if(TileManager_PlaceHolder.instance.Grid.GetTile(coord.z, coord.x - 1) is Free && coord.x != 0)
        {
            res.Add(TileManager_PlaceHolder.instance.Grid.GetTile(coord.z, coord.x - 1));
        }

        if(TileManager_PlaceHolder.instance.Grid.GetTile(coord.z, coord.x + 1) is Free && coord.x != TileManager_PlaceHolder.instance.Grid.tilemap.GetLength(1))
        {
            res.Add(TileManager_PlaceHolder.instance.Grid.GetTile(coord.z, coord.x + 1));
        }

        return res;
    }
}