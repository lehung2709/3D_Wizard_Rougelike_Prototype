using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEventLinker : MonoBehaviour
{
    private Enemy enemyScript;
    private void Awake()
    {
        enemyScript = GetComponentInParent<Enemy>();
    }
    public void DoAttack()
    {
        enemyScript.DoAttack();
    }    

    public void ExitAttack()
    {
        enemyScript.ExitAttack();
    }    
    
}
