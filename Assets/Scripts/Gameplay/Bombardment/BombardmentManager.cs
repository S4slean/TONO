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

    [Header("Placement")]
    public GameObject barrelMarker;
    List<BarrelMarker> activeMarkers = new List<BarrelMarker>();
    public Color roundColor;
    public Color plusColor;
    public Color crossColor;

    [Header("Spawning")]
    public float barrelSpawningHeight;

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
        StartPlacingNextBarrelMarker();
    }


    public void StartPlacingNextBarrelMarker()
    {
        placementIndex++;
        waitingToPlace = true;
        //PlayerManager.instance.hoverMode = HoverMode.Bombardment;
    }

    bool waitingToPlace;
    public void PlaceBarrelMarker(Tile selectedTile)
    {
        waitingToPlace = false;
        BarrelMarker toPlace = Instantiate(barrelMarker, transform).GetComponent<BarrelMarker>();
        toPlace.Initialize(selectedTile, barrelsToDrop[placementIndex]);
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
