using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "FX Config", menuName = "ScriptableObjects/FX Config", order = 300)]
public class FXConfig : ScriptableObject
{
    public FXData[] fxs;
}
