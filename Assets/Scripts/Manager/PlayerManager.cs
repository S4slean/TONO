using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector]public PlayerCharacter playerCharacter;
    [HideInInspector]public bool playerCanMove = true;
    [HideInInspector]public Camera cam;
    public LayerMask mouseMask;

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
    }
}
