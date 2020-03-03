using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public enum BarrelContent { empty, alcool, gunpowder};

    public bool standing = true;
    public BarrelContent content = BarrelContent.empty;

    public virtual void Break()
    {

    }

    public virtual void Drink()
    {

    }

    public virtual void Kick(Vector3 direction)
    {

    }

    public virtual void Throw(Vector3 direction, float distance)
    {

    }

    public virtual void Explode()
    {

    }
    


}
