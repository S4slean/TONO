using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : MonoBehaviour
{
    public static TransitionScreen Instance;
    public Animator animator;
    public float transitionDuration;
    float transitionCount;

    public GameObject graphics;

    public void PlayTransition()
    {
        graphics.SetActive(true);
        animator.SetTrigger("transition");
        transitionCount = transitionDuration;
    }

    public void EndTransition()
    {
        graphics.SetActive(false);
    }

    private void Update()
    {
        if(transitionCount > 0f)
        {
            transitionCount -= Time.deltaTime;
            if(transitionCount <= 0f)
            {
                EndTransition();
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
