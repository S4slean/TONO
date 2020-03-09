using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardmentManager : MonoBehaviour
{
    public static BombardmentManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Transform barrelParent;

    public GameObject[] barrels;
    public RangeType[] barrelRangeTypes;

    public RangeType[] barrelsToDrop;

    [Header("Pacing")]
    public float delayBeforeBarrelPlacement;
    int placementIndex;

    [Header("Stats")]
    public int barrelAmount;
    public int knownBarrelsAmount;
    public bool knowsAllBarrels;

    public void StartBombardment()
    {
        CalculateBarrelsToDrop();
    }

    IEnumerator WaitThenStartPlacingBarrels()
    {
        placementIndex = -1;
        yield return new WaitForSeconds(delayBeforeBarrelPlacement);
        StartPlacingNextBarrel();
    }


    public void StartPlacingNextBarrel()
    {
        placementIndex++;
        waitingToPlace = true;
        //PlayerManager.instance.hoverMode = HoverMode.Bombardment;
    }

    bool waitingToPlace;
    public void PlaceBarrel(Tile selectedTile)
    {
        
    }

    public void StopBombardment()
    {
        GameManager.Instance.CheckIfCompleted(true);
    }

    public void CalculateBarrelsToDrop()
    {
        barrelsToDrop = new RangeType[barrelAmount];

        for(int i = 0; i < barrelsToDrop.Length; i++)
        {
            int rand = Random.Range(0, barrelRangeTypes.Length);
            for(int j = 0; j < barrelRangeTypes.Length; j++)
            {
                if(rand == j)
                {
                    barrelsToDrop[i] = barrelRangeTypes[j];
                }
            }
        }
    }

    public GameObject InstantiateBarrel(RangeType rangeType)
    {
        for(int i = 0; i < barrelRangeTypes.Length; i++)
        {
            if(barrelRangeTypes[i] == rangeType)
            {
                return Instantiate(barrels[i], barrelParent);
            }
        }


        return Instantiate(barrels[0], barrelParent);
    }
}
