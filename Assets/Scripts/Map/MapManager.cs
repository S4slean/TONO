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

        MapBoat.Instance.Place(LevelManager.currentLevel);
        LevelPanel.Instance.HideImmediately();
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
}
