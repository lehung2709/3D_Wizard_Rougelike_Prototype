using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string Key {  get;  set; }
    public EnemySpawner Spawner { get; set; }
    [SerializeField] protected PlayerStats playerStats;
    protected Transform playerTransform;
    protected Animator animator;
    protected string currentAnimBoolName;
    protected NavMeshAgent navMeshAgent;
    [SerializeField] protected float updatePathInterval = 0.1f;
    [SerializeField] protected string attackAnimBoolName = "Attack";
    [SerializeField] protected string idleAnimBoolName = "Idle";
    [SerializeField] protected string moveAnimBoolName = "Move";
    protected bool isMove = true;
    protected bool isIdle = false;
    protected bool isAttack = false;
    protected bool isCoolDown=false;
    [SerializeField] protected float attackRange;
    [SerializeField] protected EnemyStats stats;
    

    protected virtual void Awake()
    {
        if(playerStats != null) 
        playerTransform = playerStats.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<EnemyStats>();
        stats.Dying += Die;
        currentAnimBoolName = idleAnimBoolName;

    }

    protected virtual void Start() 
    {
        navMeshAgent.speed = stats.MoveSpeed;
        Move();
    }

    protected virtual void FixedUpdate()
    {
        Handle();
        
    }

    protected virtual IEnumerator UpdatePathCoroutine()
    {
        
            while (isMove)
            {
                navMeshAgent.SetDestination(playerTransform.position);
                yield return new WaitForSeconds(updatePathInterval);
            }
        
    }

    protected virtual void Attack()
    {
        ChangeAnimation(attackAnimBoolName);
        navMeshAgent.isStopped = true;
        isMove = false;
        isAttack = true;
        isIdle = false;
    }

    protected virtual void Idle()
    {
        ChangeAnimation(idleAnimBoolName);
        navMeshAgent.isStopped = true;
        isIdle = true;
        isAttack = false;
        isMove = false;
    }

    protected virtual void Move()
    {
        ChangeAnimation(moveAnimBoolName);
        navMeshAgent.isStopped = false;
        isIdle = false;
        isAttack = false;
        isMove = true;
        StartCoroutine(UpdatePathCoroutine());
    }

    protected virtual void Handle()
    {
        if (!isCoolDown && !isAttack)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) < attackRange)
            {
                Attack();
            }
            else if (!isMove)
            {
                Move();
            }
        }
    }

    protected virtual IEnumerator HandleAfterAttack()
    {
        Idle();
        isCoolDown = true;
        yield return new WaitForSeconds(stats.AttackCooldown);
        isCoolDown = false;
        Move();
    }

    public virtual void ExitAttack()
    {
        StartCoroutine(HandleAfterAttack());
    }

    public virtual void DoAttack()
    {
        this.transform.forward=(playerStats.transform.position - transform.position).normalized;
    }

    protected void ChangeAnimation(string animBoolName)
    {
        animator.SetBool(currentAnimBoolName, false);
        currentAnimBoolName = animBoolName;
        animator.SetBool(animBoolName, true);
    }
    protected virtual void ReturnToPool()
    {
        navMeshAgent.enabled = false;
        Spawner.Return(Key,this);
        
    }    
    protected void Die()
    {
        XPSpawner.Instance.SpawnXPCrystal(this.transform.position, 3);
        ReturnToPool();
    }   
    public void SetPlayerAndPos(PlayerStats player,Vector3 pos)
    {
        stats.SetInitialEnemyStats();
        this.playerStats = player;
        this.playerTransform = player.transform;
        navMeshAgent.enabled = true;
        navMeshAgent.Warp(pos);
        Move();
    

    }


}
