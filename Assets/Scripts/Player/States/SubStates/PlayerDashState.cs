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

        player.SpendStamina(playerData.dashStaminaCost);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CurrentVelocityY = player.CurrentVelocity.y;
        player.SetVelocityY(CurrentVelocityY < 0 ? CurrentVelocityY : 0);
        player.SetVelocityX(playerData.dashVelocity * player.FacingDirection);

        if(player.InputHandler.stopDash)
        {
            stateMachine.ChangeState(player.IdelState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
