using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "projectile" ,menuName = "TONO/Skill/projectile")]
public class ProjectileSkill : Skill
{
    public GameObject projectilePrefab;

    public override void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
            enemy.anim.SetTrigger("Throw");
            SkillManager.instance.ThrowProjectile(user, target.GetPawnOnTile(), projectilePrefab, damage);

        }
        else if (user is PlayerCharacter)
        {
            PlayerCharacter player = (PlayerCharacter)user;
            SkillManager.instance.LiftPawn(player, target.GetPawnOnTile());
            UI_Manager.instance.actionPanel.RefreshActions();
        }
        
    }
}
