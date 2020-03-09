using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free : Tile
{
    int previewID;
    void OnMouseEnter()
    {
        switch (PlayerManager.instance.hoverMode)
        {
            case HoverMode.MovePath:
                if (isWalkable && isClickable && !PlayerManager.instance.playerCharacter.IsDoingSomething())
                {
                    PlayerCharacter player = PlayerManager.instance.playerCharacter;
                    if (PlayerManager.instance.showMoveRangeWithPathHighlight)
                    {
                        player.ShowMoveRange();
                    }
                    List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(player.GetTile(), this);

                    previewID = Highlight_Manager.instance.ShowHighlight(path, HighlightMode.MoveHighlight);
                }
                break;
            case HoverMode.Bombardment:

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
                    if (PlayerManager.instance.showMoveRangeWithPathHighlight)
                    {
                        player.HideMoveRange();
                    }
                    Highlight_Manager.instance.HideHighlight(previewID);
                }
                break;
        }
    }
}
