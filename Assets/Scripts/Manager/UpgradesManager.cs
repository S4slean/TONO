using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;

    private void Awake()
    {
        Instance = this;

        for(int i = 0; i < config.upgradeDatas.Length; i++)
        {
            config.upgradeDatas[i].index = i;

            for (int j = 0; j < config.upgradeChoices.Length; j++)
            {
                for (int k = 0; k < config.upgradeChoices[j].choices.Length; k++)
                {
                    if(config.upgradeChoices[j].choices[k] == i)
                    {
                        config.upgradeDatas[i].choiceIndex = j;
                    }
                }
            }
        }

        upgradesIntroAnimator.SetBool("shown", false);
    }

    public UpgradesConfig config;

    public PlayerStats playerStats;

    public float[] displayDistancesByAmount;
    public float[] displayUIDistancesByAmount;

    public GameObject upgradeChoiceDisplayUI;
    public GameObject upgradeChoiceDisplay;

    public GameObject displayParent;
    public GameObject displayUIParent;

    public Animator upgradesIntroAnimator;

    
    public void PickUpgrade(UpgradeChoiceDisplayUI clickedUI)
    {
        if (playerStats.upgradesPossessed.Length >= clickedUI.upgradeIndex)
        {
            playerStats.upgradesPossessed[clickedUI.upgradeIndex] = true;
            playerStats.upgradeChoicesMade[clickedUI.upgradeChoiceIndex] = true;
            for(int i = 0; i < currentDisplays.Length; i++)
            {
                currentDisplays[i].Disappear();
            }

            DataManager.Instance.Save(SceneType.map);
        }

        upgradesIntroAnimator.SetBool("shown", false);
        MapManager.Instance.NextLevelIfSet();
    }

    public void CheckUpgrades()
    {
        //player can only pick as many upgrades as levels completed
        int upgradesPossessed = 0;
        for(int i = 0; i < playerStats.upgradesPossessed.Length; i++)
        {
            if(playerStats.upgradesPossessed[i])
            {
                upgradesPossessed++;
            }
        }

        if(upgradesPossessed >= MapManager.Instance.combatsCompleted)
        {
            MapManager.Instance.NextLevelIfSet();
            return;
        }

        //if upgrades don't exist in player stats, create them
        if(playerStats.upgradesPossessed == null)
        {
            SetUpgradeArrays();
        }

        if(playerStats.upgradesPossessed.Length != config.upgradeDatas.Length)
        {
            SetUpgradeArrays();
        }

        List<UpgradeChoice> choicesToDisplay = new List<UpgradeChoice>();

        if(!playerStats.upgradeChoicesMade[0])
        {
            choicesToDisplay.Add(config.upgradeChoices[0]);
        }
        else
        {
            for (int i = 1; i < playerStats.upgradeChoicesMade.Length; i++)
            {
                if(choicesToDisplay.Count >= 2)
                {
                    break;
                }
                if (playerStats.upgradeChoicesMade[i])
                {
                    continue;
                }
                else
                {
                    if (playerStats.upgradesPossessed[config.upgradeChoices[i].dependency])
                    {
                        if(config.upgradeChoices[i].minLevel <= LevelManager.currentLevel)
                        {
                            choicesToDisplay.Add(config.upgradeChoices[i]);
                        }

                    }
                }

            }
        }

        if(choicesToDisplay.Count > 0)
        {
            StartCoroutine(DisplayUpgrade(choicesToDisplay));
        }
        else
        {
            MapManager.Instance.NextLevelIfSet();
        }

    }

    void SetUpgradeArrays()
    {
        print("Setting Upgrades Array");
        playerStats.upgradesPossessed = new bool[config.upgradeDatas.Length];
        playerStats.upgradeChoicesMade = new bool[config.upgradeChoices.Length];
    }

    UpgradeChoiceDisplayUI[] currentDisplays;
    public float displayDistFromCamera;
    
    public float upgradeDisplayDelay;
    public float delayBetweenDisplays;
    public float displaySpinningSpeed;
    public Ease displaySpinningEase;
    public float displaySpinningEaseStrength;
    public float displayInitialRot;

    IEnumerator DisplayUpgrade(List<UpgradeChoice> choices)
    {
        upgradesIntroAnimator.SetBool("shown", true);
        yield return new WaitForSeconds(upgradeDisplayDelay);
        int amount = 0;
        for(int i = 0; i < choices.Count; i++)
        {
            amount += choices[i].choices.Length;
        }

        int count = 0;
        UpgradeData[] datas = new UpgradeData[amount];
        for (int i = 0; i < choices.Count; i++)
        {
            for(int j = 0; j < choices[i].choices.Length; j++)
            {
                datas[count] = config.upgradeDatas[choices[i].choices[j]];
                count++;
            }
        }

        currentDisplays = new UpgradeChoiceDisplayUI[amount];

        float distance = displayDistancesByAmount[amount];
        float minPos = (-1 * (distance * (amount - 1)))/2f;
        float uiDistance = displayUIDistancesByAmount[amount];
        float uiMinPos = (-1 * (uiDistance * (amount - 1))) / 2f;
        for (int i = 0; i < amount; i++)
        {
            UpgradeChoiceDisplayUI newDisplayUI = Instantiate(upgradeChoiceDisplayUI, displayUIParent.transform).GetComponent<UpgradeChoiceDisplayUI>();
            UpgradeChoiceDisplay newDisplay = Instantiate(upgradeChoiceDisplay, displayParent.transform).GetComponent<UpgradeChoiceDisplay>();

            currentDisplays[i] = newDisplayUI;
            newDisplayUI.choiceDisplay = newDisplay;

            RectTransform rt = newDisplayUI.GetComponent<RectTransform>();
            RectTransform parentRT = displayUIParent.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(uiMinPos + (uiDistance * i), parentRT.anchoredPosition.y);

            Vector3 displayPos = new Vector3(0, 0, displayDistFromCamera);
            displayPos.x = minPos + distance * i;
            newDisplay.transform.localPosition = displayPos;
            newDisplay.rotator.localEulerAngles = new Vector3(0, displayInitialRot, 0);
            newDisplay.rotator.DOLocalRotate(Vector3.zero, displaySpinningSpeed).SetEase(displaySpinningEase, displaySpinningEaseStrength);
            newDisplayUI.Initialize(datas[i]);

            yield return new WaitForSeconds(delayBetweenDisplays);
        }

    }

}
