using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : GamePawn
{
    [Header("References")]
    public Transform LiftPawnSocket;
    public Animator anim;
    //LOGIC
    [HideInInspector]
    public List<Tile> gunRange = new List<Tile>();
    [HideInInspector] 
    public List<Tile> lineUp = new List<Tile>();
    [HideInInspector] 
    public List<Tile> lineRight = new List<Tile>();
    [HideInInspector] 
    public List<Tile> lineDown = new List<Tile>();
    [HideInInspector] 
    public List<Tile> lineLeft = new List<Tile>();

    [HideInInspector] public GamePawn liftedPawn;

    //Skills
    [Header("Skills")]
    public Kick kickSkill;
    public ThrowElement throwElementSkill;
    public Jump jumpSkill;
    public GunShot gunShotSkill;
    public Reload reloadSkill;

    //Stats
    [Header("Stats")]
    public int currentPA;
    public int currentPM;
    public int currentLife;
    public bool isGunLoaded;


    private void Awake()
    {
    }

    protected override void Start()
    {
        PlayerManager.instance.playerCharacter = this;

        PlayerManager.instance.AssignPlayerStatsToCharacter();

        base.Start();
        StartCoroutine(Init());

        skills.AddRange(new List<Skill> { kickSkill, throwElementSkill, jumpSkill, gunShotSkill});
    }

    IEnumerator Init()
    {
        yield return new WaitForEndOfFrame();
        InitializeAllSkillRange(associatedTile);
    }

    public override void OnMouseEnter()
    {
        if(PlayerManager.instance.hoverMode == HoverMode.MovePath)
        {
            base.OnMouseEnter();
            //print("SHOW PREVIEW PLAYER : "+ PlayerManager.instance.hoverMode);
            hovered = true;
            //oldMaterial = rend.material;
            //rend.material = Highlight_Manager.instance.hoverMat;
            ShowMoveRange();
        }
    }
    public override void OnMouseExit()
    {
        if (hovered)
        {
            base.OnMouseExit();
            hovered = false;
            rend.material = oldMaterial;
            HideMoveRange();
        }
    }

    public override void SetDestination(Tile destination, bool showHighlight = false, bool movedByPlayer = false)
    {
        //print(destination);
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

            currentPM -= path.Count;
            //print(currentPM);

            UI_Manager.instance.characterInfoPanel.ResetAllCharacterInfo();
            //UI_Manager.instance.characterInfoPanel.SetCharacterInfoWithCost(UI_SelectedCharacterInfo.Stats.PM, path.Count);

            Sequence s = DOTween.Sequence();
            foreach (Tile tile in path)
            {
                s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {

                        SoundManager.Instance.PlaySound(SoundManager.Instance.step);
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

        HideMoveRange();
        InitializeAllSkillRange(destination);
    }

    public void SetPlayerTile(Tile newTile)
    {
        associatedTile = newTile;
    }

    public void ShowMoveRange()
    {
        SetPreviewID(Highlight_Manager.instance.ShowHighlight(moveRange, HighlightMode.MoveRangePreview, true));
    }

    public void HideMoveRange()
    {
        Highlight_Manager.instance.HideHighlight(GetSkillPreviewID(), null, false);
    }

    public void ActivateSkill(Skill skill, Tile target)
    {
        skill.Activate(this, target);
    }

    public void InitializeAllSkillRange(Tile destination)
    {
        //Move Range
        foreach (Tile tile in moveRange)
        {
            tile.isClickable = false;
        }
        //print(currentPM);
        moveRange = Pathfinder_Dijkstra.instance.SearchForRange(destination, currentPM, false);
        Debug.Log(moveRange.Count);
        foreach (Tile tile in moveRange)
        {
            tile.isClickable = true;
        }

        //Gun Range
        gunRange.Clear();
        lineUp.Clear();
        lineRight.Clear();
        lineDown.Clear();
        lineLeft.Clear();

        lineUp = GridManager.instance.GetLineUntilObstacle(Direction.Up, destination, true, true);
        lineRight = GridManager.instance.GetLineUntilObstacle(Direction.Right, destination, true, true);
        lineDown = GridManager.instance.GetLineUntilObstacle(Direction.Down, destination, true, true);
        lineLeft = GridManager.instance.GetLineUntilObstacle(Direction.Left, destination, true, true);

        //DEBUG
        //print(destination.neighbours.right);

        gunRange.AddRange(lineUp);
        gunRange.AddRange(lineRight);
        gunRange.AddRange(lineDown);
        gunRange.AddRange(lineLeft);

        UI_Manager.instance.actionPanel.RefreshActions();
    }

    public override void ReceiveDamage(int dmg)
    {
        currentLife = Mathf.Clamp(currentLife - dmg, 0, currentLife);
        
        UI_Manager.instance.characterInfoPanel.ResetAllCharacterInfo();
        if(currentLife <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    public override void Die()
    {
        UI_Manager.instance.messagePanel.ShowMessage(UI_MessagePanel.Messages.Defeat);
        anim.SetTrigger("Death");
    }


    public override void OnKicked(GamePawn user, int dmg, Direction dir)
    {
        ReceiveDamage(dmg);
        Tile newTile = GetTile().GetNeighbours(dir);

        Sequence s = DOTween.Sequence();

        //Play vertical Anim
        s.Append(transform.DOMove(newTile.transform.position + new Vector3(0, newTile.transform.localScale.y, 0), 0.3f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                GetTile().SetPawnOnTile(null);
                SetTile(newTile);

            }));


        s.OnComplete(() =>
        {
            user.EndAction();

        });
    }

    public override void EndAction()
    {
        base.EndAction();
        PlayerManager.instance.hoverMode = HoverMode.MovePath;
        InitializeAllSkillRange(GetTile());
    }

    

}
