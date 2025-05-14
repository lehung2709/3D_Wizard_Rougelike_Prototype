using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OffensiveSpirit : Spirit
{
    [Header("Offensive Stats")]
    private float damage;
    private float range;
    private float coolDown;
    private float projectilePerCast;
    private ProjectileSO projectileSO;
    private List<StatusEffectDetail> effects;

    [SerializeField] private Transform shootPoint;
    private float lastAttackTime;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float delayBetweenShots;

    private void Awake()
    {
        lastAttackTime = Time.time;
    }

    private void FixedUpdate()
    {
        DetectAndShootEnemy();
    }

    public void SetOffensiveSpiritStats(OffensiveSpiritStatsDetail detail)
    {
        SetSpiritBaseStats();
        this.damage = detail.damage;
        this.range = detail.range;
        this.coolDown = detail.coolDown;
        this.projectilePerCast = detail.projectilePerCast;
        
        
    }

    public void SetStatusEffect(List<StatusEffectDetail> effects)
    {
        this.effects = effects;


    }


    public void SetProjectile( ProjectileSO projectileSO)
    {
        this.projectileSO = projectileSO;
    }    

    
    private void DetectAndShootEnemy()
    {
        
        if (Time.time - lastAttackTime < coolDown) return;


        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, range, enemyLayer);


        if (hitEnemies.Length > 0)
        {
            Transform nearestEnemy = GetNearestEnemy(hitEnemies);

            RotateTowards(nearestEnemy.position);
            lastAttackTime = Time.time;
            StartCoroutine(ShootCoroutine());
            
        }

    }
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        
        if (direction != Vector3.zero)
        {
            
            transform.forward = direction;
        }
    }

    
    private Transform GetNearestEnemy(Collider[] enemies)
    {
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
    private IEnumerator ShootCoroutine()
    {
        for (int i = 0; i < projectilePerCast; i++)
        {
            ShootProjectile();

            yield return new WaitForSeconds(delayBetweenShots);
        }
        
    }

    private void ShootProjectile()
    {
        
        BaseProjectile projectile = Instantiate(projectileSO.projectile, shootPoint.position, Quaternion.identity);
       
        projectile.SetData(projectileSO.vel, transform.forward, damage , effects, projectileSO.damageRangeRadius,enemyLayer);


    }





}
