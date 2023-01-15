using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyOnGroundState
{
    private int patrolDirection;
    private int nextDirection = 1;
    private float targetX;

    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
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

        patrolDirection = nextDirection;

        enemy.CheckIfShouldFlip(patrolDirection);

        var distance = (enemyData.startingPosition.x + enemy.patrolRange * patrolDirection - enemy.transform.position.x) * patrolDirection;

        nextDirection *= -1;
        targetX = enemy.transform.position.x + distance * patrolDirection;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (patrolDirection == 1 && enemy.transform.position.x >= targetX || patrolDirection == -1 && enemy.transform.position.x <= targetX)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
        else
        {
            enemy.SetVelocityX(enemyData.movementVelocity * patrolDirection);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
