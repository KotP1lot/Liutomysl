using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(xinput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (isAnimationFinished || xinput == 0)//«Ã≤Õ»“» œ–» ¿Õ≤Ã¿÷≤ø
        {
            stateMachine.ChangeState(player.IdelState);
        }
    }
}
