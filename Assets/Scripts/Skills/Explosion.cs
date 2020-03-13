using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion", menuName = "TONO/Skill/Explosion")]
public class Explosion : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        base.Activate(user, target);
        List<Tile> explodedTiles = GetRange(user, false);

        foreach (Tile t in explodedTiles)
        {
            if (t == user.GetTile()) continue;

            Free f = (Free)t;
            f.PlayExplosion();

            if (t.hasAlcohol)
            {
                f.SetFire();
            }

            if (t.GetPawnOnTile() != null)
            {
                t.GetPawnOnTile().ReceiveDamage(1);
            }
        }
    }

    public override void Preview(GamePawn user)
    {
        user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(ComboManager.instance.AddBarrelToComboPreview((Barrel)user, true), HighlightMode.ExplosionPreview));
    }

    public override List<Tile> GetRange(GamePawn user, bool useCombo)
    {
        List<Tile> res = new List<Tile>();
        switch (rangeType)
        {
            case RangeType.Plus:
                res = GridManager.instance.GetPlusRange(user, range, useCombo);
                break;
            case RangeType.Round:
                res = GridManager.instance.GetRoundRange(user, range, useCombo);
                break;
            case RangeType.Cross:
                res = GridManager.instance.GetCrossRange(user, range, useCombo);
                break;
            default:
                break;
        }
        //Debug.Log("ExplosionRange : " + res.Count);
        return res;
    }
}
