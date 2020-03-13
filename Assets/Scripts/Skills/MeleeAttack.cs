using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "TONO/Skill/MeleeAttack")]
public class MeleeAttack : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        target.GetPawnOnTile().ReceiveDamage(damage);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
            enemy.anim.SetTrigger("Melee");
            SoundManager.Instance.PlaySound(SoundManager.Instance.wish);
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
