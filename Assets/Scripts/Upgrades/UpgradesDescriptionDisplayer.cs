using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesDescriptionDisplayer : MonoBehaviour
{
    public static UpgradesDescriptionDisplayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public TextMeshPro textMesh;

    public UpgradeData displayedData;

    public UpgradeDisplayer[] displayers;

    private void Update()
    {
        bool displays = false;
        for(int i =0; i < displayers.Length; i++)
        {
           if(displayers[i].hovered)
            {
                displays = true;
            }
        }

        if(displays)
        {
            textMesh.text = displayedData.description;
        }
        else
        {
            textMesh.text = "";
        }
    }

}
