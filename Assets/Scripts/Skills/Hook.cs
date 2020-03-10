using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : Skill
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

        SkillManager.instance.Hook(user, target.GetPawnOnTile(), dir);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
        }
        else if (user is PlayerCharacter)
        {
            PlayerCharacter player = (PlayerCharacter)user;
            
        }
        user.EndAction();
    }

    public override void Preview(GamePawn user)
    {

    }

    public override List<Tile> HasAvailableTarget(GamePawn user)
    {
        return null;
    }
}
