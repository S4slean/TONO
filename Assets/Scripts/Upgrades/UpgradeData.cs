using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrades Data", menuName = "ScriptableObjects/Upgrade Data", order = 101)]
public class UpgradeData : ScriptableObject
{
    [Header("Properties")]
    public string description;
    public string tooltip;
    public Color color;

    [Header("Indexation")]
    public int index;
    public int choiceIndex;
}
