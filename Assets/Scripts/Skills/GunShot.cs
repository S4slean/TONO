using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun Shot", menuName = "TONO/Skill/Gun Shot")]
public class GunShot : Skill
{
    public GameObject bullet;
    public override void Activate(GamePawn user, Tile target)
    {
        base.Activate(user, target);
        SkillManager.instance.GunShot(user, target, bullet);
        Time.timeScale = 1f;

        UI_Manager.instance.gunPanel.RefreshUI();
        UI_Manager.instance.actionPanel.RefreshActions();
    }

    public override void Preview(GamePawn user)
    {
        if(user is PlayerCharacter )
        {
            PlayerManager playerManager = PlayerManager.instance;
            PlayerCharacter player = playerManager.playerCharacter;

            if (player.isGunLoaded)
            {
                if (SkillManager.instance.currentActiveSkill != this)
                {
                    base.Preview(user);
                    Time.timeScale = 0.3f;
                    player.HideMoveRange();
                    player.SetPreviewID(Highlight_Manager.instance.ShowHighlight(player.gunRange, HighlightMode.ActionPreview, true));
                    playerManager.hoverMode = HoverMode.GunShotHover;

                    playerManager.pointsOnScreen.up = Camera.main.WorldToScreenPoint(player.GetTile().transform.position + Vector3.forward * 2);
                    playerManager.pointsOnScreen.right = Camera.main.WorldToScreenPoint(player.GetTile().transform.position + Vector3.right * 2);
                    playerManager.pointsOnScreen.down = Camera.main.WorldToScreenPoint(player.GetTile().transform.position + Vector3.back * 2);
                    playerManager.pointsOnScreen.left = Camera.main.WorldToScreenPoint(player.GetTile().transform.position + Vector3.left * 2);
                }
                else
                {
                    Time.timeScale = 1f;
                    Highlight_Manager.instance.HideHighlight(player.GetSkillPreviewID(), null, false);
                    SkillManager.instance.currentActiveSkill = null;
                    playerManager.hoverMode = HoverMode.MovePath;
                }
            }
            else
            {
                SkillManager.instance.ReloadGun();
                UI_Manager.instance.gunPanel.RefreshUI();
            }
        }
    }
}
