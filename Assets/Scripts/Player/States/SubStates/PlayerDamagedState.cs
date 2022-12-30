using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : PlayerAbilityState
{
    private Animator weaponAnimator;
    public PlayerDamagedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
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

        if(player.weapon.activeSelf) weaponAnimator.Play("Idle");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(player.CurrentVelocity.x==0) // «–Œ¡»“‹ œŒ «¿ ≤Õ◊≈ÕÕﬁ ¿Õ≤Ã¿÷≤Ø
        {
            player.isDamaged = false;
            isAbilityDone = true;
        }
    }
    public override void PhysicsUpdate()
    {
 
        base.PhysicsUpdate();
      
    }

}
