using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : GamePawn
{

    private Renderer rend;
    private Material oldMaterial;
    private bool hovered;

    public void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public Tile GetPlayerTile()
    {
        return associatedTile;
    }
    void OnMouseEnter()
    {
        if (PlayerManager.instance.mouseMask == LayerMask.GetMask("Player"))
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

    public void SetPlayerTile(Tile newTile)
    {
        associatedTile = newTile;
    }

    public void SetDestination(Tile destination)
    {
        print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder.instance.SearchForShortestPath(associatedTile, destination);
        Sequence s = DOTween.Sequence();
        foreach(Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    tile.SetInShortestPath(false);
                    associatedTile = tile;
                }));              
        }
    }
}
