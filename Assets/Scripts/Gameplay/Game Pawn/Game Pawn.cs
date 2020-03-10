using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePawn : MonoBehaviour
{
    //GRAPHIC
    protected Renderer rend;
    protected Material oldMaterial;
    public LayerMask mask;

    [SerializeField] protected Tile associatedTile;
    public List<Tile> moveRange = new List<Tile>();


    //LOGIC
    protected bool hovered;
    public int skillPreviewID;
    protected bool _isMyTurn = false;
    protected bool _isDoingSomething = false;
    public List<Skill> skills = new List<Skill>();

    protected virtual void Start()
    {
        rend = GetComponent<Renderer>();

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, mask);
        //print(name +" tile : " + hit.transform.name);
        associatedTile = hit.transform.GetComponent<Tile>();
        associatedTile.SetPawnOnTile(this);
    }

    public virtual void OnMouseEnter() { }
    public virtual void OnMouseExit() { }

    public Tile GetTile()
    {
        return associatedTile;
    }

    public void SetTile(Tile newTile)
    {
        associatedTile = newTile;
        associatedTile.SetPawnOnTile(this);
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

        if (path.Count != 0)
        {
            int highlightPathID = -1;

            if (showHighlight)
            {
                Highlight_Manager.instance.HideAllHighlight();
                highlightPathID = Highlight_Manager.instance.ShowHighlight(path, HighlightMode.MoveHighlight);
            }

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
                if (highlightPathID > -1)
                    Highlight_Manager.instance.HideHighlight(highlightPathID);
                EndAction();
            });
        }
    }

    public void EndAction()
    {
        _isDoingSomething = false;
    }

    public void BeginAction()
    {
        _isDoingSomething = true;
    }

    public bool IsDoingSomething()
    {
        return _isDoingSomething;
    }

    public virtual void OnKicked(GamePawn user, int dmg, Direction dir)
    {

    }

    public virtual void ReceiveDamage(int dmg)
    {
        
    }

    public virtual void Die()
    {

    }


}
