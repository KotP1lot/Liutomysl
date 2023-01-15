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

        if (!enemy.CanAct() || !enemy.CheckDetection()) stateMachine.ChangeState(enemy.ChaseState);

        

        if(enemy.playerCollider != null)
        {
            var playerObject = enemy.playerCollider.gameObject;
            var playerDirection = enemy.transform.position.x > playerObject.transform.position.x ? -1 : 1;
            enemy.CheckIfShouldFlip(playerDirection);
        }
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityX(0f);
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    private void Shoot()
    {
        var actualShootPoint = enemy.transform.position + new Vector3(enemyData.shootPoint.x * enemy.FacingDirection, enemyData.shootPoint.y, 0);
        var bulletObject = GameObject.Instantiate(enemyData.bulletPrefab, actualShootPoint, Quaternion.identity);

        var bullet = bulletObject.GetComponent<EnemyBullet>();
        bullet.Init(enemy.playerCollider.transform.position, enemyData.bulletVelocity, enemyData.damage);

        enemy.soundEnControler.SoundEffect(SoundForEnState.Fireball, true);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        Shoot();
    }
}
