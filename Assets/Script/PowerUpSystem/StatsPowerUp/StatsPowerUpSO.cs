using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatsPowerUpDetail
{
    public StatsPowerupType powerUpType;
    public ChangeType changeType;
    public float changeValue;
}
[CreateAssetMenu(fileName ="NewStatPowerUpSO",menuName ="StatPowerUpSO")]
public class StatsPowerUpSO : ScriptableObject
{
    public Sprite avatar;
    public string powerUpName;
    public List<StatsPowerUpDetail> powerUpDetails;
}
