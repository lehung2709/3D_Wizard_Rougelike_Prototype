using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpiritPUOption : PowerUpOption
{
    private SpiritSO spiritSO;
    private int slotIndex;
    private SpiritsManager spiritsManager;

    

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        spiritsManager.TakeSpirit(spiritSO,slotIndex);
    }

    public void SetData(SpiritSO spiritSO, SpiritsManager spiritsManager, int slotIndex,int Level)
    {
        this.spiritSO = spiritSO;
        this.spiritsManager = spiritsManager;
        avatar.sprite = spiritSO.avatar;
        title.text = spiritSO.spiritName + "\n Level"+Level;
        this.slotIndex = slotIndex;
        

    }
}
