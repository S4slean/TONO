using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPanelBehaviour : MonoBehaviour
{
    [Header("Panel References")]
    public GameObject actionButtonPrefab;
    public RectTransform rect;
    float actionSpace = 17f;

    public void ShowPanelAction(Vector2 panelPos)
    {
        rect.anchoredPosition = panelPos;

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
}
