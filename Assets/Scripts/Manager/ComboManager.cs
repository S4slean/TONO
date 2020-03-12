using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;

    private List<Barrel> barrelsInComboPreview = new List<Barrel>();
    private List<Barrel> barrelsInCombo = new List<Barrel>();

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool BarrelAlreadyInCombo(Barrel barrel)
    {
        return barrelsInComboPreview.Contains(barrel);
    }

    public List<Tile> AddBarrelToComboPreview(Barrel barrel, bool usingCombo)
    {
        //print("ADD BARREL TO COMBO");
        List<Tile> res = new List<Tile>();
        if (!BarrelAlreadyInCombo(barrel))
        {
            barrelsInComboPreview.Add(barrel);
            res.AddRange(barrel.explosionSkill.GetRange(barrel, usingCombo));
            //print(res.Count);
        }
        return res;
    }

    public void ClearAllComboList()
    {
        barrelsInComboPreview.Clear();
        barrelsInCombo.Clear();
    }
}
