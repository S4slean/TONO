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

    public List<Tile> AddBarrelToComboPreview(Barrel barrel)
    {
        List<Tile> res = new List<Tile>();
        if (!barrelsInCombo.Contains(barrel))
        {
            res.AddRange(barrel.explosionSkill.GetRange(barrel));
            barrelsInCombo.Add(barrel);
        }
        return res;
    }

    public void ClearAllComboList()
    {
        barrelsInComboPreview.Clear();
        barrelsInCombo.Clear();
    }
}
