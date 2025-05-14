using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveSpiritSlot : SpiritSlot
{
    private OffensiveSpiritSO offensiveSpiritSO;
    private OffensiveSpirit offensiveSpirit;

    public OffensiveSpiritSlot(Transform location,OffensiveSpiritSO offensiveSpiritSO) : base(location)
    {
        this.offensiveSpiritSO = offensiveSpiritSO;
        level = 1;
    }

    public override bool CanUpgrade(SpiritSO spiritSO)
    {
        if (level < 3 && offensiveSpiritSO==spiritSO) return true;
        return false;

    }

    public override void SpawnSpirit()
    {
        base.SpawnSpirit();
        offensiveSpirit=GameObject.Instantiate(offensiveSpiritSO.offensiveSpirit,location.position,Quaternion.identity);
        offensiveSpirit.SetOffensiveSpiritStats(offensiveSpiritSO.offensiveSpiritStatsDetails[0]);
        offensiveSpirit.SetStatusEffect(offensiveSpiritSO.effects);
        offensiveSpirit.SetProjectile(offensiveSpiritSO.SpiritProjectileSO);
        offensiveSpirit.transform.SetParent(location.transform,false);
        offensiveSpirit.transform.position = location.position;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        offensiveSpirit.SetOffensiveSpiritStats(offensiveSpiritSO.offensiveSpiritStatsDetails[level]);
    }

    
}
