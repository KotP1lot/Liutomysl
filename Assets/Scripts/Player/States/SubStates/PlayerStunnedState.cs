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
        player.soundController.SoundEffect(SoundForState.Stunned, true);
        if (player.weapon.activeSelf) weaponAnimator.Play("Idle");
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

    public override void AnimationFinishTrigger()
    {
        player.isDamaged = false;
        isAbilityDone = true;
    }
}
