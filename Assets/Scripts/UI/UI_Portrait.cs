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
    public EnemieBehaviour selectedEnnemy;

    [Header("Animation parameters")]
    public AnimationCurve selectedIconCurve;
    public float selectedMaxTime;
    float selectedCurrentTime;

    public AnimationCurve removeIconsCurve;
    public float removeMaxTime;
    float removeCurrentTime;

    public Vector3 removedPos;
    public Vector3 selectedPos;
    public Vector3 normalPos;

    public bool isSelected;
    public bool isRemoved;
    bool isMoving;

    Vector3 current;
    Vector3 diff;

    [Header("Button Animation")]
    public Animator animator;




    private void Update()
    {
        if (isMoving)
            MoveIcon();
    }


    public void Highlighting()
    {
        animator.Play("Highlighted");
    }

    public void Clicking()
    {
        animator.Play("Clicking");
    }

    public void BackToNormal()
    {
        animator.Play("Normal");
    }

    private void MoveIcon()
    {
        if (isSelected)
        {
            if (selectedCurrentTime < selectedMaxTime)
            {
                selectedCurrentTime += Time.deltaTime;
                float percent = selectedIconCurve.Evaluate(selectedCurrentTime / selectedMaxTime);

                portraitRect.anchoredPosition3D = new Vector3(portraitRect.anchoredPosition3D.x, current.y + (diff.y * percent), portraitRect.anchoredPosition3D.z);
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

                portraitRect.anchoredPosition3D = new Vector3(portraitRect.anchoredPosition3D.x, current.y + (diff.y * percent), portraitRect.anchoredPosition3D.z);
            }
            else
            {
                isMoving = false;
                selectedCurrentTime = 0;
            }
        }

        if (isRemoved)
        {
            if (removeCurrentTime < removeMaxTime)
            {
                Debug.Log("RemovING");

                removeCurrentTime += Time.deltaTime;
                float percent = removeIconsCurve.Evaluate(removeCurrentTime / removeMaxTime);

                //Remove ICON
                portraitRect.anchoredPosition3D = new Vector3(portraitRect.anchoredPosition3D.x, current.y + (diff.y * percent), portraitRect.anchoredPosition3D.z);
            }

            if (removeCurrentTime >= removeMaxTime)
            {
                Debug.Log("Remove ends NOW");
                panelRef.RefreshSelectedIcon(indexOrder);
                isMoving = false;
                removeCurrentTime = 0;
            }
        }
    }

    public void RemoveIcon()
    {
        if (isMoving || isRemoved)
        {
            return;
        }

        Debug.Log("Remove this icon : " + gameObject.name);

        current = portraitRect.anchoredPosition3D;
        diff = new Vector3(removedPos.x - current.x, removedPos.y - current.y, current.z);

        isRemoved = true;
        isMoving = true;
    }

    /// <summary>
    /// Selection SetUp
    /// </summary>
    /// <param name="selectedIndex"></param>
    public void MoveSelectedIcon()
    {
        if (isRemoved || isSelected)
            return;

        isMoving = false;

        current = portraitRect.anchoredPosition3D;
        diff = new Vector3(selectedPos.x - current.x, selectedPos.y - current.y, current.z);

        isSelected = true;
        isMoving = true;
    }

    public void MoveBackIcon()
    {

        if (isRemoved || !isSelected)
            return;

        isMoving = false;

        current = portraitRect.anchoredPosition3D;
        diff = new Vector3(normalPos.x - current.x, normalPos.y - current.y, current.z);

        isSelected = false;
        isMoving = true;
    }
}