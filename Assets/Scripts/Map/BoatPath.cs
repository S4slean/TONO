using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPath : MonoBehaviour
{
    public int pathSubdivisions;
    public Vector3[] basePath;
    public Vector3[] startingTangeants;
    public Vector3[] endingTangeants;
    public Vector3[] path;
    public int[] anchorPointsIndexes;
    public float height;
}
