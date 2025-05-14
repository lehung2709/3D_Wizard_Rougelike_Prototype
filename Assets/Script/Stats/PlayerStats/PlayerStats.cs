using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public int ProjectilesPerCast;
    public float Maxmana;
    public float Mana;
    public float ManaRegen;
    [SerializeField]PlayerStatsSO playerStatsSO;
    [SerializeField] private SliderAndFillBar manaBar;
    private float timer = 0;


    protected override void Awake()
    {
        base.Awake();
        SetPlayerStat();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > 1.0f)
        {
            Mana += ManaRegen;
            UpdateManaBar();
            timer = 0.0f;
        } 
            
    }

    private void SetPlayerStat()
    {
        SetBaseStats(playerStatsSO);
        ProjectilesPerCast = playerStatsSO.ProjectilesPerCast;
        Maxmana = playerStatsSO.Maxmana;
        Mana = Maxmana;
        ManaRegen = playerStatsSO.ManaRegen;
    }    

    public void UpdateManaBar()
    {
        manaBar.UpdateFiller(Mana, Maxmana);
    }

    public void PowerUP(StatsPowerUpSO powerUpSO)
    {
        foreach (StatsPowerUpDetail detail in powerUpSO.powerUpDetails)
        {
            switch (detail.powerUpType)
            {
                case StatsPowerupType.Damage:
                    ApplyChange(ref Damage, detail.changeValue, detail.changeType);
                    break;
                case StatsPowerupType.MaxHealth:
                    ApplyChange(ref MaxHealth, detail.changeValue, detail.changeType);
                    if (Health > MaxHealth) Health = MaxHealth;
                    UpdateHealthBar();
                    break;
                case StatsPowerupType.ProjectilesPerCast:
                    ApplyChange(ref ProjectilesPerCast, (int)detail.changeValue, detail.changeType);
                    break;
                case StatsPowerupType.Maxmana:
                    ApplyChange(ref Maxmana, detail.changeValue, detail.changeType);
                    if(Mana>Maxmana)Mana = Maxmana;
                    UpdateManaBar();
                    break;
                case StatsPowerupType.ManaRegen:
                    ApplyChange(ref ManaRegen, detail.changeValue, detail.changeType);
                    break;
                    
            }


        }
        UpdateManaBar();

    }
    private void ApplyChange(ref float stat, float value, ChangeType changeType)//for float value
    {
        switch (changeType)
        {
            case ChangeType.Add:
                stat += value;
                break;
            
            case ChangeType.Multiply:
                stat *= value;
                break;
            
        }
    }

    private void ApplyChange(ref int stat, int value, ChangeType changeType)//for int value
    {
        switch (changeType)
        {
            case ChangeType.Add:
                stat += value;
                break;
            
            case ChangeType.Multiply:
                stat = (int)(stat * value); 
                break;
            
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        AudioManager.Instance.SpawnSoundEmitter(transform, "Player_Hurt", Vector3.zero);
        if (Health <= 0) LevelManager.Instance.Lose();
    }
}
