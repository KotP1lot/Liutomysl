using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public enum AttackType { 
Light,
Strong,
none
}
public class PlayerInputHandler : MonoBehaviour
{
    #region
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizeInputX { get; private set; }
    public int NormalizeInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool JumpDownInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool LightAttackInput { get; private set; }
    public bool StrongAttackInput { get; private set; }

    private float inputHoldTime = 0.2f;
    private float ignoreCollisionTime = 0.3f;
    //«Ã≤Õ»“» « œŒﬂ¬Œﬁ ¿Õ≤Ã¿÷≤Ø
    private float startDashTime;
    private float dashTime = 0.3f;

    private float jumpInputStartTime;
    private float ignoreCollisionStartTime;
    #endregion

    #region Attack
    public AttackType attackInput { get; private set; }
    public bool isAttacking { get; private set; }
    public int countAttack { get; private set; }
    public Dictionary<int, AttackType> AttackInputs { get; private set; }
    private float timeToContinueAttack;
    private float timeForContinueAttack;
    private bool canContinueAttack;
    
    #endregion
    private void Start()
    {
        inputHoldTime = 0.2f;
        countAttack = 0;
        canContinueAttack = true;
        AttackInputs = new Dictionary<int, AttackType>();
        isAttacking = false;
    }


    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckIgnoreCollisionTime();
        CheckDashTime();
        //CheckIfCanContinueAttack();
    }
    public void onMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormalizeInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormalizeInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }
    public void onJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (NormalizeInputY < 0)
            {
                JumpDownInput = true;
                ignoreCollisionStartTime = Time.time;
            }
            else
            {
                JumpInput = true;
                JumpInputStop = false;
                jumpInputStartTime = Time.time;
            }
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }
    public void onDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            startDashTime = Time.time;
            DashInput = true;
        }
        if (context.canceled)
        {
            DashInput = false;
        }
    }

    public void onLightAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            LightAttackInput = true;
            //Debug.Log("Light Attack Pressed");
            //if (canContinueAttack)
            //{
   
            //    attackInput = AttackType.Light;
            //    if (AttackInputs.ContainsKey(countAttack)) AttackInputs.Remove(countAttack);
            //    AttackInputs.Add(countAttack, attackInput);
      
            //    timeToContinueAttack = Time.time + 0.2f;
            //    timeForContinueAttack = timeToContinueAttack + 1f;
            //    canContinueAttack = false;
            //    isAttacking = true;

            //    if (countAttack + 1 > 2) countAttack = 0; else countAttack++;
            //    Debug.Log("Light Attack Added, counter" + countAttack);
            //}
        }
        if (context.canceled)
        {
            LightAttackInput = false;
            //if (AttackInputs.ContainsKey(countAttack)) AttackInputs.Remove(countAttack);
        }
    }

    public void ResetAttackInput()
    {
        StrongAttackInput = false;
        LightAttackInput = false;
    }

    public void onStrongAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StrongAttackInput = true;
            //Debug.Log("Strong Attack Pressed");
            //if (canContinueAttack)
            //{
            //    attackInput = AttackType.Strong;
            //    if (AttackInputs.ContainsKey(countAttack)) AttackInputs.Remove(countAttack);
            //    AttackInputs.Add(countAttack, attackInput);

            //    timeToContinueAttack = Time.time + 0.5f;
            //    timeForContinueAttack = timeToContinueAttack + 1f;
            //    isAttacking = true;

            //    canContinueAttack = false;
            //    countAttack = countAttack > 2 ? 0 : countAttack++;
            //    Debug.Log("Strong Attack Added");
            //}
        }
        if (context.canceled)
        {
            StrongAttackInput = false;
            //if (AttackInputs.ContainsKey(countAttack)) AttackInputs.Remove(countAttack);
        }
    }
    public void OnAttackAnimFinished()
    {
        if (AttackInputs.Count == 0)
        {
            isAttacking = false;
        }
    }
    public void UseJumpInput()
    {
        JumpInput = false;
    }



    #region Check Functions
    private void CheckDashTime()
    {
        if (Time.time >= startDashTime + dashTime)
        {
            DashInput = false;
        }
    }
    private void CheckIgnoreCollisionTime()
    {
        if (Time.time >= ignoreCollisionStartTime + ignoreCollisionTime)
        {
            JumpDownInput = false;
        }
    }
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckIfCanContinueAttack()
    {
        if(Time.time>= timeToContinueAttack) canContinueAttack = true;
        if (Time.time >= timeForContinueAttack) { AttackInputs.Clear(); countAttack = 0; }
    }
    #endregion
}
