using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Brick Wall Data", menuName = "Brick Wall Data", order = 2)]
public class BrickFloorData : ScriptableObject {

    public BrickFloorSettings brickWallSettings;
    public BricksSettings bricksSettings;
    public int[] brickIndexes;
    public int selectedSetting;
	
}
