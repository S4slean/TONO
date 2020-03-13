using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public int combatsCompleted;
    public TextMeshProUGUI combatsCompletedTextMesh;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.Instance.Load(true, SceneType.menu);

        combatsCompletedTextMesh.text = "Combats Completed : " + combatsCompleted.ToString();
    }

    public void NewGame()
    {
        combatsCompleted = 0;
        LevelManager.playedCinematic = 1;
        StartGame();
    }

    public void Continue()
    {
        StartGame();
    }

    public void StartGame()
    {
        DataManager.Instance.Save(SceneType.menu);
        LevelManager.GoToScene("Map");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
