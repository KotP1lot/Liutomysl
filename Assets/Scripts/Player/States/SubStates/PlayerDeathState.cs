using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerGroundedState
{
    private Animator weaponAnimator;
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
        weaponAnimator = player.weapon.GetComponent<Animator>();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);

        if (player.weapon.activeSelf) weaponAnimator.Play("Death");
    }

    public override void AnimationFinishTrigger()
    {
        player.sceneTransition.DeathTransition(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
