using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : BaseProjectile
{
    [SerializeField] private float turnSpeed = 10f;  
    [SerializeField] private float detectionRadius = 15f; 

    private Collider targetCollider;

    private void FixedUpdate()
    {
        
        if (targetCollider== null)
        {
            FindTarget();
        }

        if(!targetCollider.gameObject.activeSelf)
        {
            Explode();

        }    
        
        if (targetCollider != null)
        {
            Vector3 direction = (targetCollider.transform.position - transform.position).normalized;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, turnSpeed * Time.fixedDeltaTime, 0f);

            transform.forward = newDirection;
            rb.velocity = newDirection *vel;
            
        }
    }

   
    private void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);
        float closestDistance = Mathf.Infinity;
        

        
        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetCollider=hitCollider;
            }
        }

       
        
    }

    
    

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
