using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerGroundedState
{
    private Vector2 holdPosition;
    private Animator weaponAnimator;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
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
        holdPosition = player.transform.position;
        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
    }
    private void HoldPosition() 
    { 
        player.transform.position = holdPosition;
    }
    public override void PhysicsUpdate()
    {
 
        base.PhysicsUpdate();
      
    }
}
