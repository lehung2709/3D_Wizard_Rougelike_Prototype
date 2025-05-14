using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 

{
    public float Health { get; set; }
    public void TakeDamage(float damage);
    public void ApplyStatusEffect(List<StatusEffectDetail> effects,float damage);
    public void StatusEffectHandler(StatusEffectDetail effect,float damage); 
}
