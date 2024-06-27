using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderClimbState : PlayerOnLadderState
{
    private float timeStep;
    private float timetoNextStep = 0.3f;
    public PlayerLadderClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
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
        timeStep = Time.time;
        player.soundController.SoundEffect(SoundForState.Ladder, true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityX(0f);
        player.SetVelocityY(yInput * playerData.movementVelocityOnLadder);

        if (yInput == 0)
        {
   
            stateMachine.ChangeState(player.LadderGrabState);
        }
        if (Time.time - timeStep > timetoNextStep)
        {
            timeStep = Time.time;
            player.soundController.SoundEffect(SoundForState.Ladder, true);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
