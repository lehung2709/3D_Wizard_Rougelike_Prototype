using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPCrystal : MonoBehaviour
{
    private Rigidbody rb;
    private Transform playerTransform;
    private PowerUpManager powerUpManager;
    [SerializeField] private float collectedVel;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void StartBeingCollected(Transform player)
    {
        this.playerTransform = player;
        this.powerUpManager=player.GetComponent<PowerUpManager>();
        StartCoroutine(BeingCollectedCoroutine());
        
    }    
    private IEnumerator BeingCollectedCoroutine()
    {
        while (true)
        {
            if(Vector3.Distance(this.transform.position, playerTransform.position) < 0.2f)
            {
                powerUpManager.CollectXP();
                XPSpawner.Instance.ReturnToPool(this);
                yield break;
            }

            rb.velocity = (playerTransform.position - this.transform.position).normalized * collectedVel;
            yield return new WaitForSeconds(0.1f);
        }
    }    
}
