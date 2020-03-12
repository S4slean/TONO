using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;


public class Free : Tile
{

    [Header("Decal")]
    public DecalProjector highlightCase;
    public DecalProjector ropeUp;
    public DecalProjector ropeDown;
    public DecalProjector ropeRight;
    public DecalProjector ropeLeft;

    protected int previewID;
    public override void OnMouseEnter()
    {
        switch (PlayerManager.instance.hoverMode)
        {
            case HoverMode.MovePath:
                if (isWalkable && isClickable && !PlayerManager.instance.playerCharacter.IsDoingSomething())
                {
                    PlayerManager.instance.currentHoveredTile = this;
                    PlayerCharacter player = PlayerManager.instance.playerCharacter;
                    if (PlayerManager.instance.showMoveRangeWithPathHighlight)
                    {
                        player.ShowMoveRange();
                    }
                    List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(player.GetTile(), this);

                    UI_Manager.instance.characterInfoPanel.ResetAllCharacterInfo();
                    UI_Manager.instance.characterInfoPanel.PreviewCharacterInfo(UI_SelectedCharacterInfo.Stats.PM, path.Count);

                    SetPreviewID(Highlight_Manager.instance.ShowHighlight(path, HighlightMode.MoveHighlight,true));
                }
                break;
            case HoverMode.Bombardment:
                if (!hasBarrelMarker && tag == "FreeTile")
                {
                    PlayerManager.instance.currentHoveredTile = this;
                    ActivateHighlight(HighlightMode.Hover);
                }
                break;
            case HoverMode.ThrowHover:
            case HoverMode.MeleeHover:
                if (isClickable)
                {
                    PlayerCharacter player = PlayerManager.instance.playerCharacter;
                    PlayerManager.instance.currentHoveredTile = this;
                    ActivateHighlight(HighlightMode.ActionHighlight);

                    if(SkillManager.instance.currentActiveSkill == player.kickSkill)
                    {
                        player.kickSkill.PreviewPawnPath(player, GetPawnOnTile());
                    }
                }
                break;
        }
    }
    public override void OnMouseExit()
    {
        switch (PlayerManager.instance.hoverMode)
        {
            case HoverMode.MovePath:
                if (highlighted)
                {
                    PlayerCharacter player = PlayerManager.instance.playerCharacter;
                    if (PlayerManager.instance.showMoveRangeWithPathHighlight)
                    {
                        player.HideMoveRange();
                    }
                    Highlight_Manager.instance.HideHighlight(GetPreviewID(), null, false);
                    UI_Manager.instance.characterInfoPanel.ResetCharacterInfo(UI_SelectedCharacterInfo.Stats.PM);
                    PlayerManager.instance.currentHoveredTile = null;
                }
                break;
            case HoverMode.Bombardment:
                if (PlayerManager.instance.currentHoveredTile == this)
                {
                    PlayerManager.instance.currentHoveredTile = null;
                    DeactivateHighlight();
                }
                break;
            case HoverMode.ThrowHover:
            case HoverMode.MeleeHover:
                if (PlayerManager.instance.currentHoveredTile == this)
                {
                    PlayerManager.instance.currentHoveredTile = null;
                    DeactivateHighlight();

                    if (SkillManager.instance.currentActiveSkill == PlayerManager.instance.playerCharacter.kickSkill)
                    {
                        Highlight_Manager.instance.HideHighlight(PlayerManager.instance.GetHighlineID());
                    }

                }
                break;
        }
    }

    public override void ActivateHighlight(HighlightMode highlightMode)
    {
        Highlight_Manager.instance.ActivateOutlines(new List<Tile> { this}, highlightMode, false);
    }

    public override void DeactivateHighlight()
    {
        Highlight_Manager.instance.DeactivateOutlines(this, false);
    }

    public void SetPreviewID(int id)
    {
        previewID = id;
    }

    public int GetPreviewID()
    {
        return previewID;
    }
}
