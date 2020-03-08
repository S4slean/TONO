using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Behaviour : MonoBehaviour
{
    [Header("Panel Animation")]
    public RectTransform panelRect;
    public AnimationCurve showAndHideIconsCurve;
    public float showAndHideAnimTime;
    float showAndHideCurrentTime;

    public Vector3 hidePos;
    public Vector3 showPos;

    Vector3 current;
    Vector3 diff;

    bool isPanelMoving = false;
    bool isHiding = false;




    private void Update()
    {
        if (isPanelMoving)
        {
            MovePanel();
        }
    }

    public virtual void MovePanel()
    {
        if (isHiding)
        {
            if (showAndHideCurrentTime < showAndHideAnimTime)
            {
                showAndHideCurrentTime += Time.deltaTime;
                float percent = showAndHideIconsCurve.Evaluate(showAndHideCurrentTime / showAndHideAnimTime);

                panelRect.anchoredPosition3D = new Vector3(current.x + (diff.x * percent), current.y + (diff.y * percent), panelRect.anchoredPosition3D.z);
            }
            else
            {
                isPanelMoving = false;
            }
        }
        else
        {
            if (showAndHideCurrentTime > 0)
            {
                showAndHideCurrentTime -= Time.deltaTime;
                float percent = showAndHideIconsCurve.Evaluate(showAndHideCurrentTime / showAndHideAnimTime);

                panelRect.anchoredPosition3D = new Vector3(((current.x + diff.x) - (diff.x * percent)), ((current.y + diff.y) - (diff.y * percent)), panelRect.anchoredPosition3D.z);
            }
            else
            {
                isPanelMoving = false;
            }
        }
    }

    public virtual void HidePanel()
    {
        if (isHiding)
            return;

        isPanelMoving = false;
        isHiding = true;
        showAndHideCurrentTime = 0;

        //Set up Icon Animation values
        current = panelRect.anchoredPosition3D;
        diff = new Vector3(hidePos.x - current.x, hidePos.y - current.y, 0);


        isPanelMoving = true;
    }

    public virtual void ShowPanel()
    {
        if (!isHiding)
            return;

        isPanelMoving = false;
        isHiding = false;
        showAndHideCurrentTime = showAndHideAnimTime;

        //Set up Icon Animation values
        current = panelRect.anchoredPosition3D;
        diff = new Vector3(showPos.x - current.x, showPos.y - current.y, 0);


        isPanelMoving = true;
    }
}
