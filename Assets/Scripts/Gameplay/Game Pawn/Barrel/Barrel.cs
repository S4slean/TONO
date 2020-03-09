using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : GamePawn
{

    public bool standing = true;
    private Skill explosionSkill;

    protected override void Start()
    {
        base.Start();

        foreach(Skill skill in skills)
        {
            if(skill.skillName == "Explosion")
            {
                explosionSkill = skill;
                break;
            }
        }
    }

    public override void OnMouseEnter()
    {
        if (PlayerManager.instance.hoverMode != HoverMode.GunShotHover)
        {
            print("SHOW PREVIEW BARREL : " + PlayerManager.instance.hoverMode);
            hovered = true;
            oldMaterial = rend.material;
            rend.material = Highlight_Manager.instance.hoverMat;
            explosionSkill.Preview(this);
        }
    }

    public override void OnMouseExit()
    {
        if (hovered)
        {
            hovered = false;
            rend.material = oldMaterial;
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
