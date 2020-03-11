using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun Shot", menuName = "TONO/Skill/Gun Shot")]
public class GunShot : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        base.Activate(user, target);
    }

    public override void Preview(GamePawn user)
    {
        if(user is PlayerCharacter)
        {
            base.Preview(user);
            PlayerCharacter player = user as PlayerCharacter;
            user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(player.gunRange, HighlightMode.ActionPreview, true));
        }
    }
}
