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

    /// <summary>
    /// /////////GET PLAYER VALUES
    /// </summary>
    [Header("Debug")]
    public int totalLife; //total player LIFE
    public int currentLife; //current player LIFE etc...

    public int totalPA;
    public int currentPA;

    public int totalPM;
    public int currentPM;


    void Awake()
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

        for (int i = 0; i < totalPA; i++)
        {
            GameObject obj = Instantiate(paPointPrefab, Vector3.zero, Quaternion.identity, paParentRect.gameObject.transform);
            RectTransform rect = obj.GetComponent<RectTransform>();

            rect.anchoredPosition3D = new Vector3(paParentRect.sizeDelta.x * (i + 1), 0, 0);

            Image image = obj.GetComponent<Image>();
            image.sprite = UI_Manager.instance.uiPreset.unusedPA;

            paPoints.Add(image);
        }

        for (int i = 0; i < totalPM; i++)
        {
            GameObject obj = Instantiate(pmPointPrefab, Vector3.zero, Quaternion.identity, pmParentRect.gameObject.transform);
            RectTransform rect = obj.GetComponent<RectTransform>();

            rect.anchoredPosition3D = new Vector3(pmParentRect.sizeDelta.x * (i + 1), 0, 0);

            Image image = obj.GetComponent<Image>();
            image.sprite = UI_Manager.instance.uiPreset.unusedPA;


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
                //if(cost > playerCharacterLIFE)
                newValue = currentLife - cost;
                amount = (float)newValue / (float)totalLife;

                lifePreviewBar.fillAmount = amount;
                break;

            case Stats.PA:
                //if(cost > playerCharacterLIFE)
                newValue = currentPA - cost;
                for (int i = 0; i < newValue; i++)
                {
                    paPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
                }

                for (int i = newValue; i < paPoints.Count; i++)
                {
                    paPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)100);
                }
                break;

            case Stats.PM:
                //if(cost > playerCharacterLIFE)
                newValue = currentPM - cost;
                for (int i = 0; i < newValue; i++)
                {
                    pmPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
                }

                for (int i = newValue; i < pmPoints.Count; i++)
                {
                    pmPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)100);
                }
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
                ResetCharacterInfo(Stats.Life);
                break;

            case Stats.PA:
                //if(cost > playerCharacterLIFE)
                currentPA -= (int)cost;
                ResetCharacterInfo(Stats.PA);
                break;

            case Stats.PM:
                //if(cost > playerCharacterLIFE)
                currentPM -= (int)cost;
                ResetCharacterInfo(Stats.PM);
                break;
        }
    }

    public void SetAllCharacterInfo(int life, int pa, int pm)
    {
        currentLife = life;
        currentPA = pa;
        currentPM = pm;

        ResetAllCharacterInfo();
    }

    /// <summary>
    /// Reset a chosen stats to its current value
    /// </summary>
    /// <param name="concernedStat"></param>
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
                break;

            case Stats.PA:
                //if(cost > playerCharacterLIFE)
                for (int i = 0; i < currentPA; i++)
                {
                    paPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
                }

                for (int i = currentPA; i < paPoints.Count; i++)
                {
                    paPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)100);
                }
                break;

            case Stats.PM:
                //if(cost > playerCharacterLIFE)
                for (int i = 0; i < currentPM; i++)
                {
                    pmPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
                }

                for (int i = currentPM; i < pmPoints.Count; i++)
                {
                    pmPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)100);
                }
                break;
        }

    }

    /// <summary>
    /// Reset values to their current after previews
    /// </summary>
    public void ResetAllCharacterInfo()
    {
        float lifeAmount = (float)currentLife / (float)totalLife;
        lifePreviewBar.fillAmount = lifeAmount;
        lifeBar.fillAmount = lifeAmount;

        for (int i = 0; i < currentPA; i++)
        {
            paPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
        }

        for (int i = currentPA; i < paPoints.Count; i++)
        {
            paPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)100);
        }

        for (int i = 0; i < currentPM; i++)
        {
            pmPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
        }

        for (int i = currentPM; i < pmPoints.Count; i++)
        {
            pmPoints[i].color = new Color32((byte)255, (byte)255, (byte)255, (byte)100);
        }
    }
}
