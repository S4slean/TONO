using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BoatInfo : Panel_Behaviour
{
    [Header("Boat & Barrels")]
    public Image boatImage;
    public Transform barrelsParent;
    public GameObject barrelPrefab;
    public float barrelSpacing;
    List<RectTransform> barrels;
    RectTransform selectedBarrel;

    [Header("Remove Animation")]
    public AnimationCurve removeCurve;
    public float removeAnimTime;
    private float removeCurrentTime;
    public float removeDisplacement;

    bool isRemoving = false;

    [Header("Debug")]
    public int numberOfDisplayedBarrels;
    public int currentBarrel;



    void Update()
    {
        MovePanel();

        RemoveAnimation();
    }


    private void RemoveAnimation()
    {
        if (isRemoving)
        {
            if (removeCurrentTime < removeAnimTime)
            {
                removeCurrentTime += Time.deltaTime;

                float percent = removeCurve.Evaluate(removeCurrentTime / removeAnimTime);

                selectedBarrel.anchoredPosition3D = new Vector3(selectedBarrel.anchoredPosition3D.x, 0 - (removeDisplacement * percent), 0);
            }
            else
            {
                isRemoving = false;
                removeCurrentTime = 0;
                barrels.Remove(selectedBarrel);
            }
        }
    }

    /// <summary>
    /// Set up boat values (boat's and barrels' images) according to the player character abilities/upgrades
    /// </summary>
    public void SetUpBoatUI()
    {
        boatImage.sprite = UI_Manager.instance.uiPreset.boatPortait;
        barrels = new List<RectTransform>();

        //////////////////////////////////////         BombardmentManager.Instance.knownBarrelsAmount
        for (int i = 0; i < BombardmentManager.Instance.barrelAmount; i++)
        {
            GameObject barrel = Instantiate(barrelPrefab, Vector3.zero, Quaternion.identity, barrelsParent);
            RectTransform barrelRect = barrel.GetComponent<RectTransform>();
            Image barrelImage = barrel.GetComponent<Image>();

            if(BombardmentManager.Instance.knowsAllBarrels)
            {
                switch(BombardmentManager.Instance.barrelsToDrop[i])
                {
                    case RangeType.Default:
                        barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Default;
                        break;

                    case RangeType.Cross:
                        barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Cross;
                        break;

                    case RangeType.Plus:
                        barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Plus;
                        break;

                    case RangeType.Round:
                        barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Round;
                        break;
                }
            }
            else
            {
                barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Mystery;
            }

            barrelRect.anchoredPosition3D = new Vector3(barrelRect.sizeDelta.x * i + barrelSpacing * i,0,0);

            barrels.Add(barrelRect);
        }

        //////////////////////////////////////         BombardmentManager.Instance.knownBarrelsAmount
        currentBarrel = BombardmentManager.Instance.barrelAmount - 1;
    }

    /// <summary>
    /// Set up values to remove a barrel with animation
    /// </summary>
    public void RemoveBarrelUI()
    {
        if (currentBarrel < 0)
        {
            Debug.LogError("NO MORE BARRELS");
            return;
        }

        selectedBarrel = barrels[currentBarrel];
        currentBarrel--;

        isRemoving = true;
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
