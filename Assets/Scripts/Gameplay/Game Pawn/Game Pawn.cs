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
    [SerializeField]protected bool hovered;
    public int skillPreviewID;
    protected bool _isMyTurn = false;
    protected bool _isDoingSomething = false;
    public List<Skill> skills = new List<Skill>();

    protected virtual void Start()
    {
        rend = GetComponent<Renderer>();

        DetectTile();
    }

    public void DetectTile()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position+Vector3.up, Vector3.down, out hit, mask);
        SetTile(hit.transform.GetComponent<Tile>());
        GetTile().SetPawnOnTile(this);
    }

    public virtual void OnEnable()
    {
        DetectTile();
    }

    public virtual void OnMouseEnter()
    {

    }
    public virtual void OnMouseExit()
    {

    }

    public Tile GetTile()
    {
        return associatedTile;
    }

    public void SetTile(Tile newTile)
    {
        associatedTile = newTile;
        if(newTile != null)
            associatedTile.SetPawnOnTile(this);
    }

    public void SetAssociatedTile(Tile newTile)
    {
        associatedTile = newTile;
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

    public virtual void SetDestination(Tile destination, bool showHighlight = false, bool movedByPlayer = false)
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

                if(this is PlayerCharacter)
                {
                    PlayerManager.instance.playerCharacter.currentPM -= path.Count;
                    print(PlayerManager.instance.playerCharacter.currentPM);

                    UI_Manager.instance.characterInfoPanel.ResetAllCharacterInfo();
                    //UI_Manager.instance.characterInfoPanel.SetCharacterInfoWithCost(UI_SelectedCharacterInfo.Stats.PM, path.Count);
                }
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

    public virtual void EndAction()
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

    public virtual void OnThrowed(PlayerCharacter user, Tile targetTile)
    {
        user.liftedPawn = null;
        SetTile(null);
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOMove(targetTile.transform.position + new Vector3(0f, 1.1f, 0f), 1f))
         .SetEase(Ease.OutCubic)
         .OnComplete(() => {
             PlayerManager.instance.hoverMode = HoverMode.MovePath;
             SetTile(targetTile);
             user.EndAction();
         });

    }

    public virtual void ReceiveDamage(int dmg)
    {
        
    }

    public virtual void Die()
    {
        associatedTile.SetPawnOnTile(null);
        SetTile(null);
    }

    
}
