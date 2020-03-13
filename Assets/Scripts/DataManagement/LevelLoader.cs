using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public LevelList levelList;

    GameObject level;
    GameObject background;

    public void LoadLevel()
    {
        level = levelList.levelDatas[LevelManager.currentLevel].level;
        background = levelList.levelDatas[LevelManager.currentLevel].background;
    }
}
