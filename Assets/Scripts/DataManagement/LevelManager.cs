using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager
{
    public static LevelProgress[] levelProgresses;

    public static void InitializeLevelProgresses(LevelList levelList)
    {
        levelProgresses = new LevelProgress[levelList.levelDatas.Length];
    }
}

public enum LevelTheme
{
    dark,
    bright
}
