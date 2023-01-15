using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyOnGroundState
{
    private Animator weaponAnimator;
    public EnemyDeathState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.soundEnControler.SoundEffect(SoundForEnState.DamagedIron, true);
        enemy.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {
 
        base.PhysicsUpdate();
      
    }

    public override void AnimationFinishTrigger()
    {
        enemy.Die();
    }
}
