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

        enemy.spriteRenderer1.enabled = false; //temp
        enemy.spriteRenderer2.enabled = true; //temp

        enemy.SetVelocityX(0f);

        returnDirection = enemy.transform.position.x > enemyData.startingPosition.x ? -1 : 1;

        startTimer();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.spriteRenderer2.enabled = false; //temp
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
            enemy.spriteRenderer2.enabled = false; //temp

            if (returnDirection == 1 && enemy.transform.position.x >= enemyData.startingPosition.x
            || returnDirection == -1 && enemy.transform.position.x <= enemyData.startingPosition.x)
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
