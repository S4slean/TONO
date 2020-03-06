using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightMode
{
    Hover,
    MoveHighlight,
    MoveRangePreview,
    ActionPreview,
    ActionHighlight
}
public class Highlight_Manager : MonoBehaviour
{
    public static Highlight_Manager instance;

    public List<Material> previewMaterials;
    private int idKey = 0;

    //LOGIC
    Dictionary<int, List<Tile>> highlights = new Dictionary<int, List<Tile>>();

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
        highlights.Add(idKey, tilesToHighlight);

        return idKey;
    }

    public void HideHighlight(int id)
    {
        foreach(Tile tile in highlights[id])
        {
            tile.highlighted = false;
            tile.rend.material = tile.defaultMaterial;
        }
        //Suppr la liste de highlights
    }
}
