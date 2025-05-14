using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSpell", menuName = "Spell/NewAttackSpell")]

public class AttackSpellSO : SpellSO
{
    public float damageScalingFactor;
    public float damageRangeRadius;
    public float vel;
    public BaseProjectile projectile;
    public List<StatusEffectDetail> effects;
}
