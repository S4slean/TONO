using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public int combatsCompleted;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.Instance.Load(true, SceneType.menu);
    }

    public void NewGame()
    {

    }

    public void Continue()
    {

    }

    public void StartGame()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
