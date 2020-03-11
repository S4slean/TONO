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
    ThrowHover
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

    public PlayerCharacter playerCharacter;
    [HideInInspector]public Camera cam;
    public LayerMask mouseMask;
    //[HideInInspector]
    public HoverMode hoverMode;

    public GunModePointOnScreen pointsOnScreen;
    private List<Tile> currentLineHighlighted;
    private int highlightLineID = -1;

    public PlayerStatsConfig playerStats;



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
            playerCharacter.gunShotSkill.Preview(playerCharacter);
        }

        //THROW ELEMENT
        if (Input.GetKeyDown(throwElement))
        {
            playerCharacter.throwElementSkill.Preview(playerCharacter);
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
                //print("GUNSHOTHOVER");
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
                    if (currentHoveredTile != null && currentHoveredTile.isClickable)
                        SkillManager.instance.currentActiveSkill.Activate(playerCharacter, currentHoveredTile);
                }
                break;
            case HoverMode.ThrowHover:
                if (Input.GetMouseButtonDown(0))
                {
                    if (currentHoveredTile != null && currentHoveredTile.isClickable)
                        SkillManager.instance.ThrowElement(playerCharacter, playerCharacter.liftedPawn, currentHoveredTile);
                }
                break;
        }

    }

    public void AssignPlayerStatsToCharacter()
    {
        playerCharacter.currentLife = playerStats.playerStats.startingLP;
        playerCharacter.currentPA = playerStats.playerStats.startingAP;
        playerCharacter.currentPM = playerStats.playerStats.startingMP;
        playerCharacter.isGunLoaded = playerStats.playerStats.isGunLoadedAtStart;

        UI_Manager.instance.characterInfoPanel.CreateAndSetAllCharacterInfo();
    }

    public void StartPlayerTurn()
    {
        playerCharacter.currentPM = playerStats.playerStats.startingMP;
        playerCharacter.currentPA = playerStats.playerStats.startingAP;
        hoverMode = HoverMode.MovePath;
        playerCharacter.InitializeAllSkillRange(playerCharacter.GetTile());
    }

    public void EndPlayerTurn()
    {
        hoverMode = HoverMode.NoHover;
        GameManager.Instance.CheckIfCompleted(true);
    }
}
