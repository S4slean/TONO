using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades Config", menuName = "ScriptableObjects/Upgrades Config", order = 101)]
public class UpgradesConfig : ScriptableObject
{
    public UpgradeConfig[] configs;
}
