using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bricks Settings", menuName = "Bricks Settings", order = 1)]
public class BricksSettings : ScriptableObject
{
    public List<BrickSettings> bricks;
}
