using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "TONO/Skill/DefaultSkill")]
public class Skill : ScriptableObject
{
    public enum RangeType {Default ,Line, Cross, Square, X }

    public RangeType rangeType;
    public int range;
    public int cost;
    public string skillName;
    public string description;


    public virtual void Activate(GamePawn user, Tile target)
    {
        Debug.Log(user.gameObject.name + " used " + skillName + " on " + target.GetPawnOnTile().transform.name );
        if(user is EnemieBehaviour)
        {
            EnemieBehaviour enemy = (EnemieBehaviour)user;
            enemy.actionPoints -= cost;
        }
        user.EndAction();
    }

    public virtual void Preview(GamePawn user)
    {

    }
}
