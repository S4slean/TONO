using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Barrel Config", menuName = "ScriptableObjects/BarrelConfig", order = 200)]
public class BarrelConfig : ScriptableObject
{
    public BarrelType[] barrelTypes;
}
