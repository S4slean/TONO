using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "TONO/Skill")]
public class Move : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {

    }

    public override void Preview(GamePawn user)
    {
       user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(Pathfinder_Dijkstra.instance.SearchForRange(user.GetTile(), 5, false), HighlightMode.Range));
    }
}
