using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePawn : MonoBehaviour
{
    [SerializeField]protected Tile associatedTile;
    public LayerMask mask;

    protected virtual void Start()
    {        
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, mask);
        //print("Pawn tile : " + hit.transform.name);
        associatedTile = hit.transform.GetComponent<Tile>();
        associatedTile.SetPawnOnTile(this);
    }

    public Tile GetPlayerTile()
    {
        return associatedTile;
    }

    void Update()
    {
        
    }

    public void SetDestination(Tile destination, bool showHighlight = false)
    {
        //print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(associatedTile, destination);

        if (showHighlight)
            Highlight_Manager.instance.ShowHighlight(path, HighlightMode.Movement);

        Sequence s = DOTween.Sequence();
        foreach (Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Highlight_Manager.instance.HideHighlight(new List<Tile> { tile });
                    associatedTile.SetPawnOnTile(null);
                    associatedTile = tile;
                    associatedTile.SetPawnOnTile(this);
                }));
        }
    }

}
