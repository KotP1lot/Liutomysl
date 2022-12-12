using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGroundedAttackState : PlayerGroundedState
{
    private Dictionary<int, AttackType> attackInputs;
    private Weapon weapon;
    public PlayerGroundedAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
  
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        weapon = player.weapon.GetComponent<Weapon>();

        UpdateAnimState();

        weapon.OnAnimFinished += UpdateAnimState;
    }

    public override void Exit()
    {
        base.Exit();
    }
    private void UpdateAnimState()
    {
        attackInputs = player.InputHandler.AttackInputs;
        if (attackInputs.ContainsKey(0))
        {
            weapon.ChangeAttackInputs(attackInputs);

        }
        else
            stateMachine.ChangeState(player.IdelState);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
