using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private Tile playerTile;

    public Tile GetPlayerTile()
    {
        return playerTile;
    }

    public void SetPlayerTile(Tile newTile)
    {
        playerTile = newTile;
    }

    public void SetDestination(Tile destination)
    {
        print("Destination : " + destination.coord);
        List<Tile> path = Pathfinder.instance.SearchForShortestPath(playerTile, destination);
        Sequence s = DOTween.Sequence();
        foreach(Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.5f))
                .SetEase(Ease.Linear);                
        }
    }
}
