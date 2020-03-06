using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    public GameObject level;
    public LevelTheme theme;
}
