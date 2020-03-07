using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public float duration;
    float count;

    private void Start()
    {
        ao = SceneManager.LoadSceneAsync(LevelManager.sceneToLoadName);
        ao.allowSceneActivation = false;
        count = duration;
    }

    AsyncOperation ao;
    private void Update()
    {
        if (count <= 0) return;

        count -= Time.deltaTime;
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
