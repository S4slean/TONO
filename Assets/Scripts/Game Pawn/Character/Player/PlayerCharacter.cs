using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : GamePawn
{
    //GRAPHIC
    private Renderer rend;
    private Material oldMaterial;

    //LOGIC
    private bool hovered;
    public List<Skill> skills = new List<Skill>();
    private Dictionary<string, bool> activatedSkill = new Dictionary<string, bool>();

    protected override void Start()
    {
        rend = GetComponent<Renderer>();
        base.Start();
        PlayerManager.instance.playerCharacter = this;

        foreach(Skill skill in skills)
        {
            activatedSkill.Add(skill.name, false);
        }
    }

    public override void OnMouseEnter()
    {
        hovered = true;
        oldMaterial = rend.material;
        rend.material = Highlight_Manager.instance.previewMaterials[0];
        ShowMoveRange();
    }
    public override void OnMouseExit()
    {
        if (hovered)
        {
            hovered = false;
            rend.material = oldMaterial;
            HideMoveRange();
        }
    }

    public void SetPlayerTile(Tile newTile)
    {
        associatedTile = newTile;
    }

    public void ShowSkillPreview(Skill skill)
    {
        if (activatedSkill[skill.name])
        {
            Highlight_Manager.instance.HideHighlight(skillPreviewID);
            activatedSkill[skill.name] = false;
        }
        else
        {
            skill.Preview(this);
            activatedSkill[skill.name] = true;
        }
    }

    public void ShowMoveRange()
    {
        SetPreviewID(Highlight_Manager.instance.ShowHighlight(Pathfinder_Dijkstra.instance.SearchForRange(GetTile(), 5, false), HighlightMode.Range));
    }

    public void HideMoveRange()
    {
        Highlight_Manager.instance.HideHighlight(GetSkillPreviewID());
    }

    public void ActivateSkill(Skill skill, Tile target)
    {
        skill.Activate(this, target);
    }
}
