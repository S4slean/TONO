using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kick", menuName = "TONO/Skill/Kick")]
public class Kick : Skill
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

        SkillManager.instance.Kick(user, damage, target.GetPawnOnTile(), dir);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
        }
        
    }

    public override void Preview(GamePawn user)
    {
        List<Tile> tilesToHighlight = HasAvailableTarget(user);

        if (tilesToHighlight.Count > 0)
            user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(tilesToHighlight, HighlightMode.ActionPreview, true));
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

}
