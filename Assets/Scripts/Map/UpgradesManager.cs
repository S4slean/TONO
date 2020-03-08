using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public UpgradesConfig config;

    public PlayerStats playerStats;

    public void CheckUpgrades()
    {
        List<UpgradeChoice> choicesToDisplay = new List<UpgradeChoice>();

        bool found = false;
        for(int i = 0; i < playerStats.upgradeChoicesMade.Length; i++)
        {
            if(playerStats.upgradeChoicesMade[i])
            {
                continue;
            }
            else
            {
                if(playerStats.upgradeChoicesMade[config.choices[i].dependency])
                {
                    found = true;
                    choicesToDisplay.Add(config.choices[i]);
                }
            }

            break;
        }
        if (!found) return;

        //DisplayUpgrade(toFind);
    }


}
