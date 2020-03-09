﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : GamePawn
{

    public bool standing = true;
    [HideInInspector]public Skill explosionSkill;

    public GameObject[] graphics;

    protected override void Start()
    {
        base.Start();
    }

    public void Initialize(BarrelType type)
    {
        explosionSkill = type.explosionSkill;
        for(int i = 0; i < graphics.Length; i ++)
        {
            graphics[i].SetActive(false);
        }

        graphics[type.graphicsIndex].SetActive(true);
    }

    public override void OnMouseEnter()
    {
        //print("SHOW PREVIEW BARREL : " + PlayerManager.instance.hoverMode);
        if (PlayerManager.instance.hoverMode != HoverMode.GunShotHover)
        {
            hovered = true;
            oldMaterial = rend.material;
            //rend.material = Highlight_Manager.instance.hoverMat;
            explosionSkill.Preview(this);
        }
    }

    public override void OnMouseExit()
    {
        if (hovered)
        {
            hovered = false;
            rend.material = oldMaterial;
            ComboManager.instance.ClearAllComboList();
            Highlight_Manager.instance.HideHighlight(GetSkillPreviewID());
        }
    }

    public virtual void Break()
    {

    }

    public virtual void Drink()
    {

    }

    public virtual void Kick(Vector3 direction)
    {

    }

    public virtual void Throw(Vector3 direction, float distance)
    {

    }

    public virtual void Explode()
    {
        explosionSkill.Activate(this, GetTile());
    }
}
