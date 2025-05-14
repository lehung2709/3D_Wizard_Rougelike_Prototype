using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellPUOption : PowerUpOption
{
    private SpellSO spellSO;
    private SpellsManager spellsManager;
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        spellsManager.TakeSpell(spellSO);

    }
    public void SetData(SpellSO spellSO,SpellsManager spellsManager)
    {
        this.spellSO = spellSO;
        this.spellsManager = spellsManager;
        avatar.sprite = spellSO.avatar;
        title.text = spellSO.spellName;
    }    
}
