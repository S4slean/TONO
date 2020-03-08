using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerStats playerStats;
    public bool overridesPlayerStats;
    public PlayerStatsConfig overridingPlayerStatsConfig;
    public int combatsCompleted;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.Instance.Load(true, SceneType.game);

        PauseManager.Instance.Initialize();

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

    
    public void CompleteCombat()
    {
        if(LevelManager.currentLevel > combatsCompleted)
        {
            combatsCompleted = LevelManager.currentLevel;
        }

        SaveAndQuit();
    }

    public void SaveAndQuit()
    {
        DataManager.Instance.Save(SceneType.game);
        LevelManager.GoToScene("Map");
    }
}
