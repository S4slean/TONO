using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class SavingIcon : MonoBehaviour
{
    public static SavingIcon Instance;
    public Animator anim;
    public float duration;

    private void Awake()
    {
        Instance = this;
    }


    public void PlaySaveAnimation()
    {
        anim.SetTrigger("play");
    }

}
