using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedState : EnemyOnGroundState
{
    private Animator weaponAnimator;
    public EnemyDamagedState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.soundEnControler.SoundEffect(SoundForEnState.DamagedIron, true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished) 
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }
    public override void PhysicsUpdate()
    {
 
        base.PhysicsUpdate();
      
    }

}
