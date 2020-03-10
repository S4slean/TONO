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


    public bool hovered;

    private void OnMouseEnter()
    {
        hovered = true;
        UpgradesDescriptionDisplayer.Instance.displayedData = upgradeData;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }
}
