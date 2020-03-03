using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFloorGenerator : MonoBehaviour {

    public static BrickFloorGenerator Instance;

    private void Awake()
    {
        Instance = this;
    }

    public BrickFloorSettings settings;

    public Brick[] brickWall;

    public void CreateBrickWall()
    {
        brickWall = new Brick[settings.rowAmount * settings.lineAmount];
        for (int i = 0; i < settings.rowAmount; i++)
        {
            for (int j = 0; j < settings.lineAmount; j++)
            {
                int brickIndex = (i * settings.lineAmount) + j;
                brickWall[brickIndex] = CreateBrick(i, j);
            }
        }

    }

    Brick CreateBrick(int row, int line)
    {
        Brick newBrick = Instantiate(BrickManager.Instance.brick, transform).GetComponent<Brick>();
        newBrick.gameObject.name = "Brick_Row" + row.ToString() + "_Line" + line.ToString();
        newBrick.settings = null;

        Vector3 pos = Vector3.zero;
        pos.x = (settings.rowAmount * settings.xDistance) / 2;
        pos.x = -pos.x;
        pos.y = (settings.lineAmount * settings.yDistance) / 2;
        pos.y = -pos.y;
        pos.x += row * settings.xDistance;
        pos.y += line * settings.yDistance;
        pos.x += settings.xDistance / 2;
        pos.y += settings.yDistance / 2;
        pos += settings.centerPos;
        newBrick.transform.position = pos;

        newBrick.Init();

        return newBrick;
    }

	public void GenerateBrickWall(BrickFloorData data)
    {
        for(int i = 0; i < settings.rowAmount; i++)
        {
            for(int j = 0; j < settings.lineAmount; j++)
            {
                GenerateBrick(i, j, data);
            }
        }
    }

    void GenerateBrick(int row, int line, BrickFloorData data)
    {
        int brickIndex = (row * settings.lineAmount) + line;
        if(brickIndex <= 0)
        {
            brickIndex = 0;
        }
        if(data.brickIndexes[brickIndex] < 0)
        {
            brickWall[brickIndex].settings = null;
        }
        else
        {
            brickWall[brickIndex].settings = BrickManager.Instance.settings.bricks[data.brickIndexes[brickIndex]];
        }

        brickWall[brickIndex].TakeTypeAppearance();
    }
}
