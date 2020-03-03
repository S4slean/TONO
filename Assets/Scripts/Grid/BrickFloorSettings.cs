using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Brick Floor Settings", menuName = "Brick Floor Settings", order = 3)]
public class BrickFloorSettings : ScriptableObject
{
    public Vector3 centerPos;
    public float xDistance;
    public float yDistance;
    public int lineAmount;
    public int rowAmount;
}
