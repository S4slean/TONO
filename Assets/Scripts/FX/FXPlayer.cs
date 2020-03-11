using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPlayer : MonoBehaviour
{
    public static FXPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public FXConfig config;

    public FXPool[] fxPools;

    public void Initialize()
    {
        fxPools = new FXPool[config.fxs.Length];
        for(int i = 0; i < config.fxs.Length; i++)
        {
            GameObject newPoolGO = new GameObject("FXPool_" + config.fxs[i].fxName);
            newPoolGO.transform.parent = transform;
            FXPool newPool = newPoolGO.AddComponent<FXPool>();
            newPool.Initialize(config.fxs[i]);
            fxPools[i] = newPool;
        }
    }

    public void PlayFX(string fxName, Vector3 position)
    {
        bool found = false;
        for(int i = 0; i < fxPools.Length; i++)
        {
            if(fxPools[i].data.fxName == fxName)
            {
                found = true;
                PlayFXFromPool(fxPools[i], position);
                break;
            }
        }

        if(!found)
        {
            Debug.LogError("ERROR : No FX named " + fxName + " !");
        }
    }

    void PlayFXFromPool(FXPool pool, Vector3 position)
    {
        FX playedFX = pool.Depool();
        playedFX.transform.position = position;
        playedFX.Play();
    }
}
