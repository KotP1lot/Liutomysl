using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizeInputX { get; private set; }
    public int NormalizeInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool JumpDownInput { get; private set; }

    private float inputHoldTime = 0.2f;
    private float ignoreCollisionTime = 0.3f;

    private float jumpInputStartTime;
    private float ignoreCollisionStartTime;
    private void Awake()
    {
        inputHoldTime = 0.2f;
    }
    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckIgnoreCollisionTime();
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
    public void UseJumpInput()
    {
        JumpInput = false;
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
}
