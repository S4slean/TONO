using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "TONO/Skill/DefaultSkill")]
public class Skill : ScriptableObject
{


    public string skillName;
    public string description;
    public int cost;
    public int damage;
    public RangeType rangeType;
    public int range;

    //Skill sprites
    public Sprite enabledSprite;
    public Sprite unenabledSprite;

    public virtual void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name);
        if (user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
        }

        //UI_Manager.instance.characterInfoPanel.SetCharacterInfoWithCost(UI_SelectedCharacterInfo.Stats.PA, cost);

        user.EndAction();
    }

    public virtual void Preview(GamePawn user)
    {
        SkillManager.instance.currentActiveSkill = this;

        if (PlayerManager.instance.playerCharacter.currentPA >= cost)
        {
            UI_Manager.instance.characterInfoPanel.ResetAllCharacterInfo();
            UI_Manager.instance.characterInfoPanel.PreviewCharacterInfo(UI_SelectedCharacterInfo.Stats.PA, cost);
        }
    }

    public virtual List<Tile> HasAvailableTarget(GamePawn user)
    {
        return null;
    }

    public virtual List<Tile> GetRange(GamePawn user) { return new List<Tile>(); }

    public virtual bool IsAvailableTile(Tile currentTile, GamePawn selectedPawn)
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

    public virtual Direction GetDirection(GamePawn user, GamePawn selectedPawn)
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


public enum RangeType { Default, Line, Plus, Round, Cross }
