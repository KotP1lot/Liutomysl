using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWanderState : EnemyOnGroundState
{
    private int wanderDirection;
    private int nextDirection = 1;
    private float targetX;

    public EnemyWanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
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

        wanderDirection = nextDirection;  

        enemy.CheckIfShouldFlip(wanderDirection);
        
        var maxAllowedDistance = (enemyData.startingPosition.x + enemy.wanderRange * wanderDirection - enemy.transform.position.x) * wanderDirection;

        float distance;
        if (enemyData.wanderDistanceMax >= maxAllowedDistance)
        {
            nextDirection *= -1;
            if (enemyData.wanderDistanceMin > maxAllowedDistance) distance = maxAllowedDistance;
            else distance = Random.Range(enemyData.wanderDistanceMin, maxAllowedDistance);
        }
        else
        {
            nextDirection *= Random.Range(1, 3) == 1 ? 1 : -1;
            distance = Random.Range(enemyData.wanderDistanceMin, enemyData.wanderDistanceMax);
        }

        targetX = enemy.transform.position.x + distance * wanderDirection;
    }

    public override void Exit()
    {
        base.Exit();
    }

    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (wanderDirection==1 && enemy.transform.position.x >= targetX || wanderDirection == -1 && enemy.transform.position.x <= targetX)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
        else
        {
            enemy.SetVelocityX(enemyData.movementVelocity * wanderDirection);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
