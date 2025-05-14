using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemySO",menuName ="NewEnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public Enemy prefab;
    public EnemyStatsSO stats;
    public int difficultyPoint;
}
