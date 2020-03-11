using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Reload", menuName = "TONO/Skill/Reload")]
public class Reload : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        //base.Activate(user, target);
    }

    public override void Preview(GamePawn user)
    {
        SkillManager.instance.ReloadGun();
        //Activate();
    }
}
