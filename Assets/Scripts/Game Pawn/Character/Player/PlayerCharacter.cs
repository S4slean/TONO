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
    List<Skill> skills = new List<Skill>();

    protected override void Start()
    {
        rend = GetComponent<Renderer>();
        base.Start();
        PlayerManager.instance.playerCharacter = this;
    }

    void OnMouseEnter()
    {
        if (PlayerManager.instance.mouseMask == LayerMask.GetMask("Player"))
        {
            hovered = true;
            oldMaterial = rend.material;
            rend.material = PlayerManager.instance.hoveringMaterial;
        }
    }
    void OnMouseExit()
    {
        if (hovered)
        {
            hovered = false;
            rend.material = oldMaterial;
        }
    }

    public void SetPlayerTile(Tile newTile)
    {
        associatedTile = newTile;
    }

    public void ShowSkillPreview(Skill skill)
    {
        skill.Preview(this);
    }

    public void ActivateSkill(Skill skill, Tile target)
    {
        skill.Activate(this, target);
    }
}
