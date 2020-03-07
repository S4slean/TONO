using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OrderPanel : MonoBehaviour
{
    [Header("Icons")]
    public GameObject characterIconPrefab;
    public RectTransform panelRect;
    List<RectTransform> charactersIcons = new List<RectTransform>();
    //List<RectTransform> characterToRemove = new List<RectTransform>();

    public float currentY = 0;
    public float newY = 0;
    public float diffY = 0;
    public float spaceBetweenPlayerAndEnnemies = 0;

    [Header("Animations parameters")]
    public AnimationCurve selectedIconCurve;
    public float selectedDisplacement = 20;
    public float selectedMaxTime;
    float selectedCurrentTime;

    public AnimationCurve showAndHideIconsCurve;
    public float showAndHideDisplacement = 50;
    public float showAndHideMaxTime;
    float showAndHideCurrentTime;

    public AnimationCurve removeIconsCurve;
    public float removeMaxTime;
    float removeCurrentTime;

    public AnimationCurve replaceIconsCurve;
    public float replaceMaxTime;
    float replaceCurrentTime;

    List<RectTransform> charactersToReplace = new List<RectTransform>();
    List<float> currentsX = new List<float>();

    float prefabSize = 0;

    bool isMoving = false;
    public bool isHidden = false;

    bool isRemoving = false;
    bool isReplacing = false;
    bool isEveryone = false;
    bool isSelected = false;
    bool isFirst = true;

    private int selectedIcon = 0;
    private int lastRemovedIndex = 0;

    [Header("Debug")]
    public int numberOfIcons = 5;
    public int removeIconAtIndex = 0;



    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetUpIcons();
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    NextIconTurn();
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    ShowOrHideIcons();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    RemoveIcon(removeIconAtIndex);
        //}

        if (isMoving)
        {
            MoveIcons();
        }
    }

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
                        //Debug.Log("selectedIcon : " + selectedIcon);
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

        if (isRemoving)
        {
            if (removeCurrentTime < removeMaxTime)
            {
                removeCurrentTime += Time.deltaTime;
                float percent = removeIconsCurve.Evaluate(removeCurrentTime / removeMaxTime);

                //Remove ICON
                charactersIcons[lastRemovedIndex].anchoredPosition3D = new Vector3(charactersIcons[lastRemovedIndex].anchoredPosition3D.x, currentY + (diffY * percent), charactersIcons[lastRemovedIndex].anchoredPosition3D.z);
            }
            else
            {
                isRemoving = false;
                isMoving = false;
                removeCurrentTime = 0;

                ReplaceIconsFrom(lastRemovedIndex);
            }
        }

        if (isReplacing)
        {
            if (replaceCurrentTime < replaceMaxTime)
            {
                replaceCurrentTime += Time.deltaTime;
                float percent = replaceIconsCurve.Evaluate(replaceCurrentTime / replaceMaxTime);

                //Move from left to right
                for (int i = 0; i < lastRemovedIndex; i++)
                {
                    charactersToReplace[i].anchoredPosition3D = new Vector3(currentsX[i] + (prefabSize * percent), charactersToReplace[i].anchoredPosition3D.y, charactersToReplace[i].anchoredPosition3D.z);
                }

                panelRect.anchoredPosition3D = new Vector3(currentY - (prefabSize * percent), panelRect.anchoredPosition3D.y, panelRect.anchoredPosition3D.z);

            }
            else
            {
                isReplacing = false;
                isMoving = false;
                replaceCurrentTime = 0;
            }
        }
    }

    public void CheckRemoveList()
    {

    }

    public void SetUpIcons()
    {
        charactersIcons = new List<RectTransform>();
        prefabSize = characterIconPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float globalSpace = ((numberOfIcons - 1) * prefabSize) + spaceBetweenPlayerAndEnnemies;

        ////Get number of characters
        for (int i = 0; i < numberOfIcons; i++)
        {
            GameObject obj = Instantiate(characterIconPrefab, Vector3.zero, Quaternion.identity, this.transform);
            RectTransform rect = obj.GetComponent<RectTransform>();

            UI_Portrait values = obj.GetComponent<UI_Portrait>();

            //Set Icon Position
            Vector3 newPos;
            if (i == (numberOfIcons - 1))
            {
                newPos = new Vector3(0, 0, 0);
            }
            else
                newPos = new Vector3(-globalSpace + ((rect.sizeDelta.x * i)), 0, 0);

            rect.anchoredPosition3D = newPos;


            ////Get character TYPE (Ennemy, Player) surement un SWITCH
            values.backgroundImage.sprite = UI_Manager.instance.uiPreset.playerPortait;
            values.portraitImage.sprite = UI_Manager.instance.uiPreset.playerPortait;

            values.stickImage.sprite = UI_Manager.instance.uiPreset.stick;
            values.stickImage.color = new Color32((byte)255, (byte)0, (byte)0, (byte)255);

            charactersIcons.Add(rect);
        }

        isFirst = true;
    }

    public void RemoveIcon(int index)
    {
        if (isMoving)
            return;

        currentY = charactersIcons[index].anchoredPosition3D.y;
        newY = currentY + showAndHideDisplacement;
        diffY = newY - currentY;

        lastRemovedIndex = index;

        isRemoving = true;
        isMoving = true;
    }

    private void ReplaceIconsFrom(int replaceFromIndex)
    {
        if(lastRemovedIndex != 0)
        {
            charactersToReplace = new List<RectTransform>();
            currentsX = new List<float>();

            charactersIcons[replaceFromIndex].gameObject.SetActive(false);
            charactersIcons.RemoveAt(replaceFromIndex);

            for (int i = 0; i < replaceFromIndex; i++)
            {
                currentY = charactersIcons[i].anchoredPosition3D.x;
                newY = currentY + prefabSize;
                diffY = newY - currentY;

                currentsX.Add(currentY);
                charactersToReplace.Add(charactersIcons[i]);
            }

            currentY = panelRect.anchoredPosition3D.x;

            isReplacing = true;
            isMoving = true;
        }
    }

    public void UpdateCharactersIndex()
    {
        //Set Character Index in orderPanel
        for (int i = 0; i < charactersIcons.Count; i++)
        {
            //GetComponent character
            //character.orderIndex = i;
        }
    }

    /// <summary>
    /// Next Turn set up (incrementing for the next icon to move)
    /// </summary>
    public void NextIconTurn()
    {
        int newIndex = 0;

        if (isFirst)
            newIndex = selectedIcon;
        else
            newIndex = selectedIcon + 1;

        isFirst = false;

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
        if (isMoving)
            return;

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
