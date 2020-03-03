using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free : Tile {

    public Free(Vector3 position, float row, float column):base(position, row, column)
    {}

    void OnMouseEnter()
    {
        if(PlayerManager.instance.mouseMask == LayerMask.GetMask("Tile"))
        {
            hovered = true;
            rend.material = PlayerManager.instance.hoveringMaterial;
        }
    }
    void OnMouseExit()
    {
        if (hovered)
        {
            hovered = false;
            rend.material = defaultMaterial;
        }
    }

}
