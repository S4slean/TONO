using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadingScreen : MonoBehaviour
{
    public Image blackImage;

    public float fadingSpeed;

    public static FadingScreen Instance;

    public bool startsVisible;
    private void Start()
    {
        if(startsVisible)
        {
            blackImage.color = new Color(0, 0, 0, 1);
        }
        else
        {
            blackImage.color = new Color(0, 0, 0, 0);
        }

        FadeOut();
    }


    private void Awake()
    {
        Instance = this;
    }

    public void FadeIn()
    {
        blackImage.DOFade(1f, fadingSpeed);
    }

    public void FadeOut()
    {
        blackImage.DOFade(0f, fadingSpeed);
    }

    public void FadeInThenLoad(string toLoad)
    {
        FadeIn();
        StartCoroutine(WaitThenLoad(toLoad));
    }

    IEnumerator WaitThenLoad(string toLoad)
    {
        yield return new WaitForSeconds(fadingSpeed);
        LevelManager.GoToSceneDirectly(toLoad);

    }
}
