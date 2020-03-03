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
    }
}
