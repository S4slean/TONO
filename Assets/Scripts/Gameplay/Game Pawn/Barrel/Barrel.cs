using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Barrel : GamePawn
{

    public bool standing = true;
    [HideInInspector]public Skill explosionSkill;
    private GamePawn kicker;

    protected override void Start()
    {
        base.Start();
    }

    public GameObject[] graphics;

    public void Initialize(BarrelType type)
    {
        for(int i =0; i < graphics.Length; i++)
        {
            graphics[i].SetActive(false);
        }
        graphics[type.graphicsIndex].SetActive(true);

        explosionSkill = type.explosionSkill;
    }

    public override void OnMouseEnter()
    {
        if (PlayerManager.instance.hoverMode != HoverMode.GunShotHover)
        {
            //print("SHOW PREVIEW BARREL " + explosionSkill.rangeType + " : " + PlayerManager.instance.hoverMode);
            hovered = true;
            //oldMaterial = rend.material;
            //rend.material = Highlight_Manager.instance.hoverMat;
            explosionSkill.Preview(this);
        }
    }

    public override void OnMouseExit()
    {
        if (hovered)
        {
            hovered = false;
            //rend.material = oldMaterial;
            ComboManager.instance.ClearAllComboList();
            Highlight_Manager.instance.HideHighlight(GetSkillPreviewID());
        }
    }

    public virtual void Break()
    {

    }

    public virtual void Drink()
    {

    }

    public virtual void Kick(Direction dir, GamePawn kicker)
    {
        List<Tile> path = GridManager.instance.GetLineUntilObstacle(dir, GetTile(), false);
        SetDestination(path[path.Count - 1]);
    }

    public override void SetDestination(Tile destination, bool showHighlight = false)
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


                kicker.EndAction();
                EndAction();
            });
        }
    }

    public virtual void Throw(Vector3 direction, float distance)
    {

    }

    public virtual void Explode()
    {
        explosionSkill.Activate(this, GetTile());
    }
}
