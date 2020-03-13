using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump", menuName = "TONO/Skill/Jump")]
public class Jump : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);
        Direction dir = GetDirection(user, target.GetPawnOnTile());

        SkillManager.instance.Jump(user, target.GetPawnOnTile(), dir);
        if (user is PlayerCharacter)
        {
            PlayerManager.instance.playerCharacter.currentPA -= cost;
            PlayerManager.instance.playerCharacter.anim.SetTrigger("Jump");
        }

        UI_Manager.instance.actionPanel.RefreshActions();
        UI_Manager.instance.actionPanel.selectedAction.isSelected = false;
        UI_Manager.instance.actionPanel.selectedAction.PlayCorrectAnimation(); ;

    }

    public override void Preview(GamePawn user)
    {
        if (HasAvailableTarget(user).Count > 0 && PlayerManager.instance.playerCharacter.currentPA >= cost)
        {
            GridManager.instance.AllTilesBecameNotClickable();
            PlayerManager playerManager = PlayerManager.instance;
            PlayerCharacter player = playerManager.playerCharacter;

            if (SkillManager.instance.currentActiveSkill != this)
            {
                base.Preview(user);
                player.HideMoveRange();
                List<Tile> tilesToHighlight = HasAvailableTarget(user);

                if (tilesToHighlight.Count > 0)
                    user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(tilesToHighlight, HighlightMode.ActionPreview, true));
                playerManager.hoverMode = HoverMode.MeleeHover;
            }
            else
            {
                SkillManager.instance.currentActiveSkill = null;
                playerManager.hoverMode = HoverMode.MovePath;
                Highlight_Manager.instance.HideHighlight(player.GetSkillPreviewID(), null, false);
                UI_Manager.instance.characterInfoPanel.ResetAllCharacterInfo();
                player.ShowMoveRange();
            }
        }
    }

    public override List<Tile> HasAvailableTarget(GamePawn user)
    {
        List<Tile> tilesToHighlight = new List<Tile>();
        Tile currentTile = user.GetTile().neighbours.up;
        if (currentTile != null)
        {
            if (IsAvailableTileToJump(currentTile) && IsAvailableTileToStand(currentTile.neighbours.up))
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.right;
        if (currentTile != null)
        {
            if (IsAvailableTileToJump(currentTile) && IsAvailableTileToStand(currentTile.neighbours.right))
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.down;
        if (currentTile != null)
        {
            if (IsAvailableTileToJump(currentTile) && IsAvailableTileToStand(currentTile.neighbours.down))
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.left;
        if (currentTile != null)
        {
            if (IsAvailableTileToJump(currentTile) && IsAvailableTileToStand(currentTile.neighbours.left))
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        return tilesToHighlight;
    }

    bool IsAvailableTileToJump(Tile tile)
    {
        if(tile.GetPawnOnTile() != null && tile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool IsAvailableTileToStand(Tile tile)
    {
        if(tile != null && !(tile is Water) && !(tile is Wall) && tile.GetPawnOnTile() == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
