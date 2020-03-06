using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_ActionButton : MonoBehaviour
{
    [Header("IMAGE References")]
    public Image actionImage;
    public Image backgroundImage;

    [Header("RECT References")]
    public RectTransform rect;

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




    private void Update()
    {
        if (isMoving)
            TooltipAnimation();

        //Debug.Log(EventSystem.current.IsPointerOverGameObject() + this.gameObject.name);
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

        Debug.Log("SHOW");

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

        Debug.Log("HIDE");

        isMoving = false;

        current = rect.anchoredPosition3D.x;
        diff = 0 - current;

        isUnfold = true;
        isMoving = true;
    }

    public void DebugAction()
    {
        Debug.Log(gameObject.name);
    }
}
