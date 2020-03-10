using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trow Element", menuName = "TONO/Skill/Throw Element")]
public class ThrowElement : Skill
{
    public GameObject projectilePrefab;

    public override void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
            SkillManager.instance.ThrowProjectile(user, target.GetPawnOnTile(), projectilePrefab, damage);
        }
    }

    public override void Preview(GamePawn user)
    {
        List<Tile> tilesToHighlight = new List<Tile>();
        Tile currentTile = user.GetTile().neighbours.up;
        if(currentTile != null)
        {
            if (currentTile.GetPawnOnTile() != null && currentTile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.right;
        if(currentTile != null)
        {
            if (currentTile.GetPawnOnTile() != null && currentTile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.down;
        if(currentTile != null)
        {
            if (currentTile.GetPawnOnTile() != null && currentTile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        currentTile = user.GetTile().neighbours.left;
        if(currentTile != null)
        {
            if (currentTile.GetPawnOnTile() != null && currentTile.GetPawnOnTile() != PlayerManager.instance.playerCharacter)
            {
                tilesToHighlight.Add(currentTile);
            }
        }

        user.SetPreviewID(Highlight_Manager.instance.ShowHighlight(tilesToHighlight, HighlightMode.ActionPreview, true));
    }
}
