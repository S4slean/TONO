using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Barrel : MonoBehaviour
{
    [Header("Portrait References")]
    public RectTransform barrelRect;
    public Image barrelImage;

    [Header("Animation parameters")]
    public AnimationCurve selectedIconCurve;
    public float selectedAnimTime;
    float selectedCurrentTime;

    public AnimationCurve removeIconsCurve;
    public float removeAnimTime;
    float removeCurrentTime;

    bool isSelected;
    bool isRemoved;
    bool isMoving;

    Vector3 current;
    Vector3 diff;

    public Vector3 removedPlacement;
    public Vector3 selectedPlacement;



    private void Update()
    {
        if(isMoving)
        {
            BarrelAnimation();
        }
    }


    private void BarrelAnimation()
    {
        if (isSelected)
        {
            if (selectedCurrentTime < selectedAnimTime)
            {
                selectedCurrentTime += Time.deltaTime;
                float percent = selectedIconCurve.Evaluate(selectedCurrentTime / selectedAnimTime);

                barrelRect.anchoredPosition3D = new Vector3(current.x + (diff.x * percent), current.y + (diff.y * percent), barrelRect.anchoredPosition3D.z);
            }
            else
            {
                isMoving = false;
                selectedCurrentTime = 0;
            }
        }

        if (isRemoved)
        {
            if (removeCurrentTime < removeAnimTime)
            {
                removeCurrentTime += Time.deltaTime;
                float percent = removeIconsCurve.Evaluate(removeCurrentTime / removeAnimTime);

                //Remove ICON
                barrelRect.anchoredPosition3D = new Vector3(current.x + (diff.x * percent), current.y + (diff.y * percent), barrelRect.anchoredPosition3D.z);
            }
            else
            {
                isMoving = false;
                removeCurrentTime = 0;
            }
        }
    }

    public void RemoveBarrel()
    {
        if (isMoving || isRemoved)
        {
            return;
        }

        isMoving = false;
        isSelected = false;

        //Set up Icon Animation values
        current = barrelRect.anchoredPosition3D;
        diff = new Vector3(removedPlacement.x - current.x, removedPlacement.y - current.y, 0);

        isRemoved = true;
        isMoving = true;
    }

    /// <summary>
    /// Selection SetUp
    /// </summary>
    /// <param name="selectedIndex"></param>
    public void MoveSelectedBarrel()
    {
        if (isMoving || isSelected)
            return;

        isMoving = false;

        //Set up Icon Animation values
        current = barrelRect.anchoredPosition3D;
        diff = new Vector3(selectedPlacement.x - current.x, selectedPlacement.y - current.y, 0);

        isSelected = true;
        isMoving = true;
    }
}
