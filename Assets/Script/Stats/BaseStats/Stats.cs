using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using System;

public class Stats : MonoBehaviour,IDamagable
{
    public Action Dying;

    [SerializeField] protected SliderAndFillBar healthUI;
    [SerializeField] private GameObject statusEffectUI;
    public GameObject StatusEffectUI { get => statusEffectUI; }
    [SerializeField] private Image sEUIElementPrefab;
    public Image SEUIElementPrefab { get => sEUIElementPrefab; }

    public float MaxHealth;
    public float MoveSpeed;
    public float Damage;
    public float AttackCooldown;

    public bool IsNotStunned;
    
    public float Health { get; set; }

    private BurnCSE burnCSE;
    private StunCSE stunCSE;
    private SlowCSE slowCSE;



    protected virtual void Awake()
    {
        
        IsNotStunned= true;
        burnCSE = new BurnCSE(this);
        stunCSE = new StunCSE(this);
        slowCSE = new SlowCSE(this);

        
    }


    protected void SetBaseStats(StatsSO statsSO)
    {
        this.MaxHealth = statsSO.MaxHealth;
        this.Health=MaxHealth;
        this.MoveSpeed = statsSO.MoveSpeed;
        this.Damage = statsSO.Damage;
        this.AttackCooldown = statsSO.AttackCooldown;
    }    
    public virtual void TakeDamage(float damage)
    {
        Health-=damage;
        UpdateHealthBar();
        DamagePopUpGenerator.Instance.CreatePopUp(transform.position,damage.ToString());
        AudioManager.Instance.SpawnSoundEmitter(transform, "Hit", Vector3.zero);
        if (Health <= 0)
        {
            Health= 0;
            DamagePopUpGenerator.Instance.CreateDisapearEffect(transform.position);
            Dying?.Invoke();
        }
        
    }

    public  void ApplyStatusEffect(List<StatusEffectDetail> effects, float damage)
    {

        foreach (var effect in effects)
        {
            StatusEffectHandler(effect, damage);
        }
    }

    public virtual void StatusEffectHandler(StatusEffectDetail effectDetail, float damage)
    {
        switch (effectDetail.Type)
        {
            case StatusEffectType.Burned:

                burnCSE.StartStatusEffect(damage * effectDetail.intensity, effectDetail.duration);
                break;
            case StatusEffectType.Stunned:

                stunCSE.StartStatusEffect(0.0f,effectDetail.duration);
                break;
            case StatusEffectType.SlowedDown:

                slowCSE.StartStatusEffect(effectDetail.intensity, effectDetail.duration);
                break;

        }
    }

    public Image CreateSEUIElement()
    {
        Image e=GameObject.Instantiate(sEUIElementPrefab);
        e.transform.SetParent(statusEffectUI.transform,false);
        return e;
    }    
    protected void UpdateHealthBar()
    {
        healthUI.UpdateFiller(Health, MaxHealth);
    }    
    
}

   

   




