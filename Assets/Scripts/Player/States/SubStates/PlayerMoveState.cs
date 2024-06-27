using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private float timeStep;
    private float timetoNextStep = 0.3f;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.walkParticles.Play();
        timeStep = Time.time;
        player.soundController.SoundEffect(SoundForState.FootStep, true);
    }

    public override void Exit()
    {
        base.Exit();

        player.walkParticles.Stop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.CheckIfShouldFlip(xinput);
        player.SetVelocityX(playerData.movementVelocity * xinput);

        if (xinput == 0)
        {
            stateMachine.ChangeState(player.IdelState);
        }
        if(Time.time - timeStep > timetoNextStep)
        {
            timeStep = Time.time;
            player.soundController.SoundEffect(SoundForState.FootStep, true);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
   
    }
}
