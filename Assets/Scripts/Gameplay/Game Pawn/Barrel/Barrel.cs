using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Barrel : GamePawn
{
    public BarrelType startingExplosionType;
    public bool noStartingInit;
    public bool standing = true;
    //[HideInInspector]
    public Skill explosionSkill;
    private GamePawn _kicker;
    public Animator anim;

    protected override void Start()
    {
        base.Start();

        if (!noStartingInit)
            Initialize(startingExplosionType);
    }

    public override void OnEnable()
    {

        RaycastHit hit;
        Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, mask);
        if (hit.transform == null)  return;

        SetAssociatedTile(hit.transform.GetComponent<Tile>());
        if (GetTile().GetPawnOnTile() != null)
        {
            if (GetTile().GetPawnOnTile() == PlayerManager.instance.playerCharacter)
                Explode();
            else
            {
            GetTile().GetPawnOnTile().ReceiveDamage(1);

            }
        }

        GetTile().SetPawnOnTile(this);
        anim.Play("Barrel Fall");



    }

    public GameObject[] graphics;

    public void Initialize(BarrelType type)
    {
        for (int i = 0; i < graphics.Length; i++)
        {
            graphics[i].SetActive(false);
        }
        graphics[type.graphicsIndex].SetActive(true);
        isExplosing = false;
        explosionSkill = type.explosionSkill;
    }

    public override void OnMouseEnter()
    {
        if (explosionSkill == null) return;

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
        switch (dir)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(Vector3.zero);
                break;

            case Direction.Right:
                transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;

            case Direction.Left:
                transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                break;

            case Direction.Down:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
        }

        anim.SetBool("Rolling", true);
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

                anim.SetBool("Rolling", false);
                _kicker.EndAction();
                _kicker = null;
                EndAction();
            });
        }
    }


    public virtual void Explode()
    {
        if (isExplosing) return;

        if (CameraShake.Instance)
            CameraShake.Instance.Shake(0.3f, 0.01f);

        isExplosing = true;
        SoundManager.Instance.PlaySound(SoundManager.Instance.barrelExplosion);
        explosionSkill.Activate(this, GetTile());
        associatedTile.SetPawnOnTile(null);
        SetTile(null);
        BarrelManager.Instance.Repool(this);

    }

    private bool isExplosing = false;

    public override void ReceiveDamage(int dmg)
    {

        Explode();
    }
}
