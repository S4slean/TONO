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
    }

    public void NextRound()
    {
        currentRound++;
    }

    public void NextTurn()
    {
        currentTurn++;
    }


}
