using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : GamePawn
{

    public bool standing = true;
    [HideInInspector]public Skill explosionSkill;

    protected override void Start()
    {
        base.Start();
    }

    public GameObject[] graphics;

    public void Initialize(BarrelType type)
    {
        for(int i =0; i < graphics.Length; i++)
        {
            graphics[i].SetActive(false);
        }
        graphics[type.graphicsIndex].SetActive(true);

        explosionSkill = type.explosionSkill;
    }

    public override void OnMouseEnter()
    {
        if (PlayerManager.instance.hoverMode != HoverMode.GunShotHover)
        {
            //print("SHOW PREVIEW BARREL " + explosionSkill.rangeType + " : " + PlayerManager.instance.hoverMode);
            hovered = true;
            //oldMaterial = rend.material;
            //rend.material = Highlight_Manager.instance.hoverMat;
            explosionSkill.Preview(this);
        }
    }

    public override void OnMouseExit()
    {
        if (hovered)
        {
            hovered = false;
            //rend.material = oldMaterial;
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
