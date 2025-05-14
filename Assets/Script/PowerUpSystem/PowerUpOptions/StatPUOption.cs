using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatPUOption : PowerUpOption
{
    private StatsPowerUpSO statsPowerUpSO;
    private PlayerStats playerStats;
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        playerStats.PowerUP(statsPowerUpSO);

    }
    public void SetData(StatsPowerUpSO statsPowerUpSO, PlayerStats playerStats)
    {
        this.playerStats = playerStats;
        this.statsPowerUpSO = statsPowerUpSO;
        avatar.sprite = statsPowerUpSO.avatar;
        title.text = statsPowerUpSO.powerUpName;
    }
}
