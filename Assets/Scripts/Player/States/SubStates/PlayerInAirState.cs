using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
    }
}
