using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDisplayer : MonoBehaviour
{
    public SpriteRenderer sr;
    public UpgradeData upgradeData;

    public void Initialize(UpgradeData data)
    {
        upgradeData = data;
    }
}
