using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UpgradeChoice
{
    public int minLevel;
    public int parentChoiceIndex;
    public int dependency;
    public int[] choices;
}
