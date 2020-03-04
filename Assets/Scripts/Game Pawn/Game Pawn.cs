using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePawn : MonoBehaviour
{
    protected Tile associatedTile;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, LayerMask.GetMask("FreeTile"));
        associatedTile = hit.transform.GetComponent<Tile>();
        associatedTile.SetPawnOnTile(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
