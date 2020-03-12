using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;


public class Free : Tile
{

    [Header("Decal")]
    public DecalProjector highlightCase;
    public GameObject ropeUp;
    public GameObject ropDown;
    public GameObject ropeRight;
    public GameObject ropeLeft;

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
                    oldMaterial = rend.material;
                    rend.material = Highlight_Manager.instance.hoverMat;
                }
                break;
            case HoverMode.ThrowHover:
            case HoverMode.MeleeHover:
                if (isClickable)
                {
                    PlayerCharacter player = PlayerManager.instance.playerCharacter;
                    PlayerManager.instance.currentHoveredTile = this;
                    oldMaterial = rend.material;
                    rend.material = Highlight_Manager.instance.actionHighlightMat;

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
                    rend.material = oldMaterial;
                }
                break;
            case HoverMode.ThrowHover:
            case HoverMode.MeleeHover:
                if (PlayerManager.instance.currentHoveredTile == this)
                {
                    PlayerManager.instance.currentHoveredTile = null;
                    rend.material = oldMaterial;

                    if (SkillManager.instance.currentActiveSkill == PlayerManager.instance.playerCharacter.kickSkill)
                    {
                        Highlight_Manager.instance.HideHighlight(PlayerManager.instance.GetHighlineID());
                    }

                }
                break;
        }
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
