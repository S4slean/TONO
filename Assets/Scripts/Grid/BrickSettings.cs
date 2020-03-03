using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Brick Settings", menuName = "Brick", order = 0)]
public class BrickSettings : ScriptableObject {

    public Color color;
    public BrickType type;
}

public enum BrickType
{
    ground,
    water
}
