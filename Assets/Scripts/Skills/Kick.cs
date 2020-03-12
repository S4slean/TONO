using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kick", menuName = "TONO/Skill/Kick")]
public class Kick : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);
        Direction dir = GetDirection(user, target.GetPawnOnTile());

        SkillManager.instance.Kick(user, damage, target.GetPawnOnTile(), dir);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
            enemy.anim.SetTrigger("Kick");
        }
        else if (user is PlayerCharacter)
        {
            PlayerManager.instance.playerCharacter.currentPA -= cost;
        }
        
    }

    public override void Preview(GamePawn user)
    {
        if(HasAvailableTarget(user).Count > 0 && PlayerManager.instance.playerCharacter.currentPA >= cost)
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
            if (currentTile.GetPawnOnTile() != null && currentTile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.right;
        if (currentTile != null)
        {
            if (currentTile.GetPawnOnTile() != null && currentTile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.down;
        if (currentTile != null)
        {
            if (currentTile.GetPawnOnTile() != null && currentTile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.left;
        if (currentTile != null)
        {
            if (currentTile.GetPawnOnTile() != null && currentTile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        return tilesToHighlight;
    }

    public void PreviewPawnPath(GamePawn user ,GamePawn selectedPawn)
    {
        Direction dir = GetDirection(user, selectedPawn);

        List<Tile> tilesToHighlight = new List<Tile>();
        if(selectedPawn is Barrel)
        {
            tilesToHighlight = GridManager.instance.GetLineUntilObstacle(dir, selectedPawn.GetTile());
        }else if(selectedPawn is EnemieBehaviour || selectedPawn is Box)
        {
            Tile tileToCheck;
            switch (dir)
            {
                case Direction.Up:
                    tileToCheck = selectedPawn.GetTile().neighbours.up;
                    if (tileToCheck != null && !(tileToCheck is Wall) && !(tileToCheck is Water))
                    {
                        tilesToHighlight.Add(tileToCheck);
                    }
                    break;
                case Direction.Right:
                    tileToCheck = selectedPawn.GetTile().neighbours.right;
                    if (tileToCheck != null && !(tileToCheck is Wall) && !(tileToCheck is Water))
                    {
                        tilesToHighlight.Add(tileToCheck);
                    }
                    break;
                case Direction.Down:
                    tileToCheck = selectedPawn.GetTile().neighbours.down;
                    if (tileToCheck != null && !(tileToCheck is Wall) && !(tileToCheck is Water))
                    {
                        tilesToHighlight.Add(tileToCheck);
                    }
                    break;
                case Direction.Left:
                    tileToCheck = selectedPawn.GetTile().neighbours.left;
                    if (tileToCheck != null && !(tileToCheck is Wall) && !(tileToCheck is Water))
                    {
                        tilesToHighlight.Add(tileToCheck);
                    }
                    break;
            }
        }
        PlayerManager.instance.SetHighlightID(Highlight_Manager.instance.ShowHighlight(tilesToHighlight, HighlightMode.ActionPreview));
        tilesToHighlight[tilesToHighlight.Count - 1].rend.material = Highlight_Manager.instance.actionHighlightMat; ;
    }

    public bool IsAvailableTile(Tile currentTile, GamePawn selectedPawn)
    {
        if (currentTile != null && !(currentTile is Wall) && !(currentTile is Water))
        {
            if (currentTile.GetPawnOnTile() == selectedPawn)
            {
                return true;
            }
        }
        return false;
    }

    public Direction GetDirection(GamePawn user, GamePawn selectedPawn)
    {
        //UP
        Tile currentTile = user.GetTile().neighbours.up;
        if (IsAvailableTile(currentTile, selectedPawn))
        {
            return Direction.Up;
        }

        //RIGHT
        currentTile = user.GetTile().neighbours.right;
        if (IsAvailableTile(currentTile, selectedPawn))
        {
            return Direction.Right;
        }

        //DOWN
        currentTile = user.GetTile().neighbours.down;
        if (IsAvailableTile(currentTile, selectedPawn))
        {
            return Direction.Down;
        }

        //LEFT
        currentTile = user.GetTile().neighbours.left;
        if (IsAvailableTile(currentTile, selectedPawn))
        {
            return Direction.Left;
        }
        return Direction.Up;
    }

}
