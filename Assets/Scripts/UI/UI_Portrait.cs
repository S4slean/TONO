using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Portrait : MonoBehaviour
{
    [Header("Portrait References")]
    public RectTransform portraitRect;
    public UI_Timeline panelRef;

    public Image backgroundImage;
    public Image stickImage;
    public Image portraitImage;

    public int indexOrder = 0;

    [Header("Animation parameters")]
    public AnimationCurve selectedIconCurve;
    public float selectedDisplacement = 20;
    public float selectedMaxTime;
    float selectedCurrentTime;

    public AnimationCurve removeIconsCurve;
    public float removeDisplacement = 50;
    public float removeMaxTime;
    float removeCurrentTime;

    public bool isSelected;
    public bool isRemoved;
    bool isMoving;

    private float current;
    private float diff;




    private void Update()
    {
        if (isMoving)
            MoveIcon();
    }

    private void MoveIcon()
    {
        if (isSelected)
        {
            if (selectedCurrentTime < selectedMaxTime)
            {
                selectedCurrentTime += Time.deltaTime;
                float percent = selectedIconCurve.Evaluate(selectedCurrentTime / selectedMaxTime);

                portraitRect.anchoredPosition3D = new Vector3(portraitRect.anchoredPosition3D.x, current - (diff * percent), portraitRect.anchoredPosition3D.z);
            }
            else
            {
                isMoving = false;
                selectedCurrentTime = 0;
            }
        }
        else
        {
            if (selectedCurrentTime < selectedMaxTime)
            {
                selectedCurrentTime += Time.deltaTime;
                float percent = selectedIconCurve.Evaluate(selectedCurrentTime / selectedMaxTime);

                portraitRect.anchoredPosition3D = new Vector3(portraitRect.anchoredPosition3D.x, current + (diff * percent), portraitRect.anchoredPosition3D.z);
            }
            else
            {
                isMoving = false;
                selectedCurrentTime = 0;
            }
        }

        if (isRemoved)
        {
            Debug.Log("Removing");

            if (removeCurrentTime < removeMaxTime)
            {
                removeCurrentTime += Time.deltaTime;
                float percent = removeIconsCurve.Evaluate(removeCurrentTime / removeMaxTime);

                //Remove ICON
                portraitRect.anchoredPosition3D = new Vector3(portraitRect.anchoredPosition3D.x, current + (diff * percent), portraitRect.anchoredPosition3D.z);
            }
            else
            {
                Debug.Log("End of Removing");

                isMoving = false;
                removeCurrentTime = 0;
                panelRef.RefreshSelectedIcon(indexOrder);
            }
        }
    }

    public void RemoveIcon()
    {
        if (isMoving || isRemoved)
        {
            return;
        }

        current = portraitRect.anchoredPosition3D.y;

        if (isSelected)
            diff = (removeDisplacement + selectedDisplacement);
        else
            diff = removeDisplacement;

        isRemoved = true;
        isMoving = true;
    }

    /// <summary>
    /// Selection SetUp
    /// </summary>
    /// <param name="selectedIndex"></param>
    public void MoveSelectedIcon()
    {
        isSelected = false;
        isMoving = false;

        current = portraitRect.anchoredPosition3D.y;
        float nextValue = 0;

        nextValue = current + selectedDisplacement;

        diff = nextValue - current;

        isSelected = true;
        isMoving = true;
    }

    public void MoveBackIcon()
    {
        if (isMoving || !isSelected)
            return;

        current = portraitRect.anchoredPosition3D.y;
        float nextValue = 0;

        nextValue = current + selectedDisplacement;

        diff = nextValue - current;

        isSelected = false;
        isMoving = true;
    }
}