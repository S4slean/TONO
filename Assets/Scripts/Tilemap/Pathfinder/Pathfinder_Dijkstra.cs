using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder_Dijkstra : MonoBehaviour
{
    public static Pathfinder_Dijkstra instance;

    private Tile Start;
    public int TileVisited = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public List<Tile> SearchForRange(Tile start, int range, bool startTileIsInclude)
    {
        Start = start;
        TileVisited = 0;

        List<Tile> tilesInRange = DijkstraRangeSearch(range);
        if (startTileIsInclude)
        {
            tilesInRange.Add(start);
        }
        //print(ShortestPath.Count);

        ResetTile();

        return tilesInRange;
    }

    private List<Tile> DijkstraRangeSearch(int range)
    {
        Start.MinCostToStart = 0;
        List<Tile> rangeList = new List<Tile>();
        List<Tile> Queue = new List<Tile>();
        Queue.Add(Start);
        do
        {
            Queue = Queue.OrderBy(x => x.MinCostToStart).ToList();
            Tile currentTile = Queue.First();
            Queue.Remove(currentTile);
            TileVisited++;

            foreach(Tile neighbours in currentTile.GetFreeNeighbours())
            {
                if (neighbours.Visited)
                    continue;
                if(neighbours.MinCostToStart == 0f || currentTile.MinCostToStart + 1 < neighbours.MinCostToStart )
                {
                    neighbours.MinCostToStart = currentTile.MinCostToStart + 1;
                    if(neighbours.MinCostToStart <= range)
                    {
                        rangeList.Add(neighbours);
                        if (neighbours.MinCostToStart < range && !Queue.Contains(neighbours))
                            Queue.Add(neighbours);
                    }
                }
            }
            currentTile.Visited = true;
        } while (Queue.Any());

        return rangeList;
    }

    public void ResetTile()
    {
        foreach(Tile tile in GridManager.instance.freeTiles)
        {
            tile.Reset();
        }
    }
}
