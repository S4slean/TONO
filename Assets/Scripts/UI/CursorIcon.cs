using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SC_CursorIconPreset", menuName = "UI/CursorIconPreset", order = 100)]
public class CursorIcon : ScriptableObject
{
    public Texture2D[] cursorIcons;
}
