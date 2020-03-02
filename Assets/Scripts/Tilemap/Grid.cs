using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<Tile> freeTiles = new List<Tile>();
    public Tile[,] tilemap = new Tile[20,20];

    public Grid(string[,] array)
    {
        Vector3 position = Vector3.zero;
        Tile newTile = null;

        for(int row = 1; row <= array.GetLength(0); row++)
        {
            for(int column = 1; column <= array.GetLength(1); column++)
            {
                //print(position +" : ["+row+","+column+"]");
                switch (array[row-1,column-1])
                {
                    case "W":
                        GameObject newWall = Instantiate(TileManager_PlaceHolder.instance.WallPrefab, position, new Quaternion(0f, 0f, 0f, 0f));
                        newTile = newWall.GetComponent<Wall>();
                        newTile.coord.z = row;
                        newTile.coord.x = column;
                        break;

                    case "F":
                        GameObject newFree = Instantiate(TileManager_PlaceHolder.instance.FreePrefab, position, new Quaternion(0f, 0f, 0f, 0f));
                        newTile = newFree.GetComponent<Free>();
                        newTile.coord.z = row;
                        newTile.coord.x = column;
                        freeTiles.Add(newTile);
                        break;

                    case "P":
                        GameObject newFreePlayer = Instantiate(TileManager_PlaceHolder.instance.FreePrefab, position, new Quaternion(0f, 0f, 0f, 0f));
                        newTile = newFreePlayer.GetComponent<Free>();
                        newTile.coord.z = row;
                        newTile.coord.x = column;
                        //Spawn player
                        GameObject player = Instantiate(TileManager_PlaceHolder.instance.PlayerPrefab, new Vector3(position.x, 0.5f, position.z), new Quaternion(0f, 0f, 0f, 0f));
                        player.GetComponent<Player>().SetPlayerTile(newTile);
                        freeTiles.Add(newTile);
                        break;
                }

                SetTileInTilemap(newTile, row, column);

                if (column != 20)
                    position.z += TileManager_PlaceHolder.instance.FreePrefab.transform.localScale.x;
                else
                    position.z = 0;
            }
            position.x += TileManager_PlaceHolder.instance.FreePrefab.transform.localScale.x;
        }
    }

    public void SetTileInTilemap(Tile tile, int row, int column)
    {
        tilemap[row - 1, column - 1] = tile;
    }

    public Tile GetTile(float row, float column)
    {
        //print(row + " , " + column);
        return tilemap[(int)(row) - 1, (int)(column) - 1];
    }
}
