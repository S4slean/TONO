using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gun : Panel_Behaviour
{
    [Header("BULLETS")]
    public Image bulletImage;
    public RectTransform bulletRect;



    void Start()
    {
        SetUpBullet();
    }

    void Update()
    {
        MovePanel();
    }


    /// <summary>
    /// Call at the beginnning of the game to set the max number 4 Bullets
    /// </summary>
    public void SetUpBullet()
    {
        bulletImage.sprite = UI_Manager.instance.uiPreset.unusedBullet;
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (PlayerManager.instance.playerCharacter.isGunLoaded)
        {
            //bulletImage.color = new Color32((byte)255, (byte)255, (byte)255, (byte)255);
            bulletImage.sprite = UI_Manager.instance.uiPreset.unusedBullet;
            bulletRect.anchoredPosition3D = new Vector3(0, 0, 0);
        }
        else
        {
            //bulletImage.color = new Color32((byte)255, (byte)255, (byte)255, (byte)100);
            bulletImage.sprite = UI_Manager.instance.uiPreset.usedBullet;
            bulletRect.anchoredPosition3D = new Vector3(0, -20, 0);
        }
    }


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void MovePanel()
    {
        base.MovePanel();
    }
}
