using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewSpawnData",menuName ="NewSpawnData")]
public class SpawnData : ScriptableObject
{
    public EnemySO[] enemySOs;
    public int maxDifficultyPoint;
    public int difficultyPointIncreaseRate;
    public float spawnInterval;
    public float spawnIntervalIncreaseRate;
}
