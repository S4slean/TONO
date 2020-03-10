using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMarker : MonoBehaviour
{
    public Tile tile;
    public RangeType rangeType;

    public void Initialize(Tile _tile, RangeType _rangeType)
    {
        tile = _tile;
        tile.hasBarrelMarker = true;
        transform.position = tile.transform.position;
        rangeType = _rangeType;
    }
}
