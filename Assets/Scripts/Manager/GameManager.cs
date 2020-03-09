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

    public float introductionDuration;

    public TurnType turnType;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.Instance.Load(true, SceneType.game);
        
        if(PauseManager.Instance)
        PauseManager.Instance.Initialize();

        if(overridesPlayerStats)
        {
            if(overridingPlayerStatsConfig != null)
            {
                playerStats = overridingPlayerStatsConfig.playerStats;
            }
        }

        UI_Manager.instance.SetUIDisplayModeOn(UIDisplayMode.Start);

        BarrelManager.Instance.Initialize();
        BombardmentManager.Instance.Initialize();

        StartCoroutine("IntroThenStart");
    }

    IEnumerator IntroThenStart()
    {
        yield return new WaitForSeconds(introductionDuration);

        StartBombardmentTurn();
    }

    public void CheckIfCompleted(bool goesToNext)
    {

        if(EnemyManager.instance.NoEnemiesLeft())
        {
            CompleteCombat();
        }
        else
        {
            if(goesToNext)
            {
                NextTurn();
            }

        }
    }

    public void StartBombardmentTurn()
    {
        turnType = TurnType.bombardment;
        //UI_Manager.instance.roundPanel.TurnWheel();
        UI_Manager.instance.timelinePanel.NextIconTurn();
        BombardmentManager.Instance.StartBombardment();
    }

    public void StartEnnemyTurn()
    {
        BombardmentManager.Instance.DropBarrels();
        turnType = TurnType.enemy;
        EnemyManager.instance.PlayEnemyTurn();
    }

    public void StartPlayerTurn()
    {
        turnType = TurnType.player;
        UI_Manager.instance.timelinePanel.NextIconTurn();
        UI_Manager.instance.SetUIDisplayModeOn(UIDisplayMode.PlayerTurn);
        PlayerManager.instance.StartPlayerTurn();
    }

    public void NextTurn()
    {
        switch(turnType)
        {
            case TurnType.bombardment:
                StartEnnemyTurn();
                break;
            case TurnType.enemy:
                StartPlayerTurn();
                break;
            case TurnType.player:
                StartBombardmentTurn();
                break;
        }
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

public enum TurnType
{
    bombardment,
    enemy,
    player
}
