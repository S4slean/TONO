using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public enum RangeType {Default ,Line, Cross, Square, X }

    public RangeType rangeType;
    public int range;
    public string skillName;
    public string description;


    public virtual void Activate(GamePawn user, Tile target)
    {

    }

    public virtual void Preview(GamePawn user)
    {

    }
}
