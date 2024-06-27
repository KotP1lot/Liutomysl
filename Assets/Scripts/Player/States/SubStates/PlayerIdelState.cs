using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdelState : PlayerGroundedState
{
    private bool isIgnoreCollision;
    private Vector2 holdPosition;
    public PlayerIdelState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
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
        player.SetVelocityX(0f);
        isIgnoreCollision = player.InputHandler.JumpDownInput;
        //if(!isIgnoreCollision && player.CheckIfOnlyOnGround()) HoldPosition();
        if (xinput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
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
