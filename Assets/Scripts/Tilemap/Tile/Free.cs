using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free : Tile
{
    protected GamePawn pawnOnTile;

    void OnMouseEnter()
    {
        if(PlayerManager.instance.mouseMask == LayerMask.GetMask("Tile"))
        {
            Highlight_Manager.instance.ShowHighlight(new List<Tile> { this }, HighlightMode.Hover);
        }
    }
    void OnMouseExit()
    {
        if (highlighted)
        {
            Highlight_Manager.instance.HideHighlight(new List<Tile> { this });
        }
    }

    public override void SetPawnOnTile(GamePawn pawn)
    {
        pawnOnTile = pawn;
    }
}
