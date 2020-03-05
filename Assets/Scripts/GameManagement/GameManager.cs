using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerStats playerStats;
    public bool overridesPlayerStats;
    public PlayerStatsConfig overridingPlayerStatsConfig;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        PauseManager.Instance.Initialize();

        BrickManager.Instance.Init();

        if(overridesPlayerStats)
        {
            if(overridingPlayerStatsConfig != null)
            {
                playerStats = overridingPlayerStatsConfig.playerStats;
            }
        }
    }

    public Vector3[] floorCenterPositions;

    private void Update()
    {
        floorCenterPositions = Floor.centerPositions;
    }

    public void SaveAndQuit()
    {

    }
}
