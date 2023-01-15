using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlockState : EnemyOnGroundState
{
    private float timerStart = 0f;

    public EnemyBlockState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityX(0f);

        startTimer();
    }

    private void startTimer()
    {
        timerStart = Time.time + enemyData.blockExitDelay;
    }

    public override void StateFunction()
    {
        if(enemyData.counterAttack) stateMachine.ChangeState(enemy.AttackState);
        else stateMachine.ChangeState(enemy.ChaseState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        var playerObject = enemyData.playerCollider.gameObject;

        var playerDirection = enemy.transform.position.x > playerObject.transform.position.x ? -1 : 1;
        enemy.CheckIfShouldFlip(playerDirection);

        if (Time.time >= timerStart)
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
