using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion", menuName = "TONO/Skill/Explosion")]
public class Explosion : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        base.Activate(user, target);
    }

    public override void Preview(GamePawn user)
    {
        switch (rangeType)
        {
            case RangeType.Plus:
                user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(GridManager.instance.GetPlusRange(user.GetTile(), range), HighlightMode.ExplosionPreview));
                break;
            default:
                break;
        }
    }
}
