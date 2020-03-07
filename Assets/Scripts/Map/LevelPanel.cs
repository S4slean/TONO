using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class LevelPanel : MonoBehaviour
{
    public static LevelPanel Instance;

    

    public float showingSpeed;
    public Ease showingEase;
    public float hidingSpeed;
    public Ease hidingEase;
    public int hiddenX;
    Vector2 shownPos;
    Vector2 hiddenPos;
    RectTransform rt;
    public TextMeshProUGUI levelNameText;

    private void Awake()
    {
        Instance = this;

        rt = GetComponent<RectTransform>();

        shownPos = rt.anchoredPosition;
        hiddenPos = shownPos + new Vector2(hiddenX, 0);
    }

    public void Display()
    {
        SetNameText();
        Show();
    }

    public void Show()
    {
        rt.DOAnchorPos(shownPos, showingSpeed).SetEase(showingEase);
    }

    public void Hide()
    {
        rt.DOAnchorPos(hiddenPos, hidingSpeed).SetEase(hidingEase);
    }

    public void HideImmediately()
    {
        rt.anchoredPosition = hiddenPos;
    }

    public void SetNameText()
    {
        levelNameText.text = "LEVEL " + LevelManager.currentLevel.ToString();
    }

    public void PlayButtonClicked()
    {

    }
}
