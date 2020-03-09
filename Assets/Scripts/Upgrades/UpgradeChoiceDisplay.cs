using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeChoiceDisplay : MonoBehaviour
{
    

    public SpriteRenderer sr;
    public Transform graphics;
    public Transform rotator;

    public bool hovered;

    public float hoverScaleBonus;
    public float hoverScaleGain;
    public float hoverScaleReduction;

    Vector3 hoverScale;

    private void Awake()
    {
        hoverScale = Vector3.one * (1 + hoverScaleBonus);
    }

    private void Update()
    {
        graphics.LookAt(Camera.main.transform);
        
        if(hovered)
        {
            Vector3 newScale = graphics.transform.localScale + (Vector3.one * hoverScaleGain * Time.deltaTime);
            if (newScale.x > hoverScale.x) newScale = hoverScale;
            graphics.transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = graphics.transform.localScale - (Vector3.one * hoverScaleReduction * Time.deltaTime);
            if (newScale.x < 1) newScale = Vector3.one;
            graphics.transform.localScale = newScale;
        }
    }

    
}
