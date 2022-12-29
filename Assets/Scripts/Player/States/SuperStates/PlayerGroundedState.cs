using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerGroundedState : PlayerState
{
    protected int xinput;
    protected int yinput;

    private bool jumpInput;
    private bool isGrounded;
    private bool isTouchingLadder;
    private bool dashInput;
    private bool isAttacking;
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
       
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        isAttacking = player.InputHandler.isAttacking;
        if (!player.isDamaged && isAttacking)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        if (dashInput)
        {
            stateMachine.ChangeState(player.DashState);
        }
        if (jumpInput)
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
