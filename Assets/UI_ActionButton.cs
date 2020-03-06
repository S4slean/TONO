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
    public RectTransform actionRect;
    public RectTransform tooltipRect;

    [Header("TMP References")]
    public TextMeshProUGUI tooltipDescription;
    public TextMeshProUGUI tooltipName;

    [Header("Tooltip Animation")]
    public float maxTime = 0;
    private float currentTime = 0;
    public float tooltipDisplacement = 0;
    private float nextPos = 0;
    private float tooltipWidth = 0;
    public bool isUnfold = false;
    public bool isMoving = false;



    private void Update()
    {
        if (isMoving)
            TooltipAnimation();
    }

    private void TooltipAnimation()
    {
        
    }

    public void ShowTooltip()
    {

    }

    public void HideTooltip()
    {

    }
}
