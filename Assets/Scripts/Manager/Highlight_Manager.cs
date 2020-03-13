using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightMode
{
    NoHighlight,
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

    [Header("Material Decal Case")]
    public Material hoverDecalCaseMat;
    public Material moveRangePreviewDecalCaseMat;
    public Material moveHighlightDecalCaseMat;
    public Material actionPreviewDecalCaseMat;
    public Material actionHighlightMDecalCaseMat;
    public Material explosionPreviewDecalCaseMat;

    [Header("Material Decal Corde")]
    public Material hoverDecalCordeMat;
    public Material moveRangePreviewDecalCordeMat;
    public Material moveHighlightDecalCordeMat;
    public Material actionPreviewDecalCordeMat;
    public Material actionHighlightMDecalCordeMat;
    public Material explosionPreviewDecalCordeMat;

    [Header("Highlight Color")]
    public Color hoverColor;
    public Color moveRangePreviewColor;
    public Color moveHighlightColor;
    public Color actionPreviewColor;
    public Color actionHighlightColor;
    public Color explosionPreviewColor;    

    //LOGIC
    Dictionary<int, List<Tile>> highlights = new Dictionary<int, List<Tile>>();

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public int ShowHighlight(List<Tile> tilesToHighlight, HighlightMode highlightMode, bool tilesBecameClickable = false)
    {
        /*Material highlightMat;

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
            if (tilesBecameClickable)
            {
                tile.isClickable = true;
            }
        }*/

        ActivateOutlines(tilesToHighlight, highlightMode);
        return GenerateNewID(tilesToHighlight);
    }

    public void HideHighlight(int id, Material materialAfterHiding = null, bool tilesBecameNotClickable = true)
    {
        List<Tile> value = new List<Tile>();
        if (highlights.TryGetValue(id, out value))
        {
            foreach (Tile tile in highlights[id])
            {
                /*tile.highlighted = false;
                if (materialAfterHiding != null)
                    tile.rend.material = materialAfterHiding;
                else
                    tile.rend.material = tile.defaultMaterial;
                if(tilesBecameNotClickable)
                    tile.isClickable = false;*/

                DeactivateOutlines(tile, tilesBecameNotClickable);
            }
            //Suppr la liste de highlights
            RemoveHighlight(id);
        }
    }

    public void HideAllHighlight()
    {
        GridManager.instance.AllTilesBecameNotClickable();
        foreach(int id in highlights.Keys)
        {
            HideHighlight(id);
            print("HIDE");
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

    public void ActivateOutlines(List<Tile> tilesToHighlight, HighlightMode highlightMode, bool tilesBecameNotClickable = true)
    {
        Color highlightColor;
        Material highlightCaseMat, highlightCordeMat;
        switch (highlightMode)
        {
            case HighlightMode.MoveRangePreview:
                highlightColor = moveRangePreviewColor;
                highlightCaseMat = moveRangePreviewDecalCaseMat;
                highlightCordeMat = moveRangePreviewDecalCordeMat;
                break;

            case HighlightMode.MoveHighlight:
                highlightColor = moveHighlightColor;
                highlightCaseMat = moveHighlightDecalCaseMat;
                highlightCordeMat = moveHighlightDecalCordeMat;
                break;

            case HighlightMode.ActionPreview:
                highlightColor = actionPreviewColor;
                highlightCaseMat = actionPreviewDecalCaseMat;
                highlightCordeMat = actionPreviewDecalCordeMat;
                break;

            case HighlightMode.ActionHighlight:
                highlightColor = actionHighlightColor;
                highlightCaseMat = actionHighlightMDecalCaseMat;
                highlightCordeMat = actionHighlightMDecalCordeMat;
                break;

            case HighlightMode.ExplosionPreview:
                highlightColor = explosionPreviewColor;
                highlightCaseMat = explosionPreviewDecalCaseMat;
                highlightCordeMat = explosionPreviewDecalCordeMat;
                break;

            default:
                highlightColor = hoverColor;
                highlightCaseMat = hoverDecalCaseMat;
                highlightCordeMat = hoverDecalCordeMat;
                break;
        }

        Free tile;
        foreach (Tile t in tilesToHighlight)
        {
            if (!(t is Free))
                continue;
            tile = t as Free;

            tile.highlightCase.enabled = false;
            tile.ropeUp.enabled = false;
            tile.ropeRight.enabled = false;
            tile.ropeDown.enabled = false;
            tile.ropeLeft.enabled = false;

            //Change case decal material
            tile.highlightCase.material = highlightCaseMat;

            //Change corde decal material
            tile.ropeUp.material = highlightCordeMat;
            tile.ropeRight.material = highlightCordeMat;
            tile.ropeDown.material = highlightCordeMat;
            tile.ropeLeft.material = highlightCordeMat;

            //Change highlight color
            tile.highlightCase.material.SetColor("_EmissiveColor", highlightColor);
            tile.highlightCase.material.SetColor("_BaseColor", highlightColor);
            tile.ropeUp.material.SetColor("_EmissiveColor", highlightColor);
            tile.ropeUp.material.SetColor("_BaseColor", highlightColor);
            tile.ropeRight.material.SetColor("_EmissiveColor", highlightColor);
            tile.ropeRight.material.SetColor("_BaseColor", highlightColor);
            tile.ropeDown.material.SetColor("_EmissiveColor", highlightColor);
            tile.ropeDown.material.SetColor("_BaseColor", highlightColor);
            tile.ropeLeft.material.SetColor("_EmissiveColor", highlightColor);
            tile.ropeLeft.material.SetColor("_BaseColor", highlightColor);

            tile.highlighted = true;
            if (tilesBecameNotClickable)
                tile.isClickable = true;

            //Highlight de la case  enable
            tile.highlightCase.enabled = true;

            //Check for neighbours

            //UP
            Tile currentNeighbour = tile.neighbours.up;
            if (IsTileAvailableToHighlight(currentNeighbour))
            {
                if (tilesToHighlight.Contains(currentNeighbour))
                {
                    tile.ropeUp.enabled = false;
                }
                else
                {
                    tile.ropeUp.enabled = true;
                }
            }
            else
            {
                tile.ropeUp.enabled = true;
            }

            //RIGHT
            currentNeighbour = tile.neighbours.right;
            if (IsTileAvailableToHighlight(currentNeighbour))
            {
                if (tilesToHighlight.Contains(currentNeighbour))
                {
                    tile.ropeRight.enabled = false;
                }
                else
                {
                    tile.ropeRight.enabled = true;
                }
            }
            else
            {
                tile.ropeRight.enabled = true;
            }

            //DOWN
            currentNeighbour = tile.neighbours.down;
            if (IsTileAvailableToHighlight(currentNeighbour))
            {
                if (tilesToHighlight.Contains(currentNeighbour))
                {
                    tile.ropeDown.enabled = false;
                }
                else
                {
                    tile.ropeDown.enabled = true;
                }
            }
            else
            {
                tile.ropeDown.enabled = true;
            }

            //LEFT
            currentNeighbour = tile.neighbours.left;
            if (IsTileAvailableToHighlight(currentNeighbour))
            {
                if (tilesToHighlight.Contains(currentNeighbour))
                {
                    tile.ropeLeft.enabled = false;
                }
                else
                {
                    tile.ropeLeft.enabled = true;
                }
            }
            else
            {
                tile.ropeLeft.enabled = true;
            }

        }
    }

    public void DeactivateOutlines(Tile t, bool tilesBecameNotClickable = true, HighlightMode highlightModeAfter = HighlightMode.NoHighlight)
    {
        if (!(t is Free))
            return;
        Free tile = t as Free;

        tile.highlighted = false;

        tile.highlightCase.enabled = false;
        tile.ropeUp.enabled = false;
        tile.ropeRight.enabled = false;
        tile.ropeDown.enabled = false;
        tile.ropeLeft.enabled = false;

        if (tilesBecameNotClickable)
            tile.isClickable = false;

        switch (highlightModeAfter)
        {
            case HighlightMode.NoHighlight:
                break;
            default:
                ActivateOutlines(new List<Tile>() { t}, highlightModeAfter);
                break;
        }
    }

    bool IsTileAvailableToHighlight(Tile tile)
    {
        if (tile != null && !(tile is Wall) && !(tile is Water))
            return true;
        else
            return false;
    }
}
