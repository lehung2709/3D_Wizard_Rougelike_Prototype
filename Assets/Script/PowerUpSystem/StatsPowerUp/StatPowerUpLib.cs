using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPowerUpLib : MonoBehaviour
{
    public static StatPowerUpLib Instance { get; private set; }
    [SerializeField] private List<StatsPowerUpSO> statsPowerUpSOs;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance =this;
        }
        else
        {
            Destroy(this);
        }


    }
    public StatsPowerUpSO RandomPick()
    {
        if (statsPowerUpSOs == null || statsPowerUpSOs.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, statsPowerUpSOs.Count);

        return statsPowerUpSOs[randomIndex];

    }
}
