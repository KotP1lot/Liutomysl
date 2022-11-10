using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerGroundedState : PlayerState
{
    protected int xinput;

    private bool JumpInput;
    private bool isGrounded;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
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
        JumpInput = player.InputHandler.JumpInput;
      //  Debug.Log($"INputStop = {player.InputHandler.JumpInputStop} || Jump = {player.InputHandler.JumpInput}");
        if (JumpInput)
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            stateMachine.ChangeState(player.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        
    }
}
