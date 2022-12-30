using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingLadder;
    private int xinput;
    private int yinput;
    private bool JumpInputStop;
    private bool isJumping;
    private float lastYvelocity;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingLadder = player.CheckIfTouchingLadder();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xinput = player.InputHandler.NormalizeInputX;
        yinput = player.InputHandler.NormalizeInputY;
        JumpInputStop = player.InputHandler.JumpInputStop;

        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            if (lastYvelocity < -30f)
            {
                int damage = ((int)lastYvelocity*-1 - 30) * playerData.fallDamageModifier;
                player.GetDamaged(damage);
            }
            else
            {
                stateMachine.ChangeState(player.LandState);
            }
        }
        else if(isTouchingLadder && yinput != 0) 
        {
            stateMachine.ChangeState(player.LadderClimbState);
        }
        else
        {
            player.CheckIfShouldFlip(xinput);
            player.SetVelocityX(playerData.movementVelocity * xinput);
        }

        if(player.CurrentVelocity.y!=0f) lastYvelocity = player.CurrentVelocity.y;
    }
    private void CheckJumpMultiplier()
    {

        if (isJumping)
        {
            if (JumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetIsJumping() => isJumping = true;
}
