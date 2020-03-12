using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectedCharacterInfo : Panel_Behaviour
{
    public enum Stats
    {
        Life,
        PA,
        PM,
    }

    [Header("Stats References")]
    public Image portraitBackgroundImage;
    public Image portraitImage;

    [Space]
    public Image lifeBar;
    public Image lifeBarBackground;
    public RectTransform lifeParentRect;
    public GameObject lifeLimitPrefab;
    public Image lifePreviewBar;
    float lifePart;

    public float pointsSpacing;

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

    [HideInInspector] public PlayerStats playerStats;
    [HideInInspector] public PlayerCharacter playerCharacter;



    private void Start()
    {
        //SetUpCharacterInfo();
    }

    void Update()
    {
        MovePanel();
    }


    /// <summary>
    /// Get player character's data (player stats)
    /// </summary>
    public void CreateAndSetAllCharacterInfo()
    {
        lifeBar.sprite = UI_Manager.instance.uiPreset.lifeBarImage;
        lifeBarBackground.sprite = UI_Manager.instance.uiPreset.lifeBarBackgroundImage;
        portraitImage.sprite = UI_Manager.instance.uiPreset.playerPortait;

        paImage.sprite = UI_Manager.instance.uiPreset.paImage;
        pmImage.sprite = UI_Manager.instance.uiPreset.pmImage;

        paPoints = new List<Image>();
        pmPoints = new List<Image>();

        playerStats = GameManager.Instance.overridingPlayerStatsConfig.playerStats;
        playerCharacter = PlayerManager.instance.playerCharacter;

        //Set number of PA
        for (int i = 0; i < playerCharacter.currentPA; i++)
        {
            GameObject paObj = Instantiate(paPointPrefab, Vector3.zero, Quaternion.identity, paParentRect.gameObject.transform);
            RectTransform rect = paObj.GetComponent<RectTransform>();

            rect.anchoredPosition3D = new Vector3(paParentRect.sizeDelta.x * i + pointsSpacing * i, 0, 0);

            Image image = paObj.GetComponent<Image>();
            image.sprite = UI_Manager.instance.uiPreset.unusedPA;

            paPoints.Add(image);
        }

        //Set number of PM
        for (int i = 0; i < playerCharacter.currentPM; i++)
        {
            GameObject pmObj = Instantiate(pmPointPrefab, Vector3.zero, Quaternion.identity, pmParentRect.gameObject.transform);
            RectTransform rect = pmObj.GetComponent<RectTransform>();

            rect.anchoredPosition3D = new Vector3(pmParentRect.sizeDelta.x * i + pointsSpacing * i, 0, 0);

            Image image = pmObj.GetComponent<Image>();
            image.sprite = UI_Manager.instance.uiPreset.unusedPM;


            pmPoints.Add(image);
        }

        //Set number of life parts
        RectTransform lifeBarRect = lifeBarBackground.gameObject.GetComponent<RectTransform>();
        lifePart = (float)lifeBarRect.sizeDelta.x / ((float)playerStats.startingLP + 1f);
        float decal = lifePart * 0.75f;

        for (int i = 0; i < playerStats.startingLP - 1; i++)
        {
            GameObject obj = Instantiate(lifeLimitPrefab, Vector3.zero, Quaternion.identity, lifeParentRect.gameObject.transform);
            RectTransform limitRect = obj.GetComponent<RectTransform>();
            Image image = obj.GetComponent<Image>();

            image.sprite = UI_Manager.instance.uiPreset.lifeBarLimitImage;
            limitRect.anchoredPosition3D = new Vector3(decal + lifePart * i, -21.5f, 0);
        }

        float lifeAmount = (float)playerCharacter.currentLife / (float)playerStats.startingLP;

        lifePreviewBar.fillAmount = lifeAmount;
    }

    /// <summary>
    /// Preview a CHOSEN stat
    /// </summary>
    /// <param name="concernedStat">chosen stat</param>
    /// <param name="cost">withdrawned value</param>
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

    /// <summary>
    /// Set a CHOSEN stat
    /// </summary>
    /// <param name="concernedStat">chosen stat</param>
    /// <param name="cost">withdrawned value</param>
    public void SetCharacterInfoWithCost(Stats concernedStat, int cost)
    {
        switch (concernedStat)
        {
            case Stats.Life:
                playerCharacter.currentLife -= (int)cost;
                ResetCharacterInfo(Stats.Life);
                break;

            case Stats.PA:
                playerCharacter.currentPA -= (int)cost;
                ResetCharacterInfo(Stats.PA);
                break;

            case Stats.PM:
                playerCharacter.currentPM -= (int)cost;
                ResetCharacterInfo(Stats.PM);
                break;
        }
    }

    /// <summary>
    /// Set ALL character's info at once
    /// </summary>
    /// <param name="life"> New value of LIFE</param>
    /// <param name="pa">New value of PA</param>
    /// <param name="pm">New value of PM</param>
    public void SetAllCharacterInfo(int life, int pa, int pm)
    {
        playerCharacter.currentLife = life;
        playerCharacter.currentPA = pa;
        playerCharacter.currentPM = pm;

        ResetAllCharacterInfo();
    }

    /// <summary>
    /// Reset a chosen stats to its current value
    /// </summary>
    /// <param name="concernedStat">concerned stat</param>
    public void ResetCharacterInfo(Stats concernedStat)
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
    public void ResetAllCharacterInfo()
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


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void MovePanel()
    {
        base.MovePanel();
    }
}
