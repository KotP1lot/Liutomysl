using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyOnGroundState
{
    private GameObject playerObject;
    private int playerDirection;
    private float timerStart = 0f;
    private float returnTimerStart = 0f;
    private bool returnTimerStarted = false;

    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (!returnTimerStarted && !enemy.CheckDetection()) startReturnTimer();
        if (returnTimerStarted && enemy.CheckDetection()) returnTimerStarted = false;
        if (returnTimerStarted && Time.time >= returnTimerStart) stateMachine.ChangeState(enemy.ReturnState);

        if (enemy.CheckIfNeedsToJump()) { stateMachine.ChangeState(enemy.JumpState); enemyData.continueChasing = true; }

        if (enemy.CanAct()) { stateMachine.ChangeState(enemy.ActState); enemyData.continueChasing = true; }
    }

    public override void Enter()
    {
        base.Enter();

        if (enemyData.playerCollider == null) { stateMachine.ChangeState(enemy.ReturnState); return; }
        playerObject = enemyData.playerCollider.gameObject;

        enemy.SetVelocityX(0f);

        playerDirection = enemy.transform.position.x > playerObject.transform.position.x ? -1 : 1;
        enemy.CheckIfShouldFlip(playerDirection);

        if (!enemyData.continueChasing) {  startTimer(); }
    }

    private void startTimer()
    {
        enemy.alerts.DetectedAlert();
        timerStart = Time.time + enemyData.waitBeforeChase;
    }

    private void startReturnTimer()
    {
        returnTimerStarted = true;
        returnTimerStart = Time.time + enemyData.chaseBeforeReturnFor;
    }

    public override void Exit()
    {
        base.Exit();

        returnTimerStarted = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= timerStart)
        {
            if (!returnTimerStarted) playerDirection = enemy.transform.position.x > playerObject.transform.position.x ? -1 : 1;

            enemy.SetVelocityX(enemyData.movementVelocity * playerDirection);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
