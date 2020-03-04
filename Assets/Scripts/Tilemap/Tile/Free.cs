using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState
{
    Default,
    Hovered,
    MoveHighlight,
}

public class Free : Tile
{
    private bool inShortestPath;
    private TileState oldState;
    public TileState tileState;


    void OnMouseEnter()
    {
        if(PlayerManager.instance.mouseMask == LayerMask.GetMask("Tile"))
        {
            oldState = tileState;
            tileState = TileState.Hovered;
            UpdateMaterial();
        }
    }
    void OnMouseExit()
    {
        if (tileState == TileState.Hovered)
        {
            tileState = oldState;
            UpdateMaterial();
        }
    }

    void UpdateMaterial()
    {
        switch (tileState)
        {
            case TileState.Default:
                rend.material = defaultMaterial;
                break;
            case TileState.Hovered:
                rend.material = PlayerManager.instance.hoveringMaterial;
                break;
            case TileState.MoveHighlight:
                rend.material = PlayerManager.instance.highlightMaterial;
                break;
        }
    }
    public override void SetInShortestPath(bool inShortestPath)
    {
        this.inShortestPath = inShortestPath;
        if(inShortestPath)
            tileState = TileState.MoveHighlight;
        else
            tileState = TileState.Default;

        UpdateMaterial();
    }
}
