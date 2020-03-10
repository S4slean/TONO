using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "TONO/Skill/MeleeAttack")]
public class MeleeAttack : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);


        user.ReceiveDamage(damage);
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

    public override List<Tile> HasAvailableTarget(GamePawn user)
    {
        return null;
    }
}
