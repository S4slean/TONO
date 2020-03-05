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
    public Image lifeBar;
    public Image lifePreviewBar;
    public TextMeshProUGUI lifePoints;

    public Image paBar;
    public Image paPreviewBar;
    public TextMeshProUGUI paPoints;

    public Image pmBar;
    public Image pmPreviewBar;
    public TextMeshProUGUI pmPoints;

    bool isVisible = false;


    [Header("Debug")]
    public int totalLife;
    public int currentLife;

    public int totalPA;
    public int currentPA;

    public int totalPM;
    public int currentPM;


    //void Awake()
    //{
    //    ResetAllCharacterInfo();
    //}

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.A))
    //    {
    //        PreviewCharacterInfo(Stats.Life, 6);
    //    }

    //    if (Input.GetKeyUp(KeyCode.A))
    //    {
    //        ResetCharacterInfo(Stats.Life);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        SetCharacterInfoWithCost(Stats.Life, 1);
    //    }


    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        PreviewCharacterInfo(Stats.PA, 2);
    //    }

    //    if (Input.GetKeyUp(KeyCode.Z))
    //    {
    //        ResetCharacterInfo(Stats.PA);
    //    }

    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        SetCharacterInfoWithCost(Stats.PA, 1);
    //    }


    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        PreviewCharacterInfo(Stats.PM, 3);
    //    }

    //    if (Input.GetKeyUp(KeyCode.E))
    //    {
    //        ResetCharacterInfo(Stats.PM);
    //    }

    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        SetCharacterInfoWithCost(Stats.PM, 1);
    //    }
    //}


    public void ShowCharacterInfo()
    {
        if (isVisible)
            return;

        lifeBar.gameObject.SetActive(true);
        lifePreviewBar.gameObject.SetActive(true);

        paBar.gameObject.SetActive(true);
        paPreviewBar.gameObject.SetActive(true);

        pmBar.gameObject.SetActive(true);
        pmPreviewBar.gameObject.SetActive(true);

        isVisible = true;
    }

    public void HideCharacterInfo()
    {
        if (!isVisible)
            return;

        lifeBar.gameObject.SetActive(false);
        lifePreviewBar.gameObject.SetActive(false);

        paBar.gameObject.SetActive(false);
        paPreviewBar.gameObject.SetActive(false);

        pmBar.gameObject.SetActive(false);
        pmPreviewBar.gameObject.SetActive(false);

        isVisible = false;
    }

    public void PreviewCharacterInfoWithCost(Stats concernedStat, int cost)
    {
        float amount = 0;
        float newValue = 0;

        switch (concernedStat)
        {
            case Stats.Life:
                //if(cost > playerCharacterLIFE)
                newValue = (float)currentLife - (float)cost;
                amount = newValue / (float)totalLife;

                lifePreviewBar.fillAmount = amount;
                lifePoints.text = newValue.ToString();
                break;

            case Stats.PA:
                //if(cost > playerCharacterLIFE)
                newValue = (float)currentPA - (float)cost;
                amount = newValue / (float)totalPA;

                paPreviewBar.fillAmount = amount;
                paPoints.text = newValue.ToString();
                break;

            case Stats.PM:
                //if(cost > playerCharacterLIFE)
                newValue = (float)currentPM - (float)cost;
                amount = newValue / (float)totalPM;

                pmPreviewBar.fillAmount = amount;
                pmPoints.text = newValue.ToString();
                break;
        }
    }

    public void SetCharacterInfoWithNewValue(Stats concernedStat, int newValue)
    {
        float amount = 0;

        switch (concernedStat)
        {
            case Stats.Life:
                //if(cost > playerCharacterLIFE)
                currentLife = newValue;
                amount = (float)currentLife / (float)totalLife;

                lifePreviewBar.fillAmount = amount;
                lifeBar.fillAmount = amount;
                lifePoints.text = currentLife.ToString();
                break;

            case Stats.PA:
                //if(cost > playerCharacterLIFE)
                currentPA = newValue;
                amount = (float)currentPA / (float)totalPA;

                paPreviewBar.fillAmount = amount;
                paBar.fillAmount = amount;
                paPoints.text = currentPA.ToString();
                break;

            case Stats.PM:
                //if(cost > playerCharacterLIFE)
                currentPM = newValue;
                amount = (float)currentPM / (float)totalPM;

                pmPreviewBar.fillAmount = amount;
                pmBar.fillAmount = amount;
                pmPoints.text = currentPM.ToString();
                break;
        }
    }

    public void SetCharacterInfoWithCost(Stats concernedStat, int cost)
    {
        float amount = 0;

        switch (concernedStat)
        {
            case Stats.Life:
                //if(cost > playerCharacterLIFE)
                currentLife -= (int)cost;
                amount = (float)currentLife / (float)totalLife;

                lifePreviewBar.fillAmount = amount;
                lifeBar.fillAmount = amount;
                lifePoints.text = currentLife.ToString();
                break;

            case Stats.PA:
                //if(cost > playerCharacterLIFE)
                currentPA -= (int)cost;
                amount = (float)currentPA / (float)totalPA;

                paPreviewBar.fillAmount = amount;
                paBar.fillAmount = amount;
                paPoints.text = currentPA.ToString();
                break;

            case Stats.PM:
                //if(cost > playerCharacterLIFE)
                currentPM -= (int)cost;
                amount = (float)currentPM / (float)totalPM;

                pmPreviewBar.fillAmount = amount;
                pmBar.fillAmount = amount;
                pmPoints.text = currentPM.ToString();
                break;
        }
    }

    public void SetAllCharacterInfo(int life, int pa, int pm)
    {
        float lifeAmount = (float)life / (float)totalLife;
        lifePreviewBar.fillAmount = lifeAmount;
        lifeBar.fillAmount = lifeAmount;
        lifePoints.text = life.ToString();

        float paAmount = (float)pa / (float)totalPA;
        paPreviewBar.fillAmount = paAmount;
        paBar.fillAmount = paAmount;
        paPoints.text = pa.ToString();

        float pmAmount = (float)pm / (float)totalPM;
        pmPreviewBar.fillAmount = pmAmount;
        pmBar.fillAmount = pmAmount;
        pmPoints.text = pm.ToString();
    }

    public void ResetCharacterInfo(Stats concernedStat)
    {
        float amount = 0;

        switch (concernedStat)
        {
            case Stats.Life:
                //if(cost > playerCharacterLIFE)
                amount = (float)currentLife / (float)totalLife;
                lifePreviewBar.fillAmount = amount;
                lifeBar.fillAmount = amount;
                lifePoints.text = currentLife.ToString();
                break;

            case Stats.PA:
                //if(cost > playerCharacterLIFE)
                amount = (float)currentPA / (float)totalPA;
                paPreviewBar.fillAmount = amount;
                paBar.fillAmount = amount;
                paPoints.text = currentPA.ToString();
                break;

            case Stats.PM:
                //if(cost > playerCharacterLIFE)
                amount = (float)currentPM / (float)totalPM;
                pmPreviewBar.fillAmount = amount;
                pmBar.fillAmount = amount;
                pmPoints.text = currentPM.ToString();
                break;
        }

    }

    public void ResetAllCharacterInfo()
    {
        float lifeAmount = (float)currentLife / (float)totalLife;
        lifePreviewBar.fillAmount = lifeAmount;
        lifeBar.fillAmount = lifeAmount;
        lifePoints.text = currentLife.ToString();

        float paAmount = (float)currentPA / (float)totalPA;
        paPreviewBar.fillAmount = paAmount;
        paBar.fillAmount = paAmount;
        paPoints.text = currentPA.ToString();

        float pmAmount = (float)currentPM / (float)totalPM;
        pmPreviewBar.fillAmount = pmAmount;
        pmBar.fillAmount = pmAmount;
        pmPoints.text = currentPM.ToString();
    }
}
