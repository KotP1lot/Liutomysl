using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerGroundedState : PlayerState
{
    protected int xinput;
    protected int yinput;

    private bool JumpInput;
    private bool isGrounded;
    private bool isTouchingLadder;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
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
        JumpInput = player.InputHandler.JumpInput;
        if (JumpInput)
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        else if (isTouchingLadder && yinput > 0)
        {
            stateMachine.ChangeState(player.LadderClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }
}
