using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected Rigidbody rb;
    private List<StatusEffectDetail> statusEffectDetails;
    private float damage;
    protected float vel;

    private float damageRangeRadius;
    [SerializeField]protected LayerMask targetMask;
    [SerializeField] private float maxRange;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float traveledDistance = 0f;
    [SerializeField] private LayerMask explosionMasks;
    [SerializeField] private GameObject explodeEffect;
    [SerializeField] private string explodeSoundName;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        traveledDistance = Vector3.Distance(startPos, transform.position);
        if(traveledDistance>maxRange) Explode();
    }
    public void SetData(float vel,Vector3 direction,float damage,List<StatusEffectDetail> statusEffectDetails,float damageRangeRadius,LayerMask targetMask)
    {
        this.vel = vel;
        rb.velocity = direction*vel;
        this.damage = damage;
        this.statusEffectDetails = statusEffectDetails;
        this.damageRangeRadius = damageRangeRadius;
        this.targetMask = targetMask;
        traveledDistance = 0.0f;
        startPos = transform.position;
        explosionMasks += targetMask;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (((1 << other.gameObject.layer) & explosionMasks) != 0)
        {
            Explode();
        }
    }

    protected void Explode()
    {
        if(explodeEffect != null) 
        Instantiate(explodeEffect,transform.position,Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRangeRadius, targetMask);
        foreach (Collider hitCollider in hitColliders)
        {
            
            IDamagable damagableComponent = hitCollider.GetComponent<IDamagable>();
            if (damagableComponent != null)
            {
                damagableComponent.TakeDamage(damage);
                
                damagableComponent.ApplyStatusEffect(statusEffectDetails, damage);
            }
        }
        if(explodeSoundName!= "") AudioManager.Instance.SpawnSoundEmitter(null,explodeSoundName,transform.position);
        Destroy(gameObject);
    }


}
