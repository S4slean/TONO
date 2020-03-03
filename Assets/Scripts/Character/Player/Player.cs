using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
}
