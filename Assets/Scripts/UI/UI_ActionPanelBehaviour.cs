using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ActionPanelBehaviour : MonoBehaviour
{
    [Header("Panel References")]
    public GameObject actionButtonPrefab;
    public RectTransform uiRect;
    List<GameObject> actionGO = new List<GameObject>();


    float actionHeight = 0;
    public float spacing = 100;
    bool isDisplayed = false;

    public UI_ActionButton selectedAction;


    [Header("Debug")]
    public int actionCost;
    //GetActionType


    private void Awake()
    {
        actionHeight = actionButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CleanPanel();
            SetUpPanel();
        }
    }



    public void SetUpTooltip(Skill skill)
    {
        selectedAction.actionCost = skill.cost;
        selectedAction.tooltipName.text = skill.skillName;
        selectedAction.tooltipDescription.text = skill.description;
        selectedAction.enabled = skill.enabledSprite;
        selectedAction.unenabled = skill.unenabledSprite;
    }


    /// <summary>
    /// Set Up panel action corresponding to a Character
    /// </summary>
    private void SetUpPanel()
    {
        if(PlayerManager.instance.playerCharacter == null)
        {
            Debug.LogError("NO PLAYER CHARACTER");
            return;
        }


        isDisplayed = true;

        if (PlayerManager.instance.playerCharacter.skills.Count == 0)
            return;


        actionGO = new List<GameObject>();

        //Set action panel
        for (int i = 0; i < PlayerManager.instance.playerCharacter.skills.Count; i++)
        {
            GameObject obj = Instantiate(actionButtonPrefab, Vector3.zero, Quaternion.identity, this.transform);
            selectedAction = obj.GetComponent<UI_ActionButton>();
            selectedAction.rect.anchoredPosition3D = new Vector3(0, spacing * i + actionHeight * i, 0);

            //Set tooltip information 4 action
            SetUpTooltip(PlayerManager.instance.playerCharacter.skills[i]);
            selectedAction.SetUpActionCost();

            actionGO.Add(obj);
        }
    }

    /// <summary>
    /// Clear panel
    /// </summary>
    private void CleanPanel()
    {
        if (actionGO.Count == 0)
        {
            isDisplayed = false;
            return;
        }

        for (int i = 0; i < actionGO.Count; i++)
        {
            Destroy(actionGO[i]);
        }
    }

    /// <summary>
    /// Clear then call the setUp function
    /// </summary>
    public void ShowPanelAction()
    {
        /////Get list of Actions/////

        if (isDisplayed)
            CleanPanel();

        SetUpPanel();
    }

    /// <summary>
    /// Call clear function
    /// </summary>
    public void HidePanelAction()
    {
        CleanPanel();
    }
}
