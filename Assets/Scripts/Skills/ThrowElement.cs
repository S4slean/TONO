using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trow Element", menuName = "TONO/Skill/Throw Element")]
public class ThrowElement : Skill
{
    public GameObject projectilePrefab;

    public override void Activate(GamePawn user, Tile target)
    {

        PlayerCharacter player = (PlayerCharacter)user;
        player.currentPA -= cost;
        SkillManager.instance.LiftPawn(player, target.GetPawnOnTile());

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

    public void ThrowPreview(GamePawn user, GamePawn liftedPawn)
    {
        GridManager.instance.AllTilesBecameNotClickable();
        List<Tile> tilesToHighlight = new List<Tile>();
        Tile playerTile = user.GetTile();

        if (liftedPawn is EnemieBehaviour)
        {
            Debug.Log("Lift Enemy");
            //UP
            if(IsAvailableTile(playerTile.neighbours.up))
                tilesToHighlight.Add(playerTile.neighbours.up);
            //RIGHT
            if(IsAvailableTile(playerTile.neighbours.right))
                tilesToHighlight.Add(playerTile.neighbours.right);
            //DOWN
            if(IsAvailableTile(playerTile.neighbours.down))
                tilesToHighlight.Add(playerTile.neighbours.down);
            //LEFT
            if(IsAvailableTile(playerTile.neighbours.left))
                tilesToHighlight.Add(playerTile.neighbours.left);
        }
        else if(liftedPawn is Barrel || liftedPawn is Box)
        {
            Debug.Log("Lift Barrel");
            RaycastHit[] hits = Physics.BoxCastAll(user.GetTile().transform.position + 2 * Vector3.up, (5 * Vector3.forward + 5 * Vector3.right), Vector3.down, Quaternion.Euler(Quaternion.identity.eulerAngles + new Vector3(0f, 45f, 0f)), 2f, LayerMask.GetMask("FreeTile"));
            foreach(RaycastHit hit in hits)
            {
                Tile tile = hit.transform.GetComponent<Tile>();
                if (IsAvailableTile(tile) || tile.GetPawnOnTile() == liftedPawn)
                {
                    tilesToHighlight.Add(tile);
                }
            }
        }
        Debug.Log("THROW PREVIEW : " + tilesToHighlight.Count);

        Highlight_Manager.instance.HideHighlight(user.GetSkillPreviewID());
        user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(tilesToHighlight, HighlightMode.ActionPreview, true));
        PlayerManager.instance.hoverMode = HoverMode.ThrowHover;
    }

    public bool IsAvailableTile(Tile tile)
    {
        if (tile != null && !(tile is Wall) && !(tile is Water) && tile.GetPawnOnTile() == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
