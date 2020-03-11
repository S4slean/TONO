using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : GamePawn
{
    [Header("References")]
    public Transform LiftPawnSocket;
    //LOGIC
    public Dictionary<string, bool> activatedSkill = new Dictionary<string, bool>();

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

        base.Start();
        StartCoroutine(Init());

        foreach (Skill skill in skills)
        {
            activatedSkill.Add(skill.skillName, false);
        }
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
            oldMaterial = rend.material;
            rend.material = Highlight_Manager.instance.hoverMat;
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

    public override void SetDestination(Tile destination, bool showHighlight = false)
    {
        //print(destination);
        base.SetDestination(destination, showHighlight);

        HideMoveRange();
        InitializeAllSkillRange(destination);
    }

    public void SetPlayerTile(Tile newTile)
    {
        associatedTile = newTile;
    }

    public void ShowSkillPreview(Skill skill)
    {
        if (activatedSkill[skill.skillName])
        {
            Highlight_Manager.instance.HideHighlight(skillPreviewID);
            activatedSkill[skill.skillName] = false;
        }
        else
        {
            skill.Preview(this);
            activatedSkill[skill.skillName] = true;
        }
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
        print(currentPM);
        moveRange = Pathfinder_Dijkstra.instance.SearchForRange(destination, currentPM, false);
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

        lineUp = GridManager.instance.GetLineUntilObstacle(Direction.Up, destination, true);
        lineRight = GridManager.instance.GetLineUntilObstacle(Direction.Right, destination, true);
        lineDown = GridManager.instance.GetLineUntilObstacle(Direction.Down, destination, true);
        lineLeft = GridManager.instance.GetLineUntilObstacle(Direction.Left, destination, true);

        //DEBUG
        //print(destination.neighbours.right);

        gunRange.AddRange(lineUp);
        gunRange.AddRange(lineRight);
        gunRange.AddRange(lineDown);
        gunRange.AddRange(lineLeft);
    }

    public override void ReceiveDamage(int dmg)
    {
        currentLife = Mathf.Clamp(currentLife - dmg, 0, currentLife);
        if(currentLife <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log("Player Died");
        Destroy(gameObject);
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

}
