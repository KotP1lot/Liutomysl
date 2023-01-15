using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedState : PlayerAbilityState
{
    private Animator weaponAnimator;
    public PlayerStunnedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
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

        if (player.weapon.activeSelf) weaponAnimator.Play("Idle");

        player.walkParticles.Play();
    }

    public override void Exit()
    {
        base.Exit();

        player.isDamaged = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.CurrentVelocity.x == 0) { player.walkParticles.Stop(); }
    }
    public override void PhysicsUpdate()
    {
 
        base.PhysicsUpdate();
      
    }

    public override void AnimationFinishTrigger()
    {
        isAbilityDone = true;
    }
}
