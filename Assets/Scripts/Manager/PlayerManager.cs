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
    ThrowElementHover,
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Inputs")]
    public KeyCode throwElement = KeyCode.Alpha3;
    public KeyCode gunShot = KeyCode.Alpha4;

    [Header("Preview")]
    public bool showMoveRangeWithPathHighlight;

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

        hoverMode = HoverMode.MovePath;
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

        if (Input.GetKeyDown(throwElement))
        {
            ThrowElementSkill();
        }

        switch (hoverMode)
        {
            case HoverMode.MovePath:
                RaycastHit hit;
                Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, mouseMask);

                if (Input.GetMouseButtonDown(0))
                {
                    //Debug.Log(hit.transform.tag);

                    if (hit.transform != null && hit.transform.tag == "FreeTile")
                    {
                        Tile clickedTile = hit.transform.GetComponent<Free>();
                        if (clickedTile.isWalkable && playerCharacter.moveRange.Contains(clickedTile))
                        {
                            playerCharacter.BeginAction();
                            playerCharacter.SetDestination(clickedTile);
                        }
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
        }

    }

    public void StartPlayerTurn()
    {

    }

    public void EndPlayerTurn()
    {
        GameManager.Instance.CheckIfCompleted(true);
    }

    public void GunShotSkill()
    {
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
        if (hoverMode != HoverMode.ThrowElementHover)
        {
            hoverMode = HoverMode.ThrowElementHover;
            playerCharacter.HideMoveRange();
        }
        else
            hoverMode = HoverMode.MovePath;

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
