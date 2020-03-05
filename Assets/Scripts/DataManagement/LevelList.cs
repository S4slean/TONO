using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level List", menuName = "ScriptableObjects/Level List", order = 2)]
public class LevelList : ScriptableObject
{
    public LevelData[] levelDatas;
}
