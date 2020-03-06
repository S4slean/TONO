using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct GunModePointOnScreen
{
    public Vector3 screenPointUp,
            screenPointRight,
            screenPointDown,
            screenPointLeft,
            upRight,
            upLeft,
            downRight,
            downLeft;
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector]public PlayerCharacter playerCharacter;
    [HideInInspector]public bool playerCanMove = true;
    [HideInInspector]public Camera cam;
    public LayerMask mouseMask;

    public bool gunModeActivated;
    public GunModePointOnScreen pointsOnScreen;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        cam = Camera.main;

    }

    public void Start()
    {

    }

    public void Update()
    {
        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, mouseMask);

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(hit.transform.tag);

            if (hit.transform.tag == "FreeTile")
            {
                Tile clickedTile = hit.transform.GetComponent<Free>();
                if (clickedTile.isWalkable)
                {
                    playerCharacter.SetDestination(clickedTile, true);
                }
            }
        }

        //GUN SKILL
        if (Input.GetKeyDown(KeyCode.R))
        {
            GunShotSkill();
        }

        if (gunModeActivated)
        {
            Vector3 pivotScreenPoint = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().transform.position);
            //print("Pivot : " + pivotScreenPoint);

            Vector3 mouseOnScreen = Input.mousePosition;
            //print("Mouse : " + mouseOnScreen);
        }
    }

    public void GunShotSkill()
    {
        playerCharacter.ShowSkillPreview(playerCharacter.skills[(int)Skills.GunShot]);

        if (playerCharacter.activatedSkill[playerCharacter.skills[(int)Skills.GunShot].name])
        {
            pointsOnScreen.screenPointUp = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().neighbours.up.transform.position);
            pointsOnScreen.screenPointRight = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().neighbours.right.transform.position);
            pointsOnScreen.screenPointDown = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().neighbours.down.transform.position);
            pointsOnScreen.screenPointLeft = Camera.main.WorldToScreenPoint(playerCharacter.GetTile().neighbours.left.transform.position);

            pointsOnScreen.upRight = Vector3.Lerp(pointsOnScreen.screenPointUp, pointsOnScreen.screenPointRight, 0.5f);
            pointsOnScreen.upLeft = Vector3.Lerp(pointsOnScreen.screenPointUp, pointsOnScreen.screenPointLeft, 0.5f);
            pointsOnScreen.downRight = Vector3.Lerp(pointsOnScreen.screenPointDown, pointsOnScreen.screenPointRight, 0.5f);
            pointsOnScreen.downLeft = Vector3.Lerp(pointsOnScreen.screenPointDown, pointsOnScreen.screenPointLeft, 0.5f);

        }
        gunModeActivated = !gunModeActivated;
    }
}
