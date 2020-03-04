using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            freeTiles.Add(t);
        }
    }
}
