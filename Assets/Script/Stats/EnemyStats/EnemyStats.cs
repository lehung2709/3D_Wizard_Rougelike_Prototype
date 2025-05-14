using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    [SerializeField] EnemyStatsSO enemyStatsSO;

    public List<StatusEffectDetail> statusEffectDetails;

    protected override void Awake()
    {
        base.Awake();
    }
    public void SetInitialEnemyStats()
    {
        SetBaseStats(enemyStatsSO);
        statusEffectDetails=enemyStatsSO.statusEffectDetails;
    }
    public void SetEnemyStatsSO(EnemyStatsSO enemyStatsSO)
    {
        this.enemyStatsSO = enemyStatsSO;
        SetInitialEnemyStats();
    }    
}
