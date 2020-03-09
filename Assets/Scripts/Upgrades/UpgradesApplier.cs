using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesApplier : MonoBehaviour
{
    public static UpgradesApplier Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool[] upgradesPossessed;


}
