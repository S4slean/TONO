using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump", menuName = "TONO/Skill/Jump")]
public class Jump : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        base.Activate(user, target);
    }

    public override void Preview(GamePawn user)
    {
        base.Preview(user);
    }

    public override List<Tile> HasAvailableTarget(GamePawn user)
    {
        return base.HasAvailableTarget(user);
    }
}
