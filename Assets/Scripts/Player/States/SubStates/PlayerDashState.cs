using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private bool dashInput;
    private float CurrentVelocityY;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
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

        dashInput = player.InputHandler.DashInput;
        if (!dashInput)
            stateMachine.ChangeState(player.IdelState);
        else
        {
            CurrentVelocityY = player.CurrentVelocity.y;
            player.SetVelocityY(CurrentVelocityY < 0 ? CurrentVelocityY : 0);
            player.SetVelocityX(playerData.dashVelocity * player.FacingDirection);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
