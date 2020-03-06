using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePawn : MonoBehaviour
{
    [SerializeField]protected Tile associatedTile;
    public LayerMask mask;
    [HideInInspector] public List<Tile> range = new List<Tile>();

    protected int skillPreviewID;
    protected bool _isMyTurn = false;
    protected bool _isDoingSomething = false;

    protected virtual void Start()
    {        
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, mask);
        //print("Pawn tile : " + hit.transform.name);
        associatedTile = hit.transform.GetComponent<Tile>();
        associatedTile.SetPawnOnTile(this);

    }

    public virtual void OnMouseEnter() { }
    public virtual void OnMouseExit() { }

    public Tile GetTile()
    {
        return associatedTile;
    }

    public int GetSkillPreviewID()
    {
        return skillPreviewID;
    }

    public void SetPreviewID(int id)
    {
        skillPreviewID = id;
    }

    void Update()
    {
        
    }

    public virtual void SetDestination(Tile destination, bool showHighlight = false)
    {
        //print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(associatedTile, destination);

        if (showHighlight)
            Highlight_Manager.instance.ShowHighlight(path, HighlightMode.MoveHighlight);

        Sequence s = DOTween.Sequence();
        foreach (Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    associatedTile.SetPawnOnTile(null);
                    associatedTile = tile;
                    associatedTile.SetPawnOnTile(this);
                    if (tile.highlighted)
                    {
                        associatedTile.rend.material = associatedTile.defaultMaterial;
                        associatedTile.highlighted = false;
                    }
                }));
            
        }

        s.OnComplete(() =>
        {
            _isDoingSomething = false;
        });
    }

    public void EndAction()
    {
        _isDoingSomething = false;
    }
}
