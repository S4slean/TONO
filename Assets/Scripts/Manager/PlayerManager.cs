using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector]public PlayerCharacter selectedCharacter = null;
    [HideInInspector]public Camera cam;
    [HideInInspector]public LayerMask mouseMask;

    public Material hoveringMaterial;

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
        if (selectedCharacter != null)
            mouseMask = LayerMask.GetMask("Tile");
        else
            mouseMask = LayerMask.GetMask("Player");

        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, mouseMask);

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(hit.transform.tag);

            if(hit.transform.tag == "Player")
            {
                selectedCharacter = hit.transform.GetComponentInChildren<PlayerCharacter>();
            }
            else if(hit.transform.tag == "FreeTile" && selectedCharacter != null)
            {
                selectedCharacter.SetDestination(hit.transform.GetComponent<Free>());
            }
        }
    }
}
