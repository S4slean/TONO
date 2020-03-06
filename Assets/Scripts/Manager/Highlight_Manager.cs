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

    public Material hoverMat;
    public Material moveRangePreviewMat;
    public Material moveHighlightMat;
    public Material actionPreviewMat;
    public Material actionHighlightMat;

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
        Material highlightMat;

        switch (highlightMode)
        {
            case HighlightMode.MoveHighlight:
                highlightMat = moveHighlightMat;
                break;
            case HighlightMode.MoveRangePreview:
                highlightMat = moveRangePreviewMat;
                break;
            case HighlightMode.ActionPreview:
                highlightMat = actionPreviewMat;
                break;
            case HighlightMode.ActionHighlight:
                highlightMat = actionHighlightMat;
                break;
            default:
                highlightMat = hoverMat;
                break;
        }

        foreach(Tile tile in tilesToHighlight)
        {
            tile.highlighted = true;
            tile.rend.material = highlightMat;
        }

        idKey++;
        highlights.Add(idKey, tilesToHighlight);

        return idKey;
    }

    public void HideHighlight(int id, Material materialAfterHiding = null)
    {
        foreach(Tile tile in highlights[id])
        {
            tile.highlighted = false;
            if (materialAfterHiding != null)
                tile.rend.material = materialAfterHiding;
            else
                tile.rend.material = tile.defaultMaterial;
        }
        //Suppr la liste de highlights
    }
}
