﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector]public PlayerCharacter playerCharacter;
    [HideInInspector]public bool playerIsSelected;
    [HideInInspector]public Camera cam;
    [HideInInspector]public LayerMask mouseMask;

    public Material hoveringMaterial;
    public Material highlightMaterial;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        if (playerCharacter != null)
            mouseMask = LayerMask.GetMask("Tile");
        else
            mouseMask = LayerMask.GetMask("Player");

        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, mouseMask);

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(hit.transform.tag);

            if (hit.transform.tag == "Player")
            {
                playerCharacter = hit.transform.GetComponentInChildren<PlayerCharacter>();
            }
            else if (hit.transform.tag == "FreeTile" && playerIsSelected)
            {
                Tile clickedTile = hit.transform.GetComponent<Free>();
                if (clickedTile.isWalkable)
                {
                    playerCharacter.SetDestination(clickedTile, true);
                    playerCharacter = null;
                }
            }
        }
    }
}
