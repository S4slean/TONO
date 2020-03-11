using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public class CinematicData
{
    public PlayableDirector director;

    [TextArea()]
    public string[] dialogues;
}
