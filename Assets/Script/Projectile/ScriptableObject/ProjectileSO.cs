using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileSO", menuName = "NewProjectileSO")]

public class ProjectileSO :ScriptableObject
{
    public string ProjectileName;

    public float damageRangeRadius; 
    public float vel;
    public BaseProjectile projectile;
}
