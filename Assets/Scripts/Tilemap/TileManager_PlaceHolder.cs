using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager_PlaceHolder : MonoBehaviour
{

    public static TileManager_PlaceHolder instance;
    public GameObject WallPrefab;
    public GameObject FreePrefab;
    public GameObject PlayerPrefab;
    public GameObject container;

    private Grid grid;
    public Grid Grid { get { return grid; } }

    public void Awake()
    {
        if(instance == null)
            instance = this;

        string[,] array = new string[,]{    { "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
                                            { "W", "F", "F", "F", "F", "W", "F", "F", "F", "W", "F", "F", "F", "F", "F", "F", "F", "F", "F", "W"},
                                            { "W", "F", "W", "W", "F", "W", "F", "W", "F", "W", "F", "W", "W", "F", "W", "F", "W", "W", "F", "W"},
                                            { "W", "F", "F", "W", "F", "W", "W", "W", "F", "W", "F", "W", "F", "F", "W", "F", "F", "W", "F", "W"},
                                            { "W", "W", "F", "W", "W", "W", "W", "F", "F", "F", "F", "W", "W", "W", "W", "W", "F", "F", "F", "W"},
                                            { "W", "F", "F", "F", "W", "F", "W", "W", "F", "W", "F", "F", "F", "W", "F", "W", "W", "W", "F", "W"},
                                            { "W", "F", "W", "F", "W", "F", "F", "W", "F", "F", "F", "W", "F", "F", "F", "F", "W", "F", "F", "W"},
                                            { "W", "F", "W", "W", "W", "F", "W", "W", "F", "W", "W", "W", "W", "W", "W", "F", "F", "F", "W", "W"},
                                            { "W", "F", "F", "F", "F", "F", "W", "F", "F", "F", "F", "F", "F", "F", "W", "W", "W", "F", "F", "W"},
                                            { "W", "W", "W", "F", "W", "W", "W", "F", "W", "W", "W", "W", "W", "F", "F", "F", "W", "W", "F", "W"},
                                            { "W", "F", "F", "F", "W", "F", "F", "F", "F", "F", "F", "F", "W", "F", "W", "W", "W", "F", "F", "W"},
                                            { "W", "W", "F", "W", "W", "F", "W", "W", "F", "W", "F", "W", "W", "F", "W", "F", "W", "W", "F", "W"},
                                            { "W", "F", "F", "F", "F", "F", "F", "W", "W", "W", "F", "W", "F", "F", "F", "F", "F", "F", "F", "W"},
                                            { "W", "F", "W", "W", "W", "W", "F", "F", "F", "W", "F", "W", "F", "W", "W", "W", "W", "W", "F", "W"},
                                            { "W", "F", "F", "F", "F", "W", "W", "W", "F", "F", "F", "W", "F", "W", "F", "F", "F", "W", "F", "W"},
                                            { "W", "F", "W", "W", "F", "W", "F", "P", "F", "W", "F", "F", "F", "W", "F", "W", "F", "W", "F", "W"},
                                            { "W", "F", "F", "W", "W", "W", "F", "W", "W", "W", "F", "W", "W", "W", "F", "W", "F", "F", "F", "W"},
                                            { "W", "W", "F", "W", "F", "F", "F", "F", "F", "W", "F", "F", "F", "F", "F", "W", "W", "W", "W", "W"},
                                            { "W", "F", "F", "F", "F", "W", "W", "W", "F", "F", "F", "W", "F", "W", "F", "F", "F", "F", "F", "W"},
                                            { "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"}
                                            };
        this.grid = new Grid(array);
    }

}
