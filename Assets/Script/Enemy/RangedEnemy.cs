using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] protected ProjectileSO projectileSO;
    [SerializeField] protected float warningDuration;
    [SerializeField] private Transform shootPoint;
    protected bool isWarning = false;
    [SerializeField] protected LayerMask playerMask;
    public override void DoAttack()
    {
        base.DoAttack();
        BaseProjectile projectile=  Instantiate(projectileSO.projectile,shootPoint.position,Quaternion.identity);
        projectile.SetData(projectileSO.vel, transform.forward, stats.Damage, stats.statusEffectDetails, 1.0f,playerMask);
    }

    protected override void Handle()
    {
        if (!isAttack)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
            {
                if (!isCoolDown && !isWarning)
                {

                    StartCoroutine(WarningAndAttack());
                }
                else if(!isIdle)
                {
                    Idle();
                } 
                    
            }
            else if (!isMove)
            {
                Move();
            }
        }
    }
    protected virtual IEnumerator WarningAndAttack()
    {
        transform.forward = (playerTransform.position - transform.position).normalized;
        isWarning = true;
        yield return new WaitForSeconds(warningDuration);
        isWarning = false;
        Attack();
    }
}
