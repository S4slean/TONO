using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OrderPanel : MonoBehaviour
{
    [Header("Icons")]
    public GameObject characterIconPrefab;
    List<RectTransform> charactersIcons;
    public float currentY;
    public float newY;
    public float diffY;

    [Header("Animations parameters")]
    public AnimationCurve selectedIconCurve;
    public float selectedDisplacement = 20;
    public float selectedMaxTime;
    float selectedCurrentTime;

    public AnimationCurve showAndHideIconsCurve;
    public float showAndHideDisplacement = 50;
    public float showAndHideMaxTime;
    float showAndHideCurrentTime;

    bool isMoving = false;
    public bool isHidden = false;

    bool isEveryone = false;
    bool isSelected = false;

    [Header("Debug")]
    private int selectedIcon = -1;
    private int numberOfIcons = 5;

    public Sprite portrait;
    public Sprite stickSprite;



    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SetUpIcons();
    //    }

    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        NextIconTurn();
    //    }

    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        ShowOrHideIcons();
    //    }

    //    if (isMoving)
    //    {
    //        MoveIcons();
    //    }
    //}

    private void MoveIcons()
    {
        if (isSelected)
        {
            if (selectedCurrentTime < selectedMaxTime)
            {
                selectedCurrentTime += Time.deltaTime;
                float percent = selectedIconCurve.Evaluate(selectedCurrentTime / selectedMaxTime);

                if (selectedIcon != charactersIcons.Count)
                    charactersIcons[selectedIcon].anchoredPosition3D = new Vector3(charactersIcons[selectedIcon].anchoredPosition3D.x, currentY - (diffY * percent), charactersIcons[selectedIcon].anchoredPosition3D.z);

                if (selectedIcon != 0)
                    charactersIcons[selectedIcon - 1].anchoredPosition3D = new Vector3(charactersIcons[selectedIcon - 1].anchoredPosition3D.x, (currentY - diffY) + (diffY * percent), charactersIcons[selectedIcon - 1].anchoredPosition3D.z);
            }
            else
            {
                isSelected = false;
                isMoving = false;
                selectedCurrentTime = 0;
            }
        }


        if (isEveryone)
        {
            if (isHidden)
            {
                if (showAndHideCurrentTime < showAndHideMaxTime)
                {
                    showAndHideCurrentTime += Time.deltaTime;
                    float percent = showAndHideIconsCurve.Evaluate(showAndHideCurrentTime / showAndHideMaxTime);

                    for (int i = 0; i < charactersIcons.Count; i++)
                    {
                        if (i != selectedIcon)
                            charactersIcons[i].anchoredPosition3D = new Vector3(charactersIcons[i].anchoredPosition3D.x, currentY - (diffY * percent), charactersIcons[i].anchoredPosition3D.z);
                    }

                    if (selectedIcon != charactersIcons.Count)
                        charactersIcons[selectedIcon].anchoredPosition3D = new Vector3(charactersIcons[selectedIcon].anchoredPosition3D.x, currentY - ((diffY + selectedDisplacement) * percent), charactersIcons[selectedIcon].anchoredPosition3D.z);
                }
                else
                {
                    isEveryone = false;
                    isMoving = false;
                    isHidden = false;
                    showAndHideCurrentTime = 0;
                }
            }
            else
            {
                if (showAndHideCurrentTime < showAndHideMaxTime)
                {
                    showAndHideCurrentTime += Time.deltaTime;
                    float percent = showAndHideIconsCurve.Evaluate(showAndHideCurrentTime / showAndHideMaxTime);

                    for (int i = 0; i < charactersIcons.Count; i++)
                    {
                        if (i != selectedIcon)
                            charactersIcons[i].anchoredPosition3D = new Vector3(charactersIcons[i].anchoredPosition3D.x, currentY + (diffY * percent), charactersIcons[i].anchoredPosition3D.z);
                    }

                    if (selectedIcon != charactersIcons.Count)
                    {
                        Debug.Log("selectedIcon : " + selectedIcon);
                        charactersIcons[selectedIcon].anchoredPosition3D = new Vector3(charactersIcons[selectedIcon].anchoredPosition3D.x, currentY + ((diffY + selectedDisplacement) * percent), charactersIcons[selectedIcon].anchoredPosition3D.z);
                    }
                }
                else
                {
                    isEveryone = false;
                    isHidden = true;
                    isMoving = false;
                    showAndHideCurrentTime = 0;
                }
            }

        }
    }


    public void SetUpIcons()
    {
        charactersIcons = new List<RectTransform>();

        ////Get number of characters
        for (int i = 0; i < numberOfIcons; i++)
        {
            GameObject obj = Instantiate(characterIconPrefab, Vector3.zero, Quaternion.identity, this.transform);
            RectTransform rect = obj.GetComponent<RectTransform>();

            UI_Portrait values = obj.GetComponent<UI_Portrait>();

            rect.anchoredPosition3D = new Vector3((rect.sizeDelta.x * i), 0, 0);

            ////Get character TYPE (Ennemy, Boat, Player) surement un SWITCH
            values.backgroundImage.sprite = portrait;
            values.portraitImage.sprite = portrait;

            values.stickImage.sprite = stickSprite;
            values.stickImage.color = new Color32((byte)255, (byte)0, (byte)0, (byte)255);

            charactersIcons.Add(rect);
        }
    }

    /// <summary>
    /// Next Turn set up (incrementing for the next icon to move)
    /// </summary>
    public void NextIconTurn()
    {
        int newIndex = selectedIcon + 1;

        if (newIndex > charactersIcons.Count)
            newIndex = 0;

        SelectIcon(newIndex);
    }

    /// <summary>
    /// Selection SetUp
    /// </summary>
    /// <param name="selectedIndex"></param>
    public void SelectIcon(int selectedIndex)
    {
        selectedIcon = selectedIndex;

        //Set up Icon Animation values
        if (selectedIcon != charactersIcons.Count)
            currentY = charactersIcons[selectedIcon].anchoredPosition3D.y;
        else
            currentY = charactersIcons[0].anchoredPosition3D.y;

        newY = currentY + selectedDisplacement;
        diffY = newY - currentY;

        isSelected = true;
        isMoving = true;
    }

    /// <summary>
    /// Show/Hide set up
    /// </summary>
    public void ShowOrHideIcons()
    {
        if (isMoving)
            return;

        //isHidden = !isHidden;

        //Set up Icon Animation values
        if (!isHidden)
            currentY = 0;
        else
            currentY = showAndHideDisplacement;

        newY = currentY + showAndHideDisplacement;
        diffY = newY - currentY;


        isEveryone = true;
        isMoving = true;
    }
}
