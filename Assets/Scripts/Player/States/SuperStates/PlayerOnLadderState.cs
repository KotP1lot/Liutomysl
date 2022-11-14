using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnLadderState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingLadder;
    protected int xInput;
    protected int yInput;
    public PlayerOnLadderState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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

        xInput = player.InputHandler.NormalizeInputX;
        yInput = player.InputHandler.NormalizeInputY;

        if (isGrounded && yInput <=0 && !isTouchingLadder )
        {
            stateMachine.ChangeState(player.IdelState);
        }
        else if(!isTouchingLadder || xInput != 0)
        {
            stateMachine.ChangeState(player.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
