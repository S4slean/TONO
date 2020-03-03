using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free : Tile
{
    private bool inShortestPath;
    private Material oldMaterial;

    public Free(Vector3 position, float row, float column):base(position, row, column)
    {}

    void OnMouseEnter()
    {
        if(PlayerManager.instance.mouseMask == LayerMask.GetMask("Tile"))
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

    public override void SetInShortestPath(bool inShortestPath)
    {
        this.inShortestPath = inShortestPath;
        if(inShortestPath)
            rend.material = PlayerManager.instance.highlightMaterial;
        else
            rend.material = defaultMaterial;
    }
}
