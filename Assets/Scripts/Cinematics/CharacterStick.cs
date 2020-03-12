using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterStick : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Talk()
    {
        anim.SetTrigger("talk");
    }

    public void SetFloating()
    {
        anim.SetBool("floating", true);
    }

    public void SetIdling()
    {
        anim.SetBool("floating", false);
    }

    public void Exit()
    {
        anim.SetTrigger("exit");
    }

    public void MoveLeft()
    {
        transform.DOMove(CinematicManager.Instance.leftPos, CinematicManager.Instance.movingSpeed).SetEase(CinematicManager.Instance.movingEase, CinematicManager.Instance.movingEaseStrength);
    }

    public void MoveRight()
    {
        transform.DOMove(CinematicManager.Instance.rightPos, CinematicManager.Instance.movingSpeed).SetEase(CinematicManager.Instance.movingEase, CinematicManager.Instance.movingEaseStrength);
    }

    public void MoveHalfLeft()
    {
        transform.DOMove(CinematicManager.Instance.halfLeftPos, CinematicManager.Instance.movingSpeed).SetEase(CinematicManager.Instance.movingEase, CinematicManager.Instance.movingEaseStrength);
    }

    public void MoveHalfRight()
    {
        transform.DOMove(CinematicManager.Instance.halfRightPos, CinematicManager.Instance.movingSpeed).SetEase(CinematicManager.Instance.movingEase, CinematicManager.Instance.movingEaseStrength);
    }

    public void MoveCenter()
    {
        transform.DOMove(CinematicManager.Instance.leftPos, CinematicManager.Instance.movingSpeed).SetEase(CinematicManager.Instance.movingEase, CinematicManager.Instance.movingEaseStrength);
    }
}
