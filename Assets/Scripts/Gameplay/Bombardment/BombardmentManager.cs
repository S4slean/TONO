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

    public void Initialize()
    {
        barrelMarkersPool = new Queue<BarrelMarker>();
        FillBarrelMarkerPool();
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

    [Header("Stats")]
    public int barrelAmount;
    public int knownBarrelsAmount;
    public bool knowsAllBarrels;

    Queue<BarrelMarker> barrelMarkersPool;

    void FillBarrelMarkerPool()
    {
        for(int i = 0; i < 10; i++)
        {
            BarrelMarker newMarker = Instantiate(barrelMarker, transform).GetComponent<BarrelMarker>();
            newMarker.gameObject.SetActive(false);
            barrelMarkersPool.Enqueue(newMarker);
        }
    }

    public void StartBombardment()
    {
        CalculateBarrelsToDrop();
        StartCoroutine(WaitThenStartPlacingBarrels());
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
        PlayerManager.instance.hoverMode = HoverMode.Bombardment;
    }

    bool waitingToPlace;
    public void PlaceBarrelMarker(Tile selectedTile)
    {
        waitingToPlace = false;
        if(barrelMarkersPool.Count < 1)
        {
            FillBarrelMarkerPool();
        }
        BarrelMarker toPlace = barrelMarkersPool.Dequeue();
        toPlace.Initialize(selectedTile, barrelsToDrop[placementIndex]);
        placementIndex++;

        if(placementIndex >= barrelAmount)
        {
            StopBombardment();
        }
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

    public void DropBarrels()
    {
        for(int i = 0; i < activeMarkers.Count; i++)
        {
            GameObject toDrop = BarrelManager.Instance.GetBarrel(activeMarkers[0].rangeType);
            toDrop.transform.position = activeMarkers[0].transform.position;
            toDrop.gameObject.SetActive(true);
            activeMarkers.RemoveAt(0);
        }
    }
}
