using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public int combatsCompleted;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.Instance.Load(true, SceneType.map);
    }
}
