using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct GunModePointOnScreen
{
    public Vector3 screenPointUp,
            screenPointRight,
            screenPointDown,
            screenPointLeft;
}

public enum HoverMode
{
    NoHover,
    MovePath,
    GunShotHover
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector]public PlayerCharacter playerCharacter;
    [HideInInspector]public bool playerCanMove = true;
    [HideInInspector]public Camera cam;
    public LayerMask mouseMask;
    public HoverMode hoverMode;

    public GunModePointOnScreen pointsOnScreen;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            GunShotSkill();
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
                        if (clickedTile.isWalkable)
                        {
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
                break;
        }

    }

    public void GunShotSkill()
    {
        if (hoverMode != HoverMode.GunShotHover)
            hoverMode = HoverMode.GunShotHover;
        else
            hoverMode = HoverMode.MovePath;

        playerCharacter.ShowSkillPreview(playerCharacter.skills[(int)Skills.GunShot]);

        if (hoverMode == HoverMode.GunShotHover)
        {
            pointsOnScreen.screenPointUp = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().neighbours.up.transform.position);
            pointsOnScreen.screenPointRight = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().neighbours.right.transform.position);
            pointsOnScreen.screenPointDown = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().neighbours.down.transform.position);
            pointsOnScreen.screenPointLeft = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().neighbours.left.transform.position);

        }
    }
}
