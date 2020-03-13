using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "TONO/Skill/Buff")]
public class Buff : Skill
{
    public int movmentBuff;
    public int actionBuff;
    public int healthBuff;
    public int rageIncrease;
    public int buffDuration;

    public override void Activate(GamePawn user, Tile target)
    {
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            FXPlayer.Instance.PlayFX("BuffEnemy", enemy.transform.position+ Vector3.up);
            enemy.Buff();
        }
        user.EndAction();
    }
}
