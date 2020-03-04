using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private Tile playerTile;

    private Renderer rend;
    private Material oldMaterial;
    private bool hovered;

    public void Start()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, LayerMask.GetMask("FreeTile"));
        playerTile = hit.transform.GetComponent<Tile>();
        rend = GetComponent<Renderer>();
    }

    public Tile GetPlayerTile()
    {
        return playerTile;
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
        playerTile = newTile;
    }

    public void SetDestination(Tile destination)
    {
        print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder.instance.SearchForShortestPath(playerTile, destination);
        Sequence s = DOTween.Sequence();
        foreach(Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    tile.SetInShortestPath(false);
                    playerTile = tile;
                }));              
        }
    }
}
