using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizeInputX { get; private set; }
    public int NormalizeInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0f;

    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
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
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }
    public void UseJumpInput()
    {
        JumpInput = false;
    }

    private void CheckJumpInputHoldTime() 
    { 
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}