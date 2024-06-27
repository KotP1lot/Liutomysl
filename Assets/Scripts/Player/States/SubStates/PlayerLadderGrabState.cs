using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerLadderGrabState : PlayerOnLadderState
{
    private Vector2 holdPosition;
    public PlayerLadderGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        holdPosition = player.transform.position;
        HoldPosition();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();
        
        if (yInput != 0)
        {
            stateMachine.ChangeState(player.LadderClimbState);
        }
    }
    
    private void HoldPosition()
    {
        player.transform.position = holdPosition;
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }
}
