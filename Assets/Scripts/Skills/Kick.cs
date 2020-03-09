using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);
        Direction dir = Direction.Up;
        if (user.transform.position.x - target.transform.position.x > .1f)
            dir = Direction.Left;
        else if (user.transform.position.x - target.transform.position.x < .1f)
            dir = Direction.Right;
        else if (user.transform.position.z - target.transform.position.z > .1f)
            dir = Direction.Down;
        else if (user.transform.position.z - target.transform.position.z < .1f)
            dir = Direction.Up;

        SkillManager.instance.Kick(user, damage, target.GetPawnOnTile(), dir);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
        }
        user.EndAction();
    }

    public override void Preview(GamePawn user)
    {

    }

    public override bool HasAvailableTarget(GamePawn user)
    {
        return true;
    }
}
