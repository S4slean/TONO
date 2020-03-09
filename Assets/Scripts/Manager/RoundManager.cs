using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int currentRound;
    public int currentTurn;

    public void StartGame()
    {
        currentRound = -1;
        currentTurn = -1;
        UI_Manager.instance.SetUIDisplayModeOn(UIDisplayMode.Start);
    }

    public void NextRound()
    {
        currentRound++;
        UI_Manager.instance.roundPanel.TurnWheel();
    }

    public void NextTurn()
    {
        currentTurn++;
        UI_Manager.instance.timelinePanel.NextIconTurn();
    }


}
