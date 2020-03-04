using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_ActionPanelBehaviour : MonoBehaviour
{
    [Header("Panel References")]
    public GameObject actionButtonPrefab;
    public RectTransform rect;
    List<GameObject> actionGO;
    float actionSpace = 0;
    bool isDisplayed = false;

    [Header("Debug")]
    public int numberOfAcions;


    private void Awake()
    {
        actionSpace = actionButtonPrefab.GetComponent<RectTransform>().sizeDelta.x;
    }

    private void SetUpPanel()
    {
        isDisplayed = true;

        if (numberOfAcions == 0)
            return;

        actionGO = new List<GameObject>();

        //Set action panel
        for (int i = 0; i < numberOfAcions; i++)
        {
            GameObject obj = Instantiate(actionButtonPrefab, Vector3.zero, Quaternion.identity, this.transform);

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition3D = new Vector3(0, actionSpace * (i + 1), 0);

            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            //Get list of actions from cell data (ennemy, player, object, or empty path)
            text.text = "action " + i + 1;

            Button button = obj.GetComponent<Button>();
            //Add listener corresponding to each action
            button.onClick.AddListener(() => Debug.Log(text.text));

            actionGO.Add(obj);
        }

        /*
         * Check player character possibilities
         * 
         * ON PLAYER________
         * RELOAD - if currentNumberOfBullets < totalNumberOfBullets && enough PA
         * ATTACK - if ennemy near && enough PA
         * THROW - if object near && enough PA
         * PASS - if passable object near && enough PA
         * WAIT
         * 
         * ON ENNEMY________
         * THROW - if enough PA
         * ATTACK
         * 
         * ON INTERACTABLE OBJECT/COMBUSTIBLE_______
         * WAIT
         * FIRE - if enough bullet
         * 
         * ON EMPTY________
         * WAIT
         * 
         * ON ALL
         * CANCEL
         */
    }

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

    public void ShowPanelAction()
    {
        /////Get list of Actions/////

        if (isDisplayed)
            CleanPanel();

        SetUpPanel();
    }

    public void HidePanelAction()
    {
        CleanPanel();
    }
}
