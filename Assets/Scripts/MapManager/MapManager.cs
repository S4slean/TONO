using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }
}
