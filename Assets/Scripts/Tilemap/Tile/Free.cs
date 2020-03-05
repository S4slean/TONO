using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free : Tile
{
    protected GamePawn pawnOnTile;
    protected bool hovered;
    public Material materialBeforeHover;

    void OnMouseEnter()
    {
        if(PlayerManager.instance.mouseMask == LayerMask.GetMask("Tile") && isWalkable)
        {
            hovered = true;
            materialBeforeHover = rend.material;
            rend.material = Highlight_Manager.instance.previewMaterials[0];
        }
    }
    void OnMouseExit()
    {
        if (hovered && !highlighted)
        {
            hovered = false;
            rend.material = materialBeforeHover;
            materialBeforeHover = null;
        }
    }

    public override void SetPawnOnTile(GamePawn pawn)
    {
        pawnOnTile = pawn;
    }
}
