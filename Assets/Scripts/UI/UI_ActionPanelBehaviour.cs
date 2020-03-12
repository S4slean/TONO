using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActionPanelBehaviour : Panel_Behaviour
{
    [Header("Skills/Actions References")]
    public GameObject actionButtonPrefab;
    public RectTransform uiRect;
    List<UI_ActionButton> actions = new List<UI_ActionButton>();

    float actionHeight = 0;
    public float spacing = 100;
    bool isDisplayed = false;

    public UI_ActionButton selectedAction;



    void Awake()
    {
        actionHeight = actionButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    void Update()
    {
        MovePanel();

        if (Input.GetMouseButtonDown(1) && selectedAction != null)
        {
            selectedAction = null;
        }
    }


    /// <summary>
    /// Set Up panel action corresponding to a Character
    /// </summary>
    private void SetUpPanel()
    {
        if (PlayerManager.instance.playerCharacter == null)
        {
            Debug.LogError("NO PLAYER CHARACTER");
            return;
        }


        isDisplayed = true;

        if (PlayerManager.instance.playerCharacter.skills.Count == 0)
            return;


        actions = new List<UI_ActionButton>();

        //Set action panel
        for (int i = 0; i < PlayerManager.instance.playerCharacter.skills.Count; i++)
        {
            GameObject obj = Instantiate(actionButtonPrefab, Vector3.zero, Quaternion.identity, this.transform);
            selectedAction = obj.GetComponent<UI_ActionButton>();
            selectedAction.rect.anchoredPosition3D = new Vector3(0, spacing * i + actionHeight * i, 0);

            //Set tooltip information 4 action
            selectedAction.actionSkill = PlayerManager.instance.playerCharacter.skills[i];
            selectedAction.backgroundImage.sprite = UI_Manager.instance.uiPreset.skillBackgroundImage;
            selectedAction.SetUpActionPointsDisplay();

            if (PlayerManager.instance.playerCharacter != null)
                selectedAction.CheckAndRefreshActionUI(PlayerManager.instance.playerCharacter.currentPA);

            actions.Add(selectedAction);
        }

        selectedAction.actionPanel = this;

        selectedAction = null;
    }

    /// <summary>
    /// Clear panel
    /// </summary>
    public void ClearPanelAction()
    {
        if (actions.Count == 0)
        {
            isDisplayed = false;
            return;
        }

        for (int i = 0; i < actions.Count; i++)
        {
            Destroy(actions[i].gameObject);
        }
    }

    /// <summary>
    /// Clear then call the setUp function
    /// </summary>
    public void ResetPanelAction()
    {
        /////Get list of Actions/////
        if (isDisplayed)
            ClearPanelAction();

        SetUpPanel();
    }

    /// <summary>
    /// Refresh action ICON according to the player character's current PA
    /// </summary>
    public void RefreshActions()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].CheckAndRefreshActionUI(PlayerManager.instance.playerCharacter.currentPA);
        }
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
