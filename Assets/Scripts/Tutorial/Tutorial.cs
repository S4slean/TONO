using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorials/New Tutorial", order = 0)]
public class TutorialData : ScriptableObject
{
    public string tutorialText;
    public Vector2 tutorialTextPosition;

    public Vector2 tutorialArrowPosition;
    [Range(0, 360)]
    public float arrowRotation;
}
