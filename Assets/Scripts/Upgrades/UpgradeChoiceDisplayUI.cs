using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeChoiceDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI nameTextMesh;
    public TextMeshProUGUI descriptionTextMesh;
    public int upgradeIndex;
    public int upgradeChoiceIndex;

    public UpgradeChoiceDisplay choiceDisplay;

    public void ButtonClicked()
    {
        UpgradesManager.Instance.PickUpgrade(this);


    }

    public void Initialize(UpgradeData data)
    {
        upgradeChoiceIndex = data.choiceIndex;
        upgradeIndex = data.index;
        nameTextMesh.text = data.name;
        descriptionTextMesh.text = data.description;
    }

    public void ButtonHovered()
    {
        choiceDisplay.hovered = true;
    }


    public void ButtonExited()
    {
        choiceDisplay.hovered = false;
    }

    public void Disappear()
    {
        choiceDisplay.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
