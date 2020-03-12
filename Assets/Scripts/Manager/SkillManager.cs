using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Skills
{
    Move,
    Jump,
    Kick,
    ThrowElement,
    GunShot,
    Reload,
    Explosion,
    Hook,

    NumberOfSkills
}

public class SkillManager : MonoBehaviour
{

    public static SkillManager instance;
    public Skill currentActiveSkill;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Kick(GamePawn user, int dmg, GamePawn target, Direction dir)
    {
        //user.PlayAnim

        currentActiveSkill = null;
        PlayerManager.instance.hoverMode = HoverMode.NoHover;

        Highlight_Manager.instance.HideHighlight(user.GetSkillPreviewID(), null, false);

        target.OnKicked(user, dmg, dir);
    }

    public AnimationCurve jumpCurve;
    public float jumpHeight = 5;

    public void Jump(GamePawn user, GamePawn target, Direction dir)
    {
        Tile jumpTile = target.GetTile().GetNeighbours(dir);

        Sequence s = DOTween.Sequence();

        //Play vertical Anim
        s.Append(transform.DOMove(jumpTile.transform.position + new Vector3(0, jumpTile.transform.localScale.y, 0), 0.3f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                user.GetTile().SetPawnOnTile(null);
                user.SetTile( jumpTile);

            }));


        s.OnComplete(() =>
        {
            user.EndAction();

        });
    }

    public void ThrowProjectile(GamePawn user, GamePawn target, GameObject projectile, int dmg)
    {
       
        GameObject instance = Instantiate(projectile, user.transform.position + Vector3.up, Quaternion.identity);
        instance.GetComponent<Projectiles>().Throw(target, user, dmg);
       
    }

    public void LiftPawn(PlayerCharacter user, GamePawn target)
    {
        target.GetTile().SetPawnOnTile(null);
        target.SetTile(null);
        user.liftedPawn = target;

        user.BeginAction();

        Sequence s = DOTween.Sequence();
        s.Append(target.transform.DOMove(user.LiftPawnSocket.position , 0.3f))
         .SetEase(Ease.OutCubic);
        user.throwElementSkill.ThrowPreview(user, target);
    }

    public void ReloadGun()
    {
       //anim + son
        PlayerManager.instance.playerCharacter.isGunLoaded = true;
        PlayerManager.instance.playerCharacter.EndAction();
    }

    public GameObject anchor;

    public void Hook(GamePawn user, GamePawn target, Direction dir)
    {

        Tile hookTile = target.GetTile().GetNeighbours(dir);
        Sequence s = DOTween.Sequence();

        GameObject.Instantiate(anchor, user.transform.position + Vector3.up, Quaternion.identity);

        s.Append(anchor.transform.DOMove(target.transform.position + Vector3.up, .3f))
            .SetEase(Ease.Linear);

        //Play vertical Anim
        s.Append(target.transform.DOMove(hookTile.transform.position + new Vector3(0, hookTile.transform.localScale.y, 0), 0.3f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                target.GetTile().SetPawnOnTile(null);
                target.SetTile(hookTile);

            }));


        s.OnComplete(() =>
        {
            user.EndAction();

        });
    }

    public void GunShot()
    {

    }

    public void ThrowElement(PlayerCharacter user, GamePawn pawnToThrow, Tile target)
    {
        pawnToThrow.OnThrowed(user, target);
        currentActiveSkill = null;
        PlayerManager.instance.hoverMode = HoverMode.NoHover;

        Highlight_Manager.instance.HideHighlight(user.GetSkillPreviewID(), null, false);
        user.ShowMoveRange();

    }

    public void CreateAlcoholPool(Tile affectedTile, bool canSpread)
    {

    }

}
