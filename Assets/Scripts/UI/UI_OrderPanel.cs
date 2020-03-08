using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OrderPanel : MonoBehaviour
{
    [Header("Icons")]
    public GameObject characterIconPrefab;
    public RectTransform panelRect;
    [HideInInspector] public List<UI_Portrait> charactersIcons = new List<UI_Portrait>();
    List<float> iconPositions = new List<float>();
    public float maxIconsSpacing;


    [Header("Animations parameters")]
    public AnimationCurve showAndHideIconsCurve;
    public float showAndHideAnimTime;
    float showAndHideCurrentTime;

    public float hidePos;
    public float showPos;


    public AnimationCurve replaceIconsCurve;
    public float replaceMaxTime;
    float replaceCurrentTime;

    float current = 0;
    float diff = 0;


    List<float> currents = new List<float>();
    List<float> diffs = new List<float>();

    bool isMoving = false;
    bool isShown = false;

    bool isRearranging = false;
    bool isEveryone = false;

    int selectedIcon = -1;
    int numberOfIcons = 0;

    List<UI_Portrait> portaitToRemove = new List<UI_Portrait>();




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetUpIcons();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            NextIconTurn();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ShowIconsOrder();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            HideIconsOrder();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DebugRemove(2);
        }

        if (isMoving)
        {
            MoveIcons();
        }
    }

    private void MoveIcons()
    {
        if (isEveryone)
        {
            if (isShown)
            {
                if (showAndHideCurrentTime < showAndHideAnimTime)
                {
                    showAndHideCurrentTime += Time.deltaTime;
                    float percent = showAndHideIconsCurve.Evaluate(showAndHideCurrentTime / showAndHideAnimTime);

                    panelRect.anchoredPosition3D = new Vector3(panelRect.anchoredPosition3D.x, current + (diff * percent), panelRect.anchoredPosition3D.z);
                }
                else
                {
                    isEveryone = false;
                    isMoving = false;
                }
            }
            else
            {
                if (showAndHideCurrentTime > 0)
                {
                    showAndHideCurrentTime -= Time.deltaTime;
                    float percent = showAndHideIconsCurve.Evaluate(showAndHideCurrentTime / showAndHideAnimTime);

                    panelRect.anchoredPosition3D = new Vector3(panelRect.anchoredPosition3D.x, ((current + diff) - (diff * percent)), panelRect.anchoredPosition3D.z);
                }
                else
                {
                    isEveryone = false;
                    isMoving = false;
                }
            }

        }

        if (isRearranging)
        {
            if (replaceCurrentTime < replaceMaxTime)
            {
                replaceCurrentTime += Time.deltaTime;
                float percent = replaceIconsCurve.Evaluate(replaceCurrentTime / replaceMaxTime);

                //Move icons to their correct position
                for (int i = 1; i < charactersIcons.Count; i++)
                {
                    charactersIcons[i].portraitRect.anchoredPosition3D = new Vector3(currents[i] + (diffs[i] * percent), charactersIcons[i].portraitRect.anchoredPosition3D.y, charactersIcons[i].portraitRect.anchoredPosition3D.z);
                }

                //panelRect.anchoredPosition3D = new Vector3(current - (recenterValue * percent), panelRect.anchoredPosition3D.y, panelRect.anchoredPosition3D.z);

            }
            else
            {
                isRearranging = false;
                isMoving = false;
                replaceCurrentTime = 0;
            }
        }
    }



    public void SetUpIcons()
    {
        charactersIcons = new List<UI_Portrait>();
        iconPositions = new List<float>();
        numberOfIcons = 2 + EnemyManager.instance.enemyList.Count;

        ////Get number of characters
        for (int i = 0; i < numberOfIcons; i++)
        {
            GameObject obj = Instantiate(characterIconPrefab, Vector3.zero, Quaternion.identity, this.transform);
            UI_Portrait values = obj.GetComponent<UI_Portrait>();
            values.panelRef = this;
            values.indexOrder = i;
            //Set Icon sprite
            if (i == (numberOfIcons - 1))
            {
                values.backgroundImage.sprite = UI_Manager.instance.uiPreset.playerPortait;
                values.portraitImage.sprite = UI_Manager.instance.uiPreset.playerPortait;
            }
            else if (i == 0)
            {
                values.backgroundImage.sprite = UI_Manager.instance.uiPreset.boatPortait;
                values.portraitImage.sprite = UI_Manager.instance.uiPreset.boatPortait;
            }
            else
            {
               

                switch (EnemyManager.instance.enemyList[i].enemyStats.enemyType)
                {
                    case EnemyData.EnemyType.Moussaillon:
                        values.backgroundImage.sprite = UI_Manager.instance.uiPreset.moussaillonImage;
                        values.portraitImage.sprite = UI_Manager.instance.uiPreset.moussaillonImage;
                        break;

                    case EnemyData.EnemyType.Captain:
                        values.backgroundImage.sprite = UI_Manager.instance.uiPreset.captainImage;
                        values.portraitImage.sprite = UI_Manager.instance.uiPreset.captainImage;
                        break;

                    case EnemyData.EnemyType.Kamikaze:
                        values.backgroundImage.sprite = UI_Manager.instance.uiPreset.kamikazeImage;
                        values.portraitImage.sprite = UI_Manager.instance.uiPreset.kamikazeImage;
                        break;

                    case EnemyData.EnemyType.Hooker:
                        values.backgroundImage.sprite = UI_Manager.instance.uiPreset.hookerImage;
                        values.portraitImage.sprite = UI_Manager.instance.uiPreset.hookerImage;
                        break;
                }
            }

            values.stickImage.sprite = UI_Manager.instance.uiPreset.stick;
            //Change stick color according to character type (Enemy, Player or Boat) ??????
            //values.stickImage.color = new Color32((byte)255, (byte)0, (byte)0, (byte)255);
            
            //Set Icon Position
            Vector3 newPos;
            newPos = new Vector3(((values.portraitRect.sizeDelta.x * i) + (maxIconsSpacing * i)), 0, 0);
            values.portraitRect.anchoredPosition3D = newPos;


            iconPositions.Add(newPos.x);
            charactersIcons.Add(values);
        }
    }

    private void DebugRemove(int index)
    {
        charactersIcons[index].RemoveIcon();
    }

    /// <summary>
    /// If the current character is removed during its turn or a previous one PLEASE update the selectedIcon
    /// </summary>
    public void RefreshSelectedIcon(int indexToRemove)
    {
        Debug.Log("Refresh Selected");

        isRearranging = false;
        isMoving = false;
        replaceCurrentTime = 0;

        if(selectedIcon < 0)
        {
            charactersIcons.RemoveAt(charactersIcons[indexToRemove].indexOrder);

            RefreshIconsOrder();

            return;
        }

        if (indexToRemove == selectedIcon)
        {
            selectedIcon--;
        }

        charactersIcons.RemoveAt(charactersIcons[indexToRemove].indexOrder);

        RefreshIconsOrder();
    }

    private void RefreshIconsOrder()
    {
        Debug.Log("Refresh ORDER");

        for (int i = 1; i < charactersIcons.Count; i++)
        {
            charactersIcons[i].indexOrder = i;
            if (charactersIcons[i].isSelected)
                selectedIcon = charactersIcons[i].indexOrder;
        }

        RearrangeIcons();
    }

    private void RearrangeIcons()
    {
        Debug.Log("Prepare to REARRANGE");


        currents = new List<float>();
        diffs = new List<float>();

        for (int i = 0; i < charactersIcons.Count; i++)
        {
            current = charactersIcons[i].portraitRect.anchoredPosition3D.x;
            diff = iconPositions[i] - current;

            currents.Add(current);
            diffs.Add(diff);
        }

        isRearranging = true;
        isMoving = true;
    }


    /// <summary>
    /// Next Turn set up (incrementing for the next icon to move)
    /// </summary>
    public void NextIconTurn()
    {
        int newIndex = 0;

        newIndex = selectedIcon + 1;

        if (newIndex > charactersIcons.Count - 1)
            newIndex = 0;

        if (selectedIcon > -1)
            charactersIcons[selectedIcon].MoveBackIcon();

        charactersIcons[newIndex].MoveSelectedIcon();

        selectedIcon = newIndex;
    }


    public void ShowIconsOrder()
    {
        if (!isShown)
            return;

        isEveryone = false;
        isMoving = false;
        isShown = false;
        showAndHideCurrentTime = showAndHideAnimTime;

        //Set up Icon Animation values
        current = panelRect.anchoredPosition3D.y;
        diff = showPos - current;


        isEveryone = true;
        isMoving = true;
    }


    public void HideIconsOrder()
    {
        if (isShown)
            return;

        isEveryone = false;
        isMoving = false;
        isShown = true;
        showAndHideCurrentTime = 0;

        //Set up Icon Animation values
        current = panelRect.anchoredPosition3D.y;
        diff = hidePos + current;


        isEveryone = true;
        isMoving = true;
    }
}
