using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpState : EnemyOnGroundState
{
    private float timerStart = 0f;

    public EnemyJumpState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityY(enemyData.jumpVelocity);
        enemy.SetVelocityX(enemyData.movementVelocity * enemy.FacingDirection);

        startTimer();
    }

    private void startTimer()
    {
        timerStart = Time.time + enemyData.fallDelay;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.Animator.SetFloat("velocityY", enemy.CurrentVelocity.y);
        if (Time.time >= timerStart)
        {
            enemy.SetVelocityY(0f);
        }
        if (enemy.CheckIfGrounded())
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
