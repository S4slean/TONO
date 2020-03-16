using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseManager : MonoBehaviour
{
    [Header("Pause parameters")]
    public KeyCode pauseInput;

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
    }


    public void Initialize()
    {
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
        PlayerManager.instance.hoverMode = HoverMode.NoHover;
        SkillManager.instance.currentActiveSkill = null;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        PauseMenu.Instance.ExitAllButtons();
        Time.timeScale = 1;
        PlayerManager.instance.hoverMode = HoverMode.MovePath;
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

}
