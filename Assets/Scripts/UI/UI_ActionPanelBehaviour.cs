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

    float actionSpace = 0;
    bool isDisplayed = false;

    public UI_ActionButton selectedAction;


    [Header("Debug")]
    public int numberOfActions;



    private void Awake()
    {
        actionSpace = actionButtonPrefab.GetComponent<RectTransform>().sizeDelta.x;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    CleanPanel();
        //    SetUpPanel();
        //}
    }



    public void SetUpTooltip(UI_ActionButton action)
    {

    }


    #region DEBUG
    public void Move()
    {
        Debug.Log("MOVE");
    }

    public void JUMP()
    {
        Debug.Log("JUMP");
    }

    public void SHOOT()
    {
        Debug.Log("SHOOT");
    }

    public void RELOAD()
    {
        Debug.Log("RELOAD");
    }

    public void SMOKE()
    {
        Debug.Log("SMOKE");
    }
    #endregion


    /// <summary>
    /// Set Up panel action corresponding to a Character
    /// </summary>
    private void SetUpPanel()
    {
        isDisplayed = true;

        float xStart = 0;
        


        if (numberOfActions != 0)
            xStart = (actionSpace / 2) * (numberOfActions - 1);

        uiRect.anchoredPosition3D = new Vector3(-xStart, -245, 0);

        if (numberOfActions == 0)
            return;


        actionGO = new List<GameObject>();

        //Set action panel
        for (int i = 0; i < numberOfActions; i++)
        {
            GameObject obj = Instantiate(actionButtonPrefab, Vector3.zero, Quaternion.identity, this.transform);
            UI_ActionButton actionButton = obj.GetComponent<UI_ActionButton>();
            actionButton.rect.anchoredPosition3D = new Vector3(actionSpace * (i), 0, 0);

            //Set tooltip information 4 action
            SetUpTooltip(actionButton);


            Button button = obj.GetComponent<Button>();
            //Add listener corresponding to each action SWITCH

            switch(i)
            {
                case 0:
                    button.onClick.AddListener(Move);
                    break;

                case 1:
                    button.onClick.AddListener(JUMP);
                    break;

                case 2:
                    button.onClick.AddListener(SHOOT);
                    break;

                case 3:
                    button.onClick.AddListener(RELOAD);
                    break;

                default:
                    break;
            }

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
