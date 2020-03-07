using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_ActionButton : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject paCostPrefab;
    float costPrefabHeightSize = 0;

    [Header("IMAGE References")]
    public Image actionImage;
    public Image backgroundImage;
    List<Image> costPoints;

    [Header("RECT References")]
    public RectTransform rect;
    public RectTransform costParent;

    [Header("TMP References")]
    public TextMeshProUGUI tooltipDescription;
    public TextMeshProUGUI tooltipName;

    [Header("Tooltip Animation")]
    public AnimationCurve tooltipCurve;
    public float animTime = 0;
    private float currentTime = 0;

    public bool isUnfold = false;
    public bool isMoving = false;

    private float current;
    private float diff;

    public float unfoldPos = 300f;
    public int actionCost;

    public Sprite enabled;
    public Sprite unenabled;



    private void Awake()
    {
        costPrefabHeightSize = paCostPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void Update()
    {
        if (isMoving)
            TooltipAnimation();
    }

    public void CheckSkillCondition()
    {

    }

    public void CheckPlayerPA(int currentPACompared)
    {
        if (currentPACompared < actionCost)
        {
            for (int i = 0; i < currentPACompared; i++)
            {
                costPoints[i].sprite = enabled;
            }

            for (int i = currentPACompared; i < actionCost; i++)
            {
                costPoints[i].sprite = unenabled;
            }
        }
        else
        {
            for (int i = 0; i < costPoints.Count; i++)
            {
                costPoints[i].sprite = enabled;
            }
        }
    }

    private void TooltipAnimation()
    {
        if (isMoving)
        {
            if (isUnfold)
            {
                Debug.Log("FOLD");

                if (currentTime > 0)
                {
                    currentTime -= Time.deltaTime;

                    float percent = tooltipCurve.Evaluate(currentTime / animTime);

                    rect.anchoredPosition3D = new Vector3((0 - (diff * percent)), rect.anchoredPosition3D.y, rect.anchoredPosition3D.z);
                }
                else
                {
                    isMoving = false;
                    currentTime = 0;
                }
            }
            else
            {
                Debug.Log("unFOLD");

                if (currentTime < animTime)
                {
                    currentTime += Time.deltaTime;

                    float percent = tooltipCurve.Evaluate(currentTime / animTime);

                    rect.anchoredPosition3D = new Vector3(current + (diff * percent), rect.anchoredPosition3D.y, rect.anchoredPosition3D.z);
                }
                else
                {
                    isMoving = false;
                    currentTime = animTime;
                }
            }
        }
    }

    public void ShowTooltip()
    {
        if (!isUnfold)
            return;

        isMoving = false;

        current = rect.anchoredPosition3D.x;
        diff = unfoldPos - current;

        isUnfold = false;
        isMoving = true;
    }

    public void HideTooltip()
    {
        if (isUnfold)
            return;

        isMoving = false;

        current = rect.anchoredPosition3D.x;
        diff = 0 - current;

        isUnfold = true;
        isMoving = true;
    }

    public void SetUpActionCost()
    {
        costParent.anchoredPosition3D = new Vector3(costParent.anchoredPosition3D.x, (costPrefabHeightSize * 0.5f) * -(actionCost - 1), costParent.anchoredPosition3D.z);
        costPoints = new List<Image>();

        for (int i = 0; i < actionCost; i++)
        {
            GameObject costObj = Instantiate(paCostPrefab, Vector3.zero, Quaternion.identity, costParent.transform);
            RectTransform costRect = costObj.GetComponent<RectTransform>();
            Image costImage = costObj.GetComponent<Image>();


            costRect.anchoredPosition3D = new Vector3(0, costPrefabHeightSize * i, 0);
            costPoints.Add(costImage);
        }
    }

    public void DebugAction()
    {
        Debug.Log(gameObject.name);
    }
}
