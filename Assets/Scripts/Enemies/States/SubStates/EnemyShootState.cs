using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootState : EnemyOnGroundState
{
    public EnemyShootState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (!enemy.CanShoot() || !enemy.CheckDetection()) stateMachine.ChangeState(enemy.ChaseState);

        var playerObject = enemyData.playerCollider.gameObject;
        var playerDirection = enemy.transform.position.x > playerObject.transform.position.x ? -1 : 1;
        enemy.CheckIfShouldFlip(playerDirection);
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityX(0f);

        startTimer();
    }

    private float timerStart = 0f;
    private void startTimer()
    {
        timerStart = Time.time + enemyData.waitBeforeShoot;
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
            Shoot();
            startTimer();
        }
    }

    private void Shoot()
    {
        var actualShootPoint = enemy.transform.position + new Vector3(enemyData.shootPoint.x * enemy.FacingDirection, enemyData.shootPoint.y, 0);
        var bulletObject = GameObject.Instantiate(enemyData.bulletPrefab, actualShootPoint, Quaternion.identity);

        var bullet = bulletObject.GetComponent<EnemyBullet>();
        bullet.Init(enemyData.playerCollider.transform.position, enemyData.bulletVelocity, enemyData.damage);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
