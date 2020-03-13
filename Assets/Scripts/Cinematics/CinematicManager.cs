using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CinematicManager : MonoBehaviour
{
    public static CinematicManager Instance;

    public CinematicData[] datas;

    public int playingCinematic;

    public bool playsOverridingCinematic;
    public int overridingCinematic;

    [Header("Character Sticks")]
    public List<CharacterStick> characterSticks;

    public Ease movingEase;
    public float movingEaseStrength;
    public float movingSpeed;
    public float leftXPos;
    public float halfLeftXPos;
    public float halfRightXPos;
    public float rightXPos;

    public Vector3 basePos;
    [HideInInspector]
    public Vector3 leftPos;
    [HideInInspector]
    public Vector3 halfLeftPos;
    [HideInInspector]
    public Vector3 halfRightPos;
    [HideInInspector]
    public Vector3 rightPos;

    [Header("Waves")]
    public Transform waves;
    public float wavesHidingY;
    Vector3 wavesHidingPos;
    public float wavesShowingSpeed;
    public float wavesHidingSpeed;

    private void Awake()
    {
        Instance = this;

        leftPos = basePos + new Vector3(leftXPos, 0, 0);
        halfLeftPos = basePos + new Vector3(halfLeftXPos, 0, 0);
        halfRightPos = basePos + new Vector3(halfRightXPos, 0, 0);
        rightPos = basePos + new Vector3(rightXPos, 0, 0);

        wavesHidingPos = new Vector3(0, wavesHidingY, 0);
        waves.position = wavesHidingPos;
    }

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
        waves.DOMove(Vector3.zero, wavesShowingSpeed);

        if(index >= datas.Length)
        {
            Debug.LogError("ERROR : No Cinematic at index " + LevelManager.playedCinematic);
            return;
        }

        dialogueIndex = 0;
        playingCinematic = index;

        LoadingScreenManager.Instance.count += (float)datas[index].director.duration + wavesDelay;
        StartCoroutine(PlayAfterWavesEnter(index));

    }

    public float wavesDelay;
    IEnumerator PlayAfterWavesEnter(int index)
    {
        yield return new WaitForSeconds(wavesDelay);
        datas[index].director.Play();
    }

    public int dialogueIndex;
    public void NextDialogue()
    {
        string dialogue = datas[playingCinematic].dialogues[dialogueIndex];
        CinematicDialogueDisplay.Instance.DisplayDialogue(dialogue);
        dialogueIndex++;
    }

    public void AllCharacterSticksExit()
    {
        for(int i = 0; i < characterSticks.Count; i++)
        {
            characterSticks[i].Exit();
        }
    }

    public void EndCinematic()
    {
        AllCharacterSticksExit();
        waves.DOMove(wavesHidingPos, wavesHidingSpeed);
    }
}
