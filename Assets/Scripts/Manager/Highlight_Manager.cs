using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightMode
{
    Hover,
    MoveHighlight,
    MoveRangePreview,
    ActionPreview,
    ActionHighlight,
    ExplosionPreview
}
public class Highlight_Manager : MonoBehaviour
{
    public static Highlight_Manager instance;

    public Material hoverMat;
    public Material moveRangePreviewMat;
    public Material moveHighlightMat;
    public Material actionPreviewMat;
    public Material actionHighlightMat;
    public Material explosionPreviewMat;

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
            case HighlightMode.ExplosionPreview:
                highlightMat = explosionPreviewMat;
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

        return GenerateNewID(tilesToHighlight);
    }

    public void HideHighlight(int id, Material materialAfterHiding = null)
    {
        List<Tile> value = new List<Tile>();
        if (highlights.TryGetValue(id, out value))
        {
            foreach (Tile tile in highlights[id])
            {
                tile.highlighted = false;
                if (materialAfterHiding != null)
                    tile.rend.material = materialAfterHiding;
                else
                    tile.rend.material = tile.defaultMaterial;
            }
            //Suppr la liste de highlights
            RemoveHighlight(id);
        }

    }

    public void HideAllHighlight()
    {
        foreach(int id in highlights.Keys)
        {
            HideHighlight(id);
        }
    }

    int GenerateNewID(List<Tile> tilesToHighlight)
    {
        //DEBUG
        /*foreach(int id in highlights.Keys)
        {
            print("ID : " + id);
        }*/

        int iDKey = 0;
        List<Tile> value = new List<Tile>();
        bool finding = true;
        while(finding)
        {
            if(!highlights.TryGetValue(iDKey, out value))
            {
                finding = false;
            }
            else
            {
                iDKey++;
            }
        }

        highlights.Add(iDKey, tilesToHighlight);

        //print("ID : " + iDKey);
        return iDKey;
    }

    void RemoveHighlight(int id)
    {
        List<Tile> value = new List<Tile>();
        if(highlights.TryGetValue(id, out value))
            highlights.Remove(id);
    }
}
