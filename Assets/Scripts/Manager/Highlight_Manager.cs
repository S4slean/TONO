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

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowHighlight(List<Tile> tilesToHighlight, HighlightMode highlightMode)
    {
        Material highlightMat = previewMaterials[(int)highlightMode];
        foreach(Tile tile in tilesToHighlight)
        {
            tile.highlighted = true;
            tile.rend.material = highlightMat;
        }
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
