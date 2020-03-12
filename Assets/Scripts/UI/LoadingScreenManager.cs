using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    public float duration;
    public float count;
    public float cinematicDelay;
    bool checkedCinematic;

    private void Start()
    {
        checkedCinematic = false;
        ao = SceneManager.LoadSceneAsync(LevelManager.sceneToLoadName);
        ao.allowSceneActivation = false;
        count = duration;
    }

    AsyncOperation ao;
    private void Update()
    {
        if (count <= 0) return;

        count -= Time.deltaTime;

        if(!checkedCinematic)
        {
            if(count <= cinematicDelay)
            {
                checkedCinematic = true;
                CinematicManager.Instance.CheckCinematic();
            }
        }

        if(count <= 0)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        ao.allowSceneActivation = true;
    }
}
