using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_SelectedCharacterInfo : MonoBehaviour
{
    public enum Stats
    {
        Life,
        PA,
        PM,
    }

    [Header("Stats References")]
    public Image portraitImage;

    [Space]
    public Image lifeBar;
    public Image lifePreviewBar;

    [Space]
    public Image paImage;
    public RectTransform paParentRect;
    public GameObject paPointPrefab;
    List<Image> paPoints;
    [Space]
    public Image pmImage;
    public RectTransform pmParentRect;
    public GameObject pmPointPrefab;
    List<Image> pmPoints;

    bool isVisible = false;

    [HideInInspector] public PlayerStats playerStats;
    [HideInInspector] public PlayerCharacter playerCharacter;


    void Start()
    {
        SetUpCharacterInfo();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    PreviewCharacterInfo(Stats.Life, 6);
        //}

        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    ResetCharacterInfo(Stats.Life);
        //}

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    SetCharacterInfoWithCost(Stats.Life, 1);
        //}


        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    PreviewCharacterInfo(Stats.PA, 2);
        //}

        //if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    ResetCharacterInfo(Stats.PA);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SetCharacterInfoWithCost(Stats.PA, 1);
        //}


        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    PreviewCharacterInfo(Stats.PM, 3);
        //}

        //if (Input.GetKeyUp(KeyCode.E))
        //{
        //    ResetCharacterInfo(Stats.PM);
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    SetCharacterInfoWithCost(Stats.PM, 1);
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetUpCharacterInfo();
        //}
    }


    public void SetUpCharacterInfo()
    {
        lifeBar.sprite = UI_Manager.instance.uiPreset.lifeBarImage;
        portraitImage.sprite = UI_Manager.instance.uiPreset.playerPortait;

        paImage.sprite = UI_Manager.instance.uiPreset.paImage;
        pmImage.sprite = UI_Manager.instance.uiPreset.pmImage;

        paPoints = new List<Image>();
        pmPoints = new List<Image>();

        playerStats = GameManager.Instance.overridingPlayerStatsConfig.playerStats;
        playerCharacter = PlayerManager.instance.playerCharacter;

        for (int i = 0; i < playerCharacter.currentPA; i++)
        {
            GameObject obj = Instantiate(paPointPrefab, Vector3.zero, Quaternion.identity, paParentRect.gameObject.transform);
            RectTransform rect = obj.GetComponent<RectTransform>();

            rect.anchoredPosition3D = new Vector3(paParentRect.sizeDelta.x * (i + 1), 0, 0);

            Image image = obj.GetComponent<Image>();
            image.sprite = UI_Manager.instance.uiPreset.unusedPA;

            paPoints.Add(image);
        }

        for (int i = 0; i < playerCharacter.currentPM; i++)
        {
            GameObject obj = Instantiate(pmPointPrefab, Vector3.zero, Quaternion.identity, pmParentRect.gameObject.transform);
            RectTransform rect = obj.GetComponent<RectTransform>();

            rect.anchoredPosition3D = new Vector3(pmParentRect.sizeDelta.x * (i + 1), 0, 0);

            Image image = obj.GetComponent<Image>();
            image.sprite = UI_Manager.instance.uiPreset.unusedPM;


            pmPoints.Add(image);
        }


        lifePreviewBar.fillAmount = 1f;
    }

    public void ShowCharacterInfo()
    {
        if (isVisible)
            return;

        lifeBar.gameObject.SetActive(true);
        lifePreviewBar.gameObject.SetActive(true);

        pmParentRect.gameObject.SetActive(true);
        paParentRect.gameObject.SetActive(true);

        isVisible = true;
    }

    public void HideCharacterInfo()
    {
        if (!isVisible)
            return;

        lifeBar.gameObject.SetActive(false);
        lifePreviewBar.gameObject.SetActive(false);

        pmParentRect.gameObject.SetActive(false);
        paParentRect.gameObject.SetActive(false);

        isVisible = false;
    }

    public void PreviewCharacterInfo(Stats concernedStat, int cost)
    {
        float amount = 0;
        int newValue = 0;

        switch (concernedStat)
        {
            case Stats.Life:
                newValue = playerCharacter.currentLife - cost;
                amount = (float)newValue / (float)playerStats.startingLP;

                lifePreviewBar.fillAmount = amount;
                break;

            case Stats.PA:
                newValue = playerCharacter.currentPA - cost;
                for (int i = 0; i < newValue; i++)
                {
                    paPoints[i].sprite = UI_Manager.instance.uiPreset.unusedPA;
                }

                for (int i = newValue; i < paPoints.Count; i++)
                {
                    paPoints[i].sprite = UI_Manager.instance.uiPreset.usedPA;
                }
                break;

            case Stats.PM:
                newValue = playerCharacter.currentPM - cost;
                for (int i = 0; i < newValue; i++)
                {
                    pmPoints[i].sprite = UI_Manager.instance.uiPreset.unusedPM;
                }

                for (int i = newValue; i < pmPoints.Count; i++)
                {
                    pmPoints[i].sprite = UI_Manager.instance.uiPreset.usedPM;
                }
                break;
        }
    }

    public void SetCharacterInfoWithCost(Stats concernedStat, int cost)
    {
        switch (concernedStat)
        {
            case Stats.Life:
                playerCharacter.currentLife -= (int)cost;
                RefreshCharacterInfo(Stats.Life);
                break;

            case Stats.PA:
                playerCharacter.currentPA -= (int)cost;
                RefreshCharacterInfo(Stats.PA);
                break;

            case Stats.PM:
                playerCharacter.currentPM -= (int)cost;
                RefreshCharacterInfo(Stats.PM);
                break;
        }
    }

    public void SetAllCharacterInfo(int life, int pa, int pm)
    {
        playerCharacter.currentLife = life;
        playerCharacter.currentPA = pa;
        playerCharacter.currentPM = pm;

        RefreshAllCharacterInfo();
    }

    /// <summary>
    /// Reset a chosen stats to its current value
    /// </summary>
    /// <param name="concernedStat"></param>
    public void RefreshCharacterInfo(Stats concernedStat)
    {
        float amount = 0;

        switch (concernedStat)
        {
            case Stats.Life:
                amount = (float)playerCharacter.currentLife / (float)playerStats.startingLP;
                lifePreviewBar.fillAmount = amount;
                lifeBar.fillAmount = amount;
                break;

            case Stats.PA:
                for (int i = 0; i < playerCharacter.currentPA; i++)
                {
                    paPoints[i].sprite = UI_Manager.instance.uiPreset.unusedPA;
                }

                for (int i = playerCharacter.currentPA; i < paPoints.Count; i++)
                {
                    paPoints[i].sprite = UI_Manager.instance.uiPreset.usedPA;
                }
                break;

            case Stats.PM:
                for (int i = 0; i < playerCharacter.currentPM; i++)
                {
                    pmPoints[i].sprite = UI_Manager.instance.uiPreset.unusedPM;
                }

                for (int i = playerCharacter.currentPM; i < pmPoints.Count; i++)
                {
                    pmPoints[i].sprite = UI_Manager.instance.uiPreset.usedPM;
                }
                break;
        }

    }

    /// <summary>
    /// Reset values to their current after previews
    /// </summary>
    public void RefreshAllCharacterInfo()
    {
        float lifeAmount = (float)playerCharacter.currentLife / (float)playerStats.startingLP;
        lifePreviewBar.fillAmount = lifeAmount;
        lifeBar.fillAmount = lifeAmount;

        for (int i = 0; i < playerCharacter.currentPA; i++)
        {
            paPoints[i].sprite = UI_Manager.instance.uiPreset.unusedPA;
        }

        for (int i = playerCharacter.currentPA; i < paPoints.Count; i++)
        {
            paPoints[i].sprite = UI_Manager.instance.uiPreset.usedPA;
        }

        for (int i = 0; i < playerCharacter.currentPM; i++)
        {
            pmPoints[i].sprite = UI_Manager.instance.uiPreset.unusedPM;
        }

        for (int i = playerCharacter.currentPM; i < pmPoints.Count; i++)
        {
            pmPoints[i].sprite = UI_Manager.instance.uiPreset.usedPM;
        }
    }
}
