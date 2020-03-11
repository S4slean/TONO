using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public struct GunModePointOnScreen
{
    public Vector3 up,
            right,
            down,
            left;
}

public enum HoverMode
{
    NoHover,
    MovePath,
    GunShotHover,
    MeleeHover,
    Bombardment,
    KickHover
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Inputs")]
    public KeyCode throwElement = KeyCode.Alpha3;
    public KeyCode gunShot = KeyCode.Alpha4;
    public KeyCode kick = KeyCode.Alpha2;

    [Header("Preview")]
    public bool showMoveRangeWithPathHighlight;
    //[HideInInspector]
    public Tile currentHoveredTile;

    [HideInInspector]public PlayerCharacter playerCharacter;
    [HideInInspector]public Camera cam;
    public LayerMask mouseMask;
    [HideInInspector]public HoverMode hoverMode;

    public GunModePointOnScreen pointsOnScreen;
    private List<Tile> currentLineHighlighted;
    private int highlightLineID = -1;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        cam = Camera.main;

        hoverMode = HoverMode.NoHover;
    }

    public void Start()
    {

    }

    public void Update()
    {
        //GUN SKILL
        if (Input.GetKeyDown(gunShot))
        {
            GunShotSkill();
        }

        //THROW ELEMENT
        if (Input.GetKeyDown(throwElement))
        {
            ThrowElementSkill();
        }

        /*RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, mouseMask);*/

        switch (hoverMode)
        {
            case HoverMode.MovePath:
                if (Input.GetMouseButtonDown(0))
                {
                    //Debug.Log(hit.transform.tag);

                    if (currentHoveredTile != null && currentHoveredTile.isWalkable && playerCharacter.moveRange.Contains(currentHoveredTile))
                    {
                        playerCharacter.BeginAction();
                        playerCharacter.SetDestination(currentHoveredTile);
                    }
                }
                break;

            case HoverMode.GunShotHover:
                Vector3 pivotScreenPoint = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().transform.position);
                //print("Pivot : " + pivotScreenPoint);

                Vector3 mouseOnScreen = Input.mousePosition;
                //print("Mouse : " + mouseOnScreen);
                Vector3 mouseDir = mouseOnScreen - pivotScreenPoint;

                float angleUp = Vector3.Angle(pointsOnScreen.up - pivotScreenPoint, mouseDir);
                float angleRight = Vector3.Angle(pointsOnScreen.right - pivotScreenPoint, mouseDir);
                float angleDown = Vector3.Angle(pointsOnScreen.down - pivotScreenPoint, mouseDir);
                float angleLeft = Vector3.Angle(pointsOnScreen.left - pivotScreenPoint, mouseDir);

                //print("Up : " + angleUp + ", Right : " + angleRight + ", Down : " + angleDown + ", Left : " + angleLeft);

                Dictionary<List<Tile>, float> lines = new Dictionary<List<Tile>, float>();
                lines.Add(playerCharacter.lineUp, angleUp);
                lines.Add(playerCharacter.lineRight, angleRight);
                lines.Add(playerCharacter.lineDown, angleDown);
                lines.Add(playerCharacter.lineLeft, angleLeft);

                List<Tile> lineToHighlight = lines.First().Key;
                foreach(KeyValuePair<List<Tile>, float> line in lines)
                {
                    if(line.Value < lines[lineToHighlight])
                    {
                        lineToHighlight = line.Key;
                    }
                }

                if(lineToHighlight != currentLineHighlighted)
                {
                    //print("CHANGE");
                    if (highlightLineID > -1)
                    {
                        //print(highlightLineID);
                        Highlight_Manager.instance.HideHighlight(highlightLineID);
                    }
                    currentLineHighlighted = lineToHighlight;
                    Highlight_Manager.instance.HideHighlight(playerCharacter.GetSkillPreviewID());
                    playerCharacter.SetPreviewID(Highlight_Manager.instance.ShowHighlight(playerCharacter.gunRange, HighlightMode.ActionPreview));
                    highlightLineID = Highlight_Manager.instance.ShowHighlight(lineToHighlight, HighlightMode.ActionHighlight);
                }
                //print(playerCharacter.lineUp.Count);
                break;
            case HoverMode.Bombardment:
                if (Input.GetMouseButtonDown(0))
                {
                    if(currentHoveredTile != null && !(currentHoveredTile is Wall) && !currentHoveredTile.hasBarrelMarker)
                        BombardmentManager.Instance.PlaceBarrelMarker(currentHoveredTile);
                }
                break;
            case HoverMode.MeleeHover:
                if (Input.GetMouseButtonDown(0))
                {
                    if (currentHoveredTile.isClickable)
                        SkillManager.instance.currentActiveSkill.Activate(playerCharacter, currentHoveredTile);
                }
                break;
        }

    }

    public void StartPlayerTurn()
    {
        hoverMode = HoverMode.MovePath;
        playerCharacter.InitializeAllSkillRange(playerCharacter.GetTile());
    }

    public void EndPlayerTurn()
    {
        hoverMode = HoverMode.NoHover;
        GameManager.Instance.CheckIfCompleted(true);
    }

    public void GunShotSkill()
    {
        GridManager.instance.AllTilesBecameNotClickable();
        if (hoverMode != HoverMode.GunShotHover)
        {
            hoverMode = HoverMode.GunShotHover;
            playerCharacter.HideMoveRange();
        }
        else
            hoverMode = HoverMode.MovePath;

        //DEBUG
        /*foreach(string name in playerCharacter.pawnSkills.Keys)
        {
            print("Skill name : " + name);
        }*/

        Skill gunShot = null;
        foreach(Skill skill in playerCharacter.skills)
        {
            if(skill.skillName == Skills.GunShot.ToString())
            {
                gunShot = skill;
                break;
            }
        }
        if(gunShot != null)
            playerCharacter.ShowSkillPreview(gunShot);

        if (hoverMode == HoverMode.GunShotHover)
        {
            pointsOnScreen.up = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().transform.position + Vector3.forward*2);
            pointsOnScreen.right = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().transform.position + Vector3.right*2);
            pointsOnScreen.down = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().transform.position + Vector3.back*2);
            pointsOnScreen.left = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().transform.position + Vector3.left*2);

        }
    }

    public void ThrowElementSkill()
    {
        GridManager.instance.AllTilesBecameNotClickable();
        if (SkillManager.instance.currentActiveSkill is ThrowElement)
        {
            hoverMode = HoverMode.MovePath;
            SkillManager.instance.currentActiveSkill = null;
        }
        else
        {
            hoverMode = HoverMode.MeleeHover;
            playerCharacter.HideMoveRange();
        }

        Skill throwElement = null;
        foreach (Skill skill in playerCharacter.skills)
        {
            if (skill.skillName == Skills.ThrowElement.ToString())
            {
                throwElement = skill;
                break;
            }
        }
        if (throwElement != null)
            playerCharacter.ShowSkillPreview(throwElement);

    }
}
