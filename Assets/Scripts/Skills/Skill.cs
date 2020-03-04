using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public enum RangeType {Line, Cross, Square, X }

    public RangeType rangeType;
    public int range;


    public virtual void Activate()
    {

    }
}
