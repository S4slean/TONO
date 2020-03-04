using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePawn : MonoBehaviour
{
    [SerializeField]protected Tile associatedTile;
    public LayerMask mask;

    protected virtual void Start()
    {        
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, mask);
        //print("Pawn tile : " + hit.transform.name);
        associatedTile = hit.transform.GetComponent<Tile>();
        associatedTile.SetPawnOnTile(this);
    }

    void Update()
    {
        
    }
}
