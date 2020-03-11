using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public static CinematicManager Instance;

    public CinematicData[] datas;

    public int playingCinematic;

    public bool playsOverridingCinematic;
    public int overridingCinematic;

    public void CheckCinematic()
    {
        if(playsOverridingCinematic)
        {
            PlayCinematic(overridingCinematic);
            return;
        }

        if(LevelManager.playedCinematic == 0)
        {
            return;
        }
        else
        {
            PlayCinematic(LevelManager.playedCinematic);
        }
    }

    void PlayCinematic(int index)
    {

        if(index >= datas.Length)
        {
            Debug.LogError("ERROR : No Cinematic at index " + LevelManager.playedCinematic);
            return;
        }

        dialogueIndex = 0;
        playingCinematic = index;

        LoadingScreenManager.Instance.count += (float)datas[index].director.duration;
        datas[index].director.Play();
    }

    public int dialogueIndex;
    public void NextDialogue()
    {
        string dialogue = datas[playingCinematic].dialogues[dialogueIndex];
        CinematicDialogueDisplay.Instance.DisplayDialogue(dialogue);
        dialogueIndex++;
    }
}
