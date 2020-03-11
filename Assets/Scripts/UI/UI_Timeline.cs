using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timeline : Panel_Behaviour
{
    [Header("Icons")]
    public GameObject characterIconPrefab;
    [HideInInspector] public List<UI_Portrait> charactersIcons = new List<UI_Portrait>();
    List<Vector3> iconPositions = new List<Vector3>();
    public RectTransform charactersPortraitParent;
    public float maxIconsSpacing;
    float iconSizeref;

    [Header("Icon Animations")]
    public AnimationCurve replaceIconsCurve;
    public float replaceMaxTime;
    float replaceCurrentTime;

    Vector3 currentIconPos;
    Vector3 diffIconPos;

    Vector3 currentPanelPos;
    Vector3 nextPanelPos;
    Vector3 diffPanelPos;

    List<Vector3> currents;
    List<Vector3> diffs;

    bool isMoving = false;
    bool isRearranging = false;

    int selectedIcon = -1;
    int numberOfIcons = 0;



    private void Awake()
    {
        iconSizeref = characterIconPrefab.GetComponent<RectTransform>().sizeDelta.x * 0.5f;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveIcons();
        }

        MovePanel();
    }


    /// <summary>
    /// Replace ALL icons to their corresponding IndexPostion
    /// </summary>
    private void MoveIcons()
    {
        if (isRearranging)
        {
            if (replaceCurrentTime < replaceMaxTime)
            {
                replaceCurrentTime += Time.deltaTime;
                float percent = replaceIconsCurve.Evaluate(replaceCurrentTime / replaceMaxTime);

                //Move icons to their correct position
                for (int i = 1; i < charactersIcons.Count; i++)
                {
                    charactersIcons[i].portraitRect.anchoredPosition3D = new Vector3(currents[i].x + (diffs[i].x * percent), currents[i].y + (diffs[i].y * percent), charactersIcons[i].portraitRect.anchoredPosition3D.z);
                }
            }
            else
            {
                isRearranging = false;
                isMoving = false;
                replaceCurrentTime = 0;
            }
        }
    }

    /// <summary>
    /// Set Icons parameters according to their characters specifics (image, enemmy or ally, etc.)
    /// </summary>
    public void SetUpIcons()
    {
        charactersIcons = new List<UI_Portrait>();
        iconPositions = new List<Vector3>();
        //Get the number of characters (knowing that the first one is the BOAT and the last one is the PLAYER)
        numberOfIcons = 2 + EnemyManager.instance.enemyList.Count;

        charactersPortraitParent.anchoredPosition3D = new Vector3(0 - (numberOfIcons * iconSizeref), charactersPortraitParent.anchoredPosition3D.y, 0);

        //Get number of characters
        for (int i = 0; i < numberOfIcons; i++)
        {
            GameObject obj = Instantiate(characterIconPrefab, Vector3.zero, Quaternion.identity, charactersPortraitParent);
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
                switch (EnemyManager.instance.enemyList[i - 1].enemyStats.enemyType)
                {
                    case EnemyData.EnemyType.Moussaillon:
                        values.backgroundImage.sprite = UI_Manager.instance.uiPreset.moussaillonImage;
                        values.portraitImage.sprite = UI_Manager.instance.uiPreset.moussaillonImage;
                        obj.name = "Mousaillon" + i;
                        break;

                    case EnemyData.EnemyType.Captain:
                        values.backgroundImage.sprite = UI_Manager.instance.uiPreset.captainImage;
                        values.portraitImage.sprite = UI_Manager.instance.uiPreset.captainImage;
                        obj.name = "Captain" + i;
                        break;

                    case EnemyData.EnemyType.Kamikaze:
                        values.backgroundImage.sprite = UI_Manager.instance.uiPreset.kamikazeImage;
                        values.portraitImage.sprite = UI_Manager.instance.uiPreset.kamikazeImage;
                        obj.name = "Kamikaze" + i;
                        break;

                    case EnemyData.EnemyType.Hooker:
                        values.backgroundImage.sprite = UI_Manager.instance.uiPreset.hookerImage;
                        values.portraitImage.sprite = UI_Manager.instance.uiPreset.hookerImage;
                        obj.name = "Hooker" + i;
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


            //Stock Index Position
            iconPositions.Add(newPos);
            charactersIcons.Add(values);
        }
    }

    public void RemoveCharacterAtIndex(int characterIndex)
    {
        charactersIcons[characterIndex].RemoveIcon();
    }

    /// <summary>
    /// Refresh the current position of the Timeline
    /// </summary>
    public void RefreshSelectedIcon(int indexToRemove)
    {
        isRearranging = false;
        isMoving = false;
        replaceCurrentTime = 0;

        //Check if it's not the beginning of the game
        if (selectedIcon < 0)
        {
            charactersIcons.RemoveAt(charactersIcons[indexToRemove].indexOrder);

            RefreshIconsOrder();

            return;
        }

        //If the current is removed, go to the previous one
        if (indexToRemove == selectedIcon)
        {
            selectedIcon--;
        }

        charactersIcons.RemoveAt(charactersIcons[indexToRemove].indexOrder);

        RefreshIconsOrder();
    }

    /// <summary>
    /// Refresh the list by removing removed character
    /// </summary>
    private void RefreshIconsOrder()
    {
        for (int i = 1; i < charactersIcons.Count; i++)
        {
            charactersIcons[i].indexOrder = i;
            if (charactersIcons[i].isSelected)
                selectedIcon = charactersIcons[i].indexOrder;
        }

        RearrangeIcons();
    }

    /// <summary>
    /// Set up values to replace ALL icons according to their new index
    /// </summary>
    private void RearrangeIcons()
    {
        currents = new List<Vector3>();
        diffs = new List<Vector3>();

        currentPanelPos = charactersPortraitParent.anchoredPosition3D;
        nextPanelPos = new Vector3(currentPanelPos.x + iconSizeref, currentPanelPos.y + iconSizeref, currentPanelPos.z + iconSizeref);
        diffPanelPos = new Vector3(nextPanelPos.x - currentPanelPos.x, nextPanelPos.y - currentPanelPos.y, nextPanelPos.z - currentPanelPos.z);

        for (int i = 0; i < charactersIcons.Count; i++)
        {
            currentIconPos = charactersIcons[i].portraitRect.anchoredPosition3D;
            diffIconPos = new Vector3(iconPositions[i].x - currentIconPos.x, iconPositions[i].y - currentIconPos.y, 0);

            currents.Add(currentIconPos);
            diffs.Add(diffIconPos);
        }

        isRearranging = true;
        isMoving = true;
    }

    /// <summary>
    /// Next Turn set up (incrementing for the next icon to move)
    /// </summary>
    public void NextIconTurn()
    {
        if (charactersIcons.Count == 0)
            return;

        int newIndex = 0;

        newIndex = selectedIcon + 1;

        if (newIndex > charactersIcons.Count - 1)
            newIndex = 0;

        if (selectedIcon > -1)
            charactersIcons[selectedIcon].MoveBackIcon();

        charactersIcons[newIndex].MoveSelectedIcon();

        selectedIcon = newIndex;
    }


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void MovePanel()
    {
        base.MovePanel();
    }
}
