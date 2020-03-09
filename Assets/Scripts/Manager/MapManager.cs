using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public int combatsCompleted;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.Instance.Load(true, SceneType.map);

        BoatPath.Instance.DrawPath();

        print(LevelManager.currentLevel);

        if (LevelManager.currentLevel == combatsCompleted + 1)
        {
            MapBoat.Instance.Place(LevelManager.currentLevel);
        }
        else
        {
            MapBoat.Instance.Place(combatsCompleted);
        }

        LevelPanel.Instance.HideImmediately();



        if(LightSwitch.Instance)
        {
            StartCoroutine(WaitThenCheckCompletion());
        }
        else
        {
            CheckCompletion();
        }


    }
    
    
    IEnumerator WaitThenCheckCompletion()
    {
        yield return new WaitForSeconds(LightSwitch.Instance.startingDelay);
        CheckCompletion();
    }

    public void NextLevel()
    {
        LevelManager.currentLevel++;
        MapBoat.Instance.MoveToAnchor(LevelManager.currentLevel);

    }

    bool willGoToNextLevel;
    public void CheckCompletion()
    {
        if(combatsCompleted >= LevelManager.currentLevel)
        {
            LevelManager.currentLevel = combatsCompleted;
            willGoToNextLevel = true;
            if (LevelManager.currentLevel > 0)
            {
                UpgradesManager.Instance.CheckUpgrades();
            }
            else
            {
                NextLevelIfSet();
            }

        }
        else
        {
            LevelPanel.Instance.Display();
        }
    }

    public void NextLevelIfSet()
    {
        if(willGoToNextLevel)
        {
            willGoToNextLevel = false;
            NextLevel();
        }
    }

    public void StartGame()
    {
        LevelManager.GoToScene("CombatCompletion");
    }

    public void BackToMenu()
    {
        LevelManager.GoToScene("Menu");
    }
}
