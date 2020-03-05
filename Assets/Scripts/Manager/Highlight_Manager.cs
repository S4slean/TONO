using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightMode
{
    Hover,
    Movement,
    Range
}
public class Highlight_Manager : MonoBehaviour
{
    public static Highlight_Manager instance;

    public List<Material> previewMaterials;
    private int idKey = 0;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public int ShowHighlight(List<Tile> tilesToHighlight, HighlightMode highlightMode)
    {
        Material highlightMat = previewMaterials[(int)highlightMode];
        foreach(Tile tile in tilesToHighlight)
        {
            tile.highlighted = true;
            tile.rend.material = highlightMat;
        }

        idKey++;
        return idKey;
    }

    public void HideHighlight(List<Tile> tilesToHide)
    {
        foreach(Tile tile in tilesToHide)
        {
            tile.highlighted = false;
            tile.rend.material = tile.defaultMaterial;
        }
    }

}
