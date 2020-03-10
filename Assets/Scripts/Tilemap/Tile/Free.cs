using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free : Tile
{
    int previewID;
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

                    previewID = Highlight_Manager.instance.ShowHighlight(path, HighlightMode.MoveHighlight,true);
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
            case HoverMode.ThrowElementHover:
                if (isClickable)
                {
                    PlayerManager.instance.currentHoveredTile = this;
                    oldMaterial = rend.material;
                    rend.material = Highlight_Manager.instance.actionHighlightMat;
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
                    Highlight_Manager.instance.HideHighlight(previewID, null, false);
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
            case HoverMode.ThrowElementHover:
                if (PlayerManager.instance.currentHoveredTile == this)
                {
                    PlayerManager.instance.currentHoveredTile = null;
                    rend.material = oldMaterial;
                }
                break;
        }
    }
}
