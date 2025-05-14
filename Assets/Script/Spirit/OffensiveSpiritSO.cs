using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewOffensivSpirit", menuName = "Spirit/NewOffensiveSpirit")]
public class OffensiveSpiritSO : SpiritSO
{
    public OffensiveSpiritStatsDetail[] offensiveSpiritStatsDetails;
    public OffensiveSpirit offensiveSpirit;
    public ProjectileSO SpiritProjectileSO;
    public List<StatusEffectDetail> effects;

}
