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
    List<UI_Barrel> barrels;
    UI_Barrel selectedBarrel;

    public int currentBarrel;



    void Update()
    {
        MovePanel();
    }


    /// <summary>
    /// Set up boat values (boat's and barrels' images) according to the player character abilities/upgrades
    /// </summary>
    public void SetUpBoatUI()
    {
        boatImage.sprite = UI_Manager.instance.uiPreset.boatPortait;
        barrels = new List<UI_Barrel>();

        for (int i = 0; i < BombardmentManager.Instance.barrelAmount; i++)
        {
            GameObject barrelObj = Instantiate(barrelPrefab, Vector3.zero, Quaternion.identity, barrelsParent);
            UI_Barrel barrel = barrelObj.GetComponent<UI_Barrel>();

            if(BombardmentManager.Instance.knowsAllBarrels)
            {
                switch(BombardmentManager.Instance.barrelsToDrop[i])
                {
                    case RangeType.Default:
                        barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Default;
                        break;

                    case RangeType.Cross:
                        barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Cross;
                        break;

                    case RangeType.Plus:
                        barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Plus;
                        break;

                    case RangeType.Round:
                        barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Round;
                        break;
                }
            }
            else
            {
                for (int y = 0; y < BombardmentManager.Instance.knownBarrelsAmount; y++)
                {
                    switch (BombardmentManager.Instance.barrelsToDrop[i])
                    {
                        case RangeType.Default:
                            barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Default;
                            break;

                        case RangeType.Cross:
                            barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Cross;
                            break;

                        case RangeType.Plus:
                            barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Plus;
                            break;

                        case RangeType.Round:
                            barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Round;
                            break;
                    }
                }

                for (int y = BombardmentManager.Instance.knownBarrelsAmount; y < BombardmentManager.Instance.barrelAmount; y++)
                {
                    barrel.barrelImage.sprite = UI_Manager.instance.uiPreset.barrel_Mystery;
                }
            }

            barrel.barrelRect.anchoredPosition3D = new Vector3(barrel.barrelRect.sizeDelta.x * i + barrelSpacing * i,0,0);

            barrel.selectedPlacement = new Vector3(barrel.barrelRect.sizeDelta.x * i + barrelSpacing * i, barrel.selectedPlacement.y, 0);
            barrel.removedPlacement = new Vector3(barrel.barrelRect.sizeDelta.x * i + barrelSpacing * i, barrel.removedPlacement.y, 0);

            barrels.Add(barrel);
        }

        currentBarrel = BombardmentManager.Instance.barrelAmount - 1;

        selectedBarrel = barrels[currentBarrel];
        selectedBarrel.MoveSelectedBarrel();
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

        selectedBarrel.RemoveBarrel();
        barrels.Remove(selectedBarrel);

        currentBarrel--;
        if (currentBarrel >= 0)
        {
            selectedBarrel = barrels[currentBarrel];
            selectedBarrel.MoveSelectedBarrel();
        }
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
