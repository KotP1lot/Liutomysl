using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyOnGroundState
{
    private float timerStart = 0f;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (enemy.CheckDetection()) stateMachine.ChangeState(enemy.ChaseState);
        if (enemy.CheckIfNeedsToJump()) stateMachine.ChangeState(enemy.JumpState);
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityX(0f);

        if ((enemyData.canWander && !enemyData.canPatrol) || (!enemyData.canWander && enemyData.canPatrol)) startExitTimer();
    }

    private void startExitTimer()
    {
        timerStart = Time.time + Random.Range(enemyData.idleTimeMin, enemyData.idleTimeMax);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= timerStart)
        {
            if (enemyData.canWander && !enemyData.canPatrol) stateMachine.ChangeState(enemy.WanderState);
            if (!enemyData.canWander && enemyData.canPatrol) stateMachine.ChangeState(enemy.PatrolState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
