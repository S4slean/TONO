﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Barrel : GamePawn
{
    public BarrelType startingExplosionType;
    public bool standing = true;
    [HideInInspector] public Skill explosionSkill;
    private GamePawn _kicker;

    protected override void Start()
    {
        base.Start();

        Initialize(startingExplosionType);
    }

    public override void OnEnable()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, mask);
        //print(name +" tile : " + hit.transform.name);
        if (hit.transform == null) return;
        associatedTile = hit.transform.GetComponent<Tile>();
        if (associatedTile.GetPawnOnTile() != null)
        {
            Explode();
        }
        else
        {
            associatedTile.SetPawnOnTile(this);
        }
    }

    public GameObject[] graphics;

    public void Initialize(BarrelType type)
    {
        for (int i = 0; i < graphics.Length; i++)
        {
            graphics[i].SetActive(false);
        }
        graphics[type.graphicsIndex].SetActive(true);

        explosionSkill = type.explosionSkill;
    }

    public override void OnMouseEnter()
    {
        if (PlayerManager.instance.hoverMode == HoverMode.MovePath || GameManager.Instance.turnType == TurnType.bombardment)
        {
            base.OnMouseEnter();
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
            base.OnMouseExit();
            hovered = false;
            //rend.material = oldMaterial;
            ComboManager.instance.ClearAllComboList();
            Highlight_Manager.instance.HideHighlight(GetSkillPreviewID());
        }
    }


    public override void OnKicked(GamePawn kicker, int damage, Direction dir)
    {
        List<Tile> path = GridManager.instance.GetLineUntilObstacle(dir, GetTile(), false);
        _kicker = kicker;
        SetDestination(path[path.Count - 1]);
        if (kicker is PlayerCharacter)
        {
            PlayerCharacter player = kicker as PlayerCharacter;
            PlayerManager.instance.hoverMode = HoverMode.MovePath;
            player.ShowMoveRange();
        }
    }

    public override void SetDestination(Tile destination, bool showHighlight = false, bool movedByPlayer = false)
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


                _kicker.EndAction();
                _kicker = null;
                EndAction();
            });
        }
    }


    public virtual void Explode()
    {

        explosionSkill.Activate(this, GetTile());
        Debug.Log("Boom");
        associatedTile.SetPawnOnTile(null);
        SetTile(null);
        BarrelManager.Instance.Repool(this);

    }

    public override void ReceiveDamage(int dmg)
    {
        Explode();
    }
}
