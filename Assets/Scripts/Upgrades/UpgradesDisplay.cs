using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesDisplay : MonoBehaviour
{
    public static UpgradesDisplay Instance;

    private void Awake()
    {
        Instance = this;
    }

    public UpgradeDisplayer[] upgradeDisplayers;
    public int amountDisplayed;

    public void Initialize()
    {
        for(int i = 0; i < upgradeDisplayers.Length; i++)
        {
            upgradeDisplayers[i].gameObject.SetActive(false);
        }

        List<int> toDisplay = new List<int>();

        
        for(int i = 0; i < UpgradesManager.Instance.playerStats.upgradeChoicesMade.Length; i++)
        {
            if(UpgradesManager.Instance.playerStats.upgradeChoicesMade[i])
            {
                for(int j = 0; j < UpgradesManager.Instance.config.upgradeDatas.Length; j++)
                {
                    if(UpgradesManager.Instance.config.upgradeDatas[j].choiceIndex == i)
                    {
                        if(UpgradesManager.Instance.playerStats.upgradesPossessed[j])
                        {
                            AddDisplay(UpgradesManager.Instance.config.upgradeDatas[j]);
                        }
                    }
                }
            }
        }
    }

    public void AddDisplay(UpgradeData data)
    {
        if(amountDisplayed >= upgradeDisplayers.Length)
        {
            Debug.LogError("All Upgrades Displayers already taken !");
            return;
        }

        upgradeDisplayers[amountDisplayed].gameObject.SetActive(true);
        upgradeDisplayers[amountDisplayed].Initialize(data);
        amountDisplayed++;
    }
}
