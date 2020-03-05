using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gun : MonoBehaviour
{
    [Header("GUN")]
    public Image gunImage;
    public Sprite gunUsableSprite;
    public Sprite gunReloadSprite;

    [Header("BULLETS")]
    public Transform bulletsParent;
    List<Image> bulletsImage;
    public GameObject uiBulletPrefab;
    public Sprite bulletFullSprite;
    public Sprite bulletEmptySprite;

    [Header("Debug")]
    public int totalOfBullets;
    public int currentNumberOfBullets;


    //private void Awake()
    //{
    //    SetUpBulletCounterUI();
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        RefreshUI();
    //    }
    //}


    /// <summary>
    /// Call at the beginnning of the game to set the max number 4 Bullets
    /// </summary>
    public void SetUpBulletCounterUI()
    {
        bulletsImage = new List<Image>();

        //Check/Get total bullets number
        for (int i = 0; i < totalOfBullets; i++)
        {
            GameObject obj = Instantiate(uiBulletPrefab, Vector2.zero, Quaternion.identity, bulletsParent);
            Image image = obj.GetComponent<Image>();
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition3D = new Vector3((rect.sizeDelta.x * i), 0, 0);

            bulletsImage.Add(image);
        }

        RefreshUI();
    }

    /// <summary>
    /// Call refresh functions
    /// </summary>
    public void RefreshUI()
    {
        RefreshUIBulletsImage();
        RefreshUIGunImage();
    }

    public void ShowGunUI()
    {
        bulletsParent.gameObject.SetActive(true);
        gunImage.gameObject.SetActive(true);
    }

    public void HideGunUI()
    {
        bulletsParent.gameObject.SetActive(false);
        gunImage.gameObject.SetActive(false);
    }


    /// <summary>
    /// Refresh gun sprite corresponding to the current number of bullets
    /// </summary>
    private void RefreshUIGunImage()
    {
        //Check bullets number
        if (currentNumberOfBullets == 0)
            gunImage.sprite = gunReloadSprite;
        else
            gunImage.sprite = gunUsableSprite;
    }

    /// <summary>
    /// Refresh bullet(s) sprite(s) corresponding to the current number of bullets
    /// </summary>
    private void RefreshUIBulletsImage()
    {
        for (int i = 0; i < bulletsImage.Count; i++)
        {
            if (i < currentNumberOfBullets)
                bulletsImage[i].sprite = bulletFullSprite;
            else
                bulletsImage[i].sprite = bulletEmptySprite;
        }
    }

    
}
