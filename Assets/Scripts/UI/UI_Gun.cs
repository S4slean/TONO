using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gun : MonoBehaviour
{
    [Header("BULLETS")]
    public Image bulletImage;
    public RectTransform bulletRect;

    [Header("Animation")]

    [Header("Debug")]
    public bool isLoaded;



    private void Start()
    {
        SetUpBullet();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    RefreshUI();
        //}
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

    public void ShowBulletUI()
    {
        bulletImage.gameObject.SetActive(true);
    }

    public void HideBulletUI()
    {
        bulletImage.gameObject.SetActive(false);
    }
}
