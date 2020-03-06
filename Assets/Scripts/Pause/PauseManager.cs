using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    public KeyCode pauseInput;

    public static PauseManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        PauseMenu.Instance.pauseMenuUI.SetActive(false);

        canPause = true;
    }



    public bool canPause;
    public bool paused;

    public void Pause()
    {
        if (!canPause) return;

        PauseMenu.Instance.ExitAllButtons();

        PauseMenu.Instance.pauseMenuUI.SetActive(true);

        paused = true;
        AudioListener.pause = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        PauseMenu.Instance.ExitAllButtons();
        Time.timeScale = 1;
        AudioListener.pause = false;
        PauseMenu.Instance.pauseMenuUI.SetActive(false);
        paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(pauseInput))
        {
            TogglePause();
        }

    }

    public Action OnGamePause;

    public void TogglePause()
    {
        if (paused) Unpause();
        else
        {
            Pause();
        }
    }

}
