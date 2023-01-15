using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnState : EnemyOnGroundState
{
    private int returnDirection;
    private float timerStart = 0f;

    public EnemyReturnState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
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

        enemyData.continueChasing = false;

        enemy.alerts.LostAlert();

        enemy.SetVelocityX(0f);

        returnDirection = enemy.transform.position.x > enemy.startingPosition.x ? -1 : 1;

        startTimer();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.walkParticle.Stop();
    }

    private void startTimer()
    {
        timerStart = Time.time + enemyData.waitBeforeReturn;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= timerStart)
        {
            if (!enemy.walkParticle.isPlaying) enemy.walkParticle.Play();

            if (returnDirection == 1 && enemy.transform.position.x >= enemy.startingPosition.x
            || returnDirection == -1 && enemy.transform.position.x <= enemy.startingPosition.x)
            {
                stateMachine.ChangeState(enemy.IdleState);
            }
            else
            {
                enemy.SetVelocityX(enemyData.movementVelocity * returnDirection);
            }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
