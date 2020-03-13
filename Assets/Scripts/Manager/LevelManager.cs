using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static LevelProgress[] levelProgresses;

    public static void InitializeLevelProgresses(LevelList levelList)
    {
        levelProgresses = new LevelProgress[levelList.levelDatas.Length];
    }

    public static string sceneToLoadName;
    public static int currentLevel;
    public static int playedCinematic;

    public static void GoToScene(string sceneName)
    {
        sceneToLoadName = sceneName;
        if(FadingScreen.Instance)
        {
            FadingScreen.Instance.FadeInThenLoad(sceneName);
        }
        else
        {
            SceneManager.LoadScene("NEW_LoadingScreen");
        }

    }


    public static void GoToSceneDirectly(string sceneName)
    {
        sceneToLoadName = sceneName;
        SceneManager.LoadScene("NEW_LoadingScreen");
    }

    public static IEnumerator ReloadCurrentLevel(float delayBeforeReloading)
    {
        yield return new WaitForSeconds(delayBeforeReloading);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
