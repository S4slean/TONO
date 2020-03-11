using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CinematicDialogueDisplay : MonoBehaviour
{
    public GameObject dialogueDisplay;
    public TextMeshProUGUI dialogueTextMesh;

    public static CinematicDialogueDisplay Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialogueDisplay.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (count <= 0) return;

        count -= Time.deltaTime;
        if(count <= 0)
        {
            count = 0f;
            HideDialogue();
        }
    }

    public float dialogueDuration;
    float count;
    public void DisplayDialogue(string dialogue)
    {
        count = dialogueDuration;
        dialogueTextMesh.text = dialogue;
        dialogueDisplay.SetActive(true);
    }

    public void HideDialogue()
    {
        dialogueDisplay.SetActive(false);
    }
}
