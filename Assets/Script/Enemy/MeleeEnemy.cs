using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public override void DoAttack()
    {
        base.DoAttack();
        if (Vector3.Distance(transform.position, playerTransform.position) < attackRange+0.1f)
        {
            transform.forward=(playerTransform.position-transform.position).normalized;
            playerStats.TakeDamage(stats.Damage);
            playerStats.ApplyStatusEffect(stats.statusEffectDetails,stats.Damage);
        }
    }
}
