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
        user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(GetRange(user), HighlightMode.ExplosionPreview));
    }

    public override List<Tile> GetRange(GamePawn user)
    {
        List<Tile> res = new List<Tile>();
        switch (rangeType)
        {
            case RangeType.Plus:
                res = GridManager.instance.GetPlusRange(user, range, true);
                break;
            case RangeType.Round:
                res = GridManager.instance.GetRoundRange(user, range, true);
                break;
            case RangeType.Cross:
                res = GridManager.instance.GetCrossRange(user, range, true);
                break;
            default:
                break;
        }
        return res;
    }
}
