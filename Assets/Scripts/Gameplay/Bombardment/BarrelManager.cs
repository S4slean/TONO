using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelManager : MonoBehaviour
{
    public BarrelConfig config;

    public static BarrelManager Instance;

    public GameObject barrel;

    Queue<Barrel> barrelPool;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        barrelPool = new Queue<Barrel>();
        FillBarrelPool();
    }

    void FillBarrelPool()
    {
        for (int i = 0; i < 10; i++)
        {
            Barrel newBarrel = Instantiate(barrel, transform).GetComponent<Barrel>();
            Repool(newBarrel);
        }
    }
    
    public GameObject GetBarrel(RangeType rangeType)
    {
        if(barrelPool.Count < 1)
        {
            FillBarrelPool();
        }
        Barrel newBarrel = barrelPool.Dequeue();
        BarrelType type = config.barrelTypes[0];
        for(int i = 0; i < config.barrelTypes.Length; i++)
        {
            if(config.barrelTypes[i].rangeType == rangeType)
            {
                type = config.barrelTypes[i];
                break;
            }
        }
        newBarrel.Initialize(type);
        return newBarrel.gameObject;    
    }

    public void Repool(Barrel barrel)
    {
        barrelPool.Enqueue(barrel);
        barrel.gameObject.SetActive(false);
    }
}
