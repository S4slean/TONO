using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    private void Awake()
    {
        Instance = this;

    }

    public LevelList levelList;

    GameObject level;
    GameObject background;

    public void LoadLevel()
    {
        level = levelList.levelDatas[LevelManager.currentLevel].level;
        background = levelList.levelDatas[LevelManager.currentLevel].background;

        Instantiate(level, Vector3.zero, Quaternion.identity);
        Instantiate(background, Vector3.zero, Quaternion.identity);
    }
}
