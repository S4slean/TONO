using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrickManager : MonoBehaviour
{
    public static BrickManager Instance;

    public GameObject brick;

    private void Awake()
    {
        Instance = this;
    }

    public BricksSettings settings;
    public BrickFloorData defaultBrickFloorData;

    public void Init()
    {
        BrickFloorGenerator.Instance.CreateBrickWall();
        BrickFloorGenerator.Instance.GenerateBrickWall(defaultBrickFloorData);
    }
}
