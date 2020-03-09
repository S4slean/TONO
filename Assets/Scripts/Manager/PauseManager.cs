using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseManager : Panel_Behaviour
{
    [Header("Pause parameters")]
    public KeyCode pauseInput;
    public Image pauseImage;
    public Action OnGamePause;

    public bool canPause;
    public bool paused;

    public static PauseManager Instance;


    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyUp(pauseInput))
        {
            TogglePause();
        }

        MovePanel();
    }


    public void Initialize()
    {
        pauseImage.sprite = UI_Manager.instance.uiPreset.pauseImage;

        PauseMenu.Instance.pauseMenuUI.SetActive(false);

        canPause = true;
    }

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

    public void TogglePause()
    {
        if (paused) Unpause();
        else
        {
            Pause();
        }
    }


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void MovePanel()
    {
        base.MovePanel();
    }
}
