using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool pauseActive { get; private set; }

    [HideInInspector] public bool InteractInput;

    private float inputHoldTime = 0.2f;
    private float ignoreCollisionTime = 0.3f;
    //«Ã≤Õ»“» « œŒﬂ¬Œﬁ ¿Õ≤Ã¿÷≤Ø
    private float startDashTime;
    private float dashTime = 0.15f;
    public bool stopDash { get; private set; }

    private float jumpInputStartTime;
    private float ignoreCollisionStartTime;

    #endregion

    #region Attack
    [HideInInspector]public bool isAttacking;
    
    #endregion
    private void Start()
    {
        inputHoldTime = 0.2f;
        isAttacking = false;
    }


    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckIgnoreCollisionTime();
        CheckDashTime();
    }
    public void onMoveInput(InputAction.CallbackContext context)
    {
        if (!pauseActive)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            NormalizeInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            NormalizeInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
    }
    public void onJumpInput(InputAction.CallbackContext context)
    {
        if (!pauseActive && context.started)
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
        if (!pauseActive && context.started)
        {
            stopDash = false;
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
        if (!pauseActive && context.started)
        {
            LightAttackInput = true;
            isAttacking = true;
        }
        if (context.canceled)
        {
            LightAttackInput = false;
        }
    }

    public void ResetAttackInput()
    {
        StrongAttackInput = false;
        LightAttackInput = false;

        isAttacking = false;
    }

    public void onStrongAttackInput(InputAction.CallbackContext context)
    {
        if (!pauseActive && context.started)
        {
            StrongAttackInput = true;
            isAttacking = true;
        }
        if (context.canceled)
        {
            StrongAttackInput = false;
        }
    }
    public void onInteractInput(InputAction.CallbackContext context)
    {
        if (!pauseActive && context.started)
        {
            InteractInput = true;
        }
        if (context.canceled)
        {
            InteractInput = false;
        }
    }
    public void onPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
        }
        if (context.canceled)
        {
            pauseActive = !pauseActive;
        }
    }

    public void ClosePauseMenu()
    {
        pauseActive = false;
    }
    public void OnAttackAnimFinished()
    {
        isAttacking = false;
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
            stopDash=true;
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
    #endregion
}
