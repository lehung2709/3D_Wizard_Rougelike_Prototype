using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInputController PlayerInput { get; private set; }
    private Rigidbody rb;
    public Animator animator;
    public Transform shootPoint;
    public PlayerStats stats;
    private SpellsManager spellsManager;
    [SerializeField] public float shootTimeHolder=0.5f;
    [SerializeField] private float moveSpeed=0.0f;
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float changeSS;
    [SerializeField] private float rotateSpeed = 10.0f;
    [SerializeField] private float angle;
    [SerializeField] private float sin;
    [SerializeField] private float cos;
    private bool isMove=false;
    private bool isRun=false;
    AudioEmitter audioEmitter;
    public LayerMask enemyMask;


    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInputController>();
        PlayerInput.SpellCasting += StartCasting;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        shootPoint= transform.Find("ShootPoint");
        stats=GetComponent<PlayerStats>();
        stats.Dying += Death;
        spellsManager =GetComponentInChildren<SpellsManager>();

    }
    

    private void FixedUpdate()
    {
        RotateHandler();
        MoveHandler();
        CalculateSinCosAngle();
        AudioHandler();
    }
    
    private void MoveHandler()
    {
        if (stats.IsNotStunned)
        {

            rb.velocity = PlayerInput.MoveInput * stats.MoveSpeed;
        }
        else
        {
            rb.velocity=Vector3.zero;
        }    
        
    }    
    private void RotateHandler()
    {
        if (PlayerInput.AimInput != Vector3.zero && stats.IsNotStunned)
        {
            Quaternion targetRotation = Quaternion.LookRotation(PlayerInput.AimInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
            if (PlayerInput.MoveInput != Vector3.zero)
            {
                moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, Time.deltaTime * changeSS);
            }

        }
        else if (PlayerInput.MoveInput != Vector3.zero && stats.IsNotStunned)
        {
            Quaternion targetRotation = Quaternion.LookRotation(PlayerInput.MoveInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, Time.fixedDeltaTime * changeSS);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0.0f, Time.fixedDeltaTime * changeSS);
        }

        
        animator.SetFloat("Speed", moveSpeed);



    }

    private void AudioHandler()
    {
        if(PlayerInput.MoveInput!= Vector3.zero && stats.IsNotStunned )
        {
            
                if(PlayerInput.AimInput != Vector3.zero)
                {
                   
                   if(!isMove || isRun)
                   {
                      if(audioEmitter!=null)audioEmitter.StopAndReturnToPool();
                      audioEmitter = AudioManager.Instance.SpawnSoundEmitter(transform, "Walk", Vector3.zero);
                      isRun=false;
                      isMove = true;
                   }    
                   
                    
                } 
                else  
                {
                   if (!isMove || !isRun)
                   {
                      if (audioEmitter != null) audioEmitter.StopAndReturnToPool();
                      audioEmitter = AudioManager.Instance.SpawnSoundEmitter(transform, "Run", Vector3.zero);
                      isRun=true;
                      isMove = true;
                   }
                } 
                
               
                return;
            
            
        }
        if (audioEmitter != null)
        {
            audioEmitter.StopAndReturnToPool();
            audioEmitter = null;
            isMove = false;
            
        }
    }    
    private void CalculateSinCosAngle()
    {
        Vector3 moveDir = PlayerInput.MoveInput.normalized;
        Vector3 aimDir = PlayerInput.AimInput.normalized;

        
        if (moveDir != Vector3.zero && aimDir != Vector3.zero)
        {
            
            angle = Vector3.Angle(moveDir, aimDir);

            
            float angleRad = angle * Mathf.Deg2Rad;

            
            sin = Mathf.Sin(angleRad);
            cos = Mathf.Cos(angleRad);

        }
        else
        {
            
            sin = 0f;
            cos = 0f;
            
        }
        animator.SetFloat("Sin", sin);
        animator.SetFloat("Cos", cos);

        
    }

    private void StartCasting()
    {
      
       spellsManager.SelectedSpell.Cast(this);

    }  
    private void Death()
    {
        AudioManager.Instance.SpawnSoundEmitter(transform, "Death", Vector3.zero);
    }    

   



    
}
