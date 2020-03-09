using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_MessagePanel : MonoBehaviour
{
    public enum Messages
    {
        Defeat,
        Victory,
        Round,
        EnemyTurn,
        PlayerTurn,
        BoatTurn,
    }

    [Header("Message References")]
    public Animator panelAnimator;
    public Image panelImage;
    public Image leftStringImage;
    public Image rightStringImage;
    public TextMeshProUGUI messageText;

    [Header("Messages")]
    [TextArea] public string defeatMessage;
    [TextArea] public string victoryMessage;
    [TextArea] public string enemyMessage;
    [TextArea] public string playerMessage;
    [TextArea] public string boatMessage;



    private void Start()
    {
        //SetUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowMessage(Messages.Victory);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowMessage(Messages.PlayerTurn);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowMessage(Messages.Defeat);
        }
    }

    public void ShowMessage(Messages message)
    {
        switch (message)
        {
            case Messages.Defeat:
                messageText.text = defeatMessage;
                panelAnimator.Play("Message_Defeat");
                break;

            case Messages.Victory:
                messageText.text = victoryMessage;
                panelAnimator.Play("Message_Victory");
                break;

            case Messages.EnemyTurn:
                messageText.text = enemyMessage;
                panelAnimator.Play("Message_Mundane");
                break;

            case Messages.PlayerTurn:
                messageText.text = playerMessage;
                panelAnimator.Play("Message_Mundane");
                break;

            case Messages.BoatTurn:
                messageText.text = boatMessage;
                panelAnimator.Play("Message_Mundane");
                break;
        }
    }

    private void SetUI()
    {
        panelImage.sprite = UI_Manager.instance.uiPreset.messagePanelImage;
        leftStringImage.sprite = UI_Manager.instance.uiPreset.leftStringImage;
        rightStringImage.sprite = UI_Manager.instance.uiPreset.rightStringImage;
    }
}
