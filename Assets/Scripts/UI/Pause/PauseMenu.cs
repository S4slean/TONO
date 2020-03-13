using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public List<Button> buttons;
    public List<TextMeshProUGUI> buttonstexts;
    public List<PauseButtonType> buttonTypes;
    public Color baseTextColor;
    public Color selectedTextColor;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < buttons.Count; i++)
        {
            buttonstexts.Add(buttons[i].GetComponentInChildren<TextMeshProUGUI>());

            int index = i;
            buttons[i].onClick.AddListener(() => 
            {
                ActivateButton(buttonTypes[index]);
                SoundManager.Instance.PlaySound(SoundManager.Instance.click, false, true);
            });

            EventTrigger buttonEvent = buttons[i].GetComponent<EventTrigger>();

            buttonEvent.triggers[0].callback.AddListener((x) => { HoverButton(index); });
            buttonEvent.triggers[1].callback.AddListener((x) => { ExitButton(index); });
        }
    }

        
    public void HoverButton(int index)
    {
        buttonstexts[index].color = selectedTextColor;
    }

    public void ExitButton(int index)
    {
        buttonstexts[index].color = baseTextColor;
    }
    
    public void ExitAllButtons()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            ExitButton(i);
        }
    }

    public void ActivateButton(PauseButtonType type)
    {
        FindObjectOfType<EventSystem>().SetSelectedGameObject(null);

        if (type == PauseButtonType.quit)
        {
            PauseManager.Instance.Unpause();
            PauseManager.Instance.canPause = false;
            GameManager.Instance.SaveAndQuit();
        }
        else
        {
            PauseManager.Instance.canPause = true;
            PauseManager.Instance.Unpause();
        }
    }
}

public enum PauseButtonType
{
    resume,
    quit,
}
