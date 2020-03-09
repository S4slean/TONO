using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_MessagePanel : MonoBehaviour
{
    [Header("Message References")]
    public Animator panelAnimator;
    public Image panelImage;
    public TextMeshProUGUI messageText;

    [TextArea] public string defeatMessage;
    [TextArea] public string victoryMessage;
    [TextArea] public string roundMessage;
    [TextArea] public string enemyMessage;
    [TextArea] public string playerMessage;
    [TextArea] public string boatMessage;



    private void Awake()
    {
        SetUI();
    }

    public enum Messages
    {
        Defeat,
        Victory,
        Round,
        EnemyTurn,
        PlayerTurn,
        BoatTurn,
    }

    public void ShowThisMessage(Messages message)
    {

    }

    private void SetUI()
    {
    }
}
