using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Skill
{
    public GameObject projectilePrefab;

    public override void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
            SkillManager.instance.ThrowProjectile(user, target.GetPawnOnTile(), projectilePrefab, damage);
        }
        else if (user is PlayerCharacter)
        {
            PlayerCharacter player = (PlayerCharacter)user;
            SkillManager.instance.LiftPawn(player, target.GetPawnOnTile());
        }
    }
}
