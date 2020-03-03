using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        BrickManager.Instance.Init();

    }

    public Vector3[] floorCenterPositions;

    private void Update()
    {
        floorCenterPositions = Floor.centerPositions;
    }
}
