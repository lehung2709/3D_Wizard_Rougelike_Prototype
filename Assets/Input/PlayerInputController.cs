using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    public Vector3 MoveInput { get; private set; }

    public Vector3 AimInput {  get; private set; }
    
    public Action SpellCasting;

    private PlayerStats playerStats;

    private bool canAttack = true;

    private float coolDownTimer;

    [SerializeField] private Image coolDownFiller;

    [SerializeField] private OnScreenStick screenStick;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }



    public void OnMoveInput(InputAction.CallbackContext context)
    {
        
        Vector2 rawMoveInput=context.ReadValue<Vector2>().normalized;
        MoveInput = new Vector3(rawMoveInput.x,0.0f,rawMoveInput.y);
    }  
    
    public void OnAimInput(InputAction.CallbackContext context)
    {
        Vector2 rawAimInput=context.ReadValue<Vector2>().normalized;
        AimInput = new Vector3(rawAimInput.x,0.0f,rawAimInput.y);
        if (context.canceled)
        {
            SpellCasting?.Invoke();
            StartCoroutine(AttackCoolDownCouroutine());
        } 
            
    }
    private IEnumerator AttackCoolDownCouroutine()
    {
        canAttack = false;
        coolDownTimer = playerStats.AttackCooldown;
        screenStick.enabled = canAttack;
        while (coolDownTimer > 0)
        {
            coolDownTimer -= Time.fixedDeltaTime;
            coolDownFiller.fillAmount = coolDownTimer/playerStats.AttackCooldown;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        canAttack = true;
        screenStick.enabled=canAttack;
            
    }    

    
}
