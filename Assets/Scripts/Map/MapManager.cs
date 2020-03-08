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

        CheckCompletion();
    }

    private void Update()
    {
        print(LevelManager.currentLevel);
        if(Input.GetKeyUp(KeyCode.Space))
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {


        LevelManager.currentLevel++;
        MapBoat.Instance.MoveToAnchor(LevelManager.currentLevel);

    }

    public void CheckCompletion()
    {
        if(combatsCompleted >= LevelManager.currentLevel)
        {
            LevelManager.currentLevel = combatsCompleted;
            NextLevel();
        }
        else
        {
            LevelPanel.Instance.Display();
        }
    }

    public void StartGame()
    {
        LevelManager.GoToScene("CombatCompletion");
    }
}
