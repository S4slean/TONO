using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public BrickSettings settings;

    public MeshRenderer mr;

    public void Init()
    {


    }

    public void TakeTypeAppearance()
    {
        if (settings != null)
        {
            mr.material.color = settings.color;
        }
    }
}
