using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSpawner : MonoBehaviour
{
    public static XPSpawner Instance { get; private set; }
    [SerializeField] private XPCrystal XPCrystalPrefab;
    [SerializeField] private float spawnForce = 5f;

    private Queue<XPCrystal> crystalPool= new Queue<XPCrystal>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void SpawnXPCrystal(Vector3 spawnPosition, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            XPCrystal crystal;

            if (crystalPool.Count > 0)
            {
                crystal = crystalPool.Dequeue();
            }
            else
            {
                crystal = Instantiate(XPCrystalPrefab); 
            }

            crystal.transform.position = spawnPosition;
            crystal.gameObject.SetActive(true);

            Rigidbody rb = crystal.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(0.5f, 1f),
                    Random.Range(-1f, 1f)).normalized;

                rb.velocity = randomDirection * spawnForce;
            }
        }
    }

    public void ReturnToPool(XPCrystal crystal)
    {
        crystal.gameObject.SetActive(false);
        crystalPool.Enqueue(crystal);
    }

}
