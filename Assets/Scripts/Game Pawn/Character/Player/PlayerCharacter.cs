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
    public Dictionary<string, bool> activatedSkill = new Dictionary<string, bool>();

    public List<Tile> gunRange = new List<Tile>();
    public List<Tile> lineUp = new List<Tile>();
    public List<Tile> lineRight = new List<Tile>();
    public List<Tile> lineDown = new List<Tile>();
    public List<Tile> lineLeft = new List<Tile>();

    protected override void Start()
    {
        rend = GetComponent<Renderer>();
        base.Start();
        PlayerManager.instance.playerCharacter = this;

        InitializeAllSkillRange(associatedTile);

        foreach (Skill skill in skills)
        {
            activatedSkill.Add(skill.name, false);
        }
    }

    public override void OnMouseEnter()
    {
        if(PlayerManager.instance.hoverMode == HoverMode.MovePath)
        {
            hovered = true;
            oldMaterial = rend.material;
            rend.material = Highlight_Manager.instance.hoverMat;
            ShowMoveRange();
        }
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

    public override void SetDestination(Tile destination, bool showHighlight = false)
    {
        base.SetDestination(destination, showHighlight);

        InitializeAllSkillRange(destination);
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
        SetPreviewID(Highlight_Manager.instance.ShowHighlight(moveRange, HighlightMode.MoveRangePreview));
    }

    public void HideMoveRange()
    {
        Highlight_Manager.instance.HideHighlight(GetSkillPreviewID());
    }

    public void ActivateSkill(Skill skill, Tile target)
    {
        skill.Activate(this, target);
    }

    public void InitializeAllSkillRange(Tile destination)
    {
        //Move Range
        foreach (Tile tile in moveRange)
        {
            tile.isClickable = false;
        }
        moveRange = Pathfinder_Dijkstra.instance.SearchForRange(destination, 5, false);
        foreach (Tile tile in moveRange)
        {
            tile.isClickable = true;
        }

        //Gun Range

        gunRange.Clear();
        lineUp.Clear();
        lineRight.Clear();
        lineDown.Clear();
        lineLeft.Clear();

        if(destination.neighbours.up != null && destination.neighbours.up.isWalkable && (destination.neighbours.up.GetPawnOnTile() == null || destination.neighbours.up.GetPawnOnTile() == this))
            lineUp = GridManager.instance.GetLineUntilObstacle(new List<Tile> { destination.neighbours.up }, Direction.Up);
        if (destination.neighbours.right != null && destination.neighbours.right.isWalkable && (destination.neighbours.right.GetPawnOnTile() == null || destination.neighbours.right.GetPawnOnTile() == this))
            lineRight = GridManager.instance.GetLineUntilObstacle(new List<Tile> { destination.neighbours.right }, Direction.Right);
        if (destination.neighbours.down != null && destination.neighbours.down.isWalkable && (destination.neighbours.down.GetPawnOnTile() == null || destination.neighbours.down.GetPawnOnTile() == this))
            lineDown = GridManager.instance.GetLineUntilObstacle(new List<Tile> { destination.neighbours.down }, Direction.Down);
        if (destination.neighbours.left != null && destination.neighbours.left.isWalkable && (destination.neighbours.left.GetPawnOnTile() == null || destination.neighbours.left.GetPawnOnTile() == this))
            lineLeft = GridManager.instance.GetLineUntilObstacle(new List<Tile> { destination.neighbours.left }, Direction.Left);

        gunRange = lineUp;
        gunRange.AddRange(lineRight);
        gunRange.AddRange(lineDown);
        gunRange.AddRange(lineLeft);
    }

}
