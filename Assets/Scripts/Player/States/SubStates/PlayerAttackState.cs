using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Vector2 holdPosition;
    private Animator weaponAnimator;
    private Weapon weaponScript;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName) : base(player, stateMachine, playerData, aminBoolName)
    {
        weaponAnimator = player.weapon.GetComponent<Animator>();
        weaponScript = player.weapon.GetComponent<Weapon>();

        weaponScript.OnAnimStarted += StepForward;
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
        Debug.Log("attack");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("ability");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        var animatorInfo = weaponAnimator.GetCurrentAnimatorClipInfo(0);
        if (animatorInfo[0].clip.name=="Idle")
        {
            player.InputHandler.isAttacking = false;
            isAbilityDone = true;
        }
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public void StepForward()
    {
        player.RB.AddForce(new Vector2(playerData.stepForce * player.FacingDirection, 0),ForceMode2D.Impulse);
    }
}
