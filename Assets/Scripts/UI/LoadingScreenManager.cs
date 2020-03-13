using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    private void Awake()
    {
        Instance = this;

        if (duration <= 0f) duration = 0.1f;
        duration += endThreshold;
        duration += cinematicDelay;
    }

    public bool loads;

    public float duration;
    public float endThreshold;
    public float count;
    public float cinematicDelay;
    bool checkedCinematic;
    bool ended;

    private void Start()
    {
        checkedCinematic = false;

        if(loads)
        {
            if (LevelManager.sceneToLoadName != "")
            {
                ao = SceneManager.LoadSceneAsync(LevelManager.sceneToLoadName);
                ao.allowSceneActivation = false;
            }
        }


        count = duration;
    }

    AsyncOperation ao;
    private void Update()
    {


        if (count <= 0) return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            count = 1f;
        }
        count -= Time.deltaTime;

        if(!checkedCinematic)
        {
            if(count <= duration - cinematicDelay)
            {
                checkedCinematic = true;
                CinematicManager.Instance.CheckCinematic();
            }

            return;
        }

        if(!ended)
        {
            if(count <= endThreshold)
            {
                ended = true;
                CinematicManager.Instance.EndCinematic();
            }

            return;
        }

        if(count <= 0)
        {
            if(loads)
            LoadScene();
        }
    }

    void LoadScene()
    {
        ao.allowSceneActivation = true;
    }
}
