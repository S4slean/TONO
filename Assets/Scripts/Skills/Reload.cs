using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Reload", menuName = "TONO/Skill/Reload")]
public class Reload : Skill
{
    public override void Activate(GamePawn user, Tile target)
    {
        //base.Activate(user, target);
    }

    public override void Preview(GamePawn user)
    {
        if(PlayerManager.instance.playerCharacter.currentPA >= cost)
        {
            UI_Manager.instance.characterInfoPanel.ResetAllCharacterInfo();
            UI_Manager.instance.characterInfoPanel.PreviewCharacterInfo(UI_SelectedCharacterInfo.Stats.PA, cost);
        }

        SkillManager.instance.ReloadGun();

        UI_Manager.instance.gunPanel.RefreshUI();
        //Activate();
    }
}
