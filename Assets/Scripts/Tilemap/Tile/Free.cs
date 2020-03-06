using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free : Tile
{
    void OnMouseEnter()
    {
        switch (PlayerManager.instance.hoverMode)
        {
            case HoverMode.MovePath:
                if (isWalkable && isClickable)
                {
                    PlayerCharacter player = PlayerManager.instance.playerCharacter;
                    List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(player.GetTile(), this);

                    player.SetPreviewID(Highlight_Manager.instance.ShowHighlight(path, HighlightMode.MoveHighlight));
                }
                break;
        }
    }
    void OnMouseExit()
    {
        switch (PlayerManager.instance.hoverMode)
        {
            case HoverMode.MovePath:
                if (highlighted)
                {
                    PlayerCharacter player = PlayerManager.instance.playerCharacter;
                    Highlight_Manager.instance.HideHighlight(player.GetSkillPreviewID());
                }
                break;
        }
    }
}
