using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyOnGroundState
{
    private EnemyWeapon weapon;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
        weapon = enemy.attackCollider.GetComponent<EnemyWeapon>();
        weapon.AttackHit += dealDamage;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocityX(0f);
    }

    public void dealDamage(Collider2D sender, Collider2D colliderHit)
    {
        var player = colliderHit.GetComponent<Player>();

        player.GetDamaged(enemyData.damage, sender);
    }

    public override void AnimationTrigger()
    {
        enemy.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        enemy.RB.AddForce(new Vector2(enemyData.stepForce * enemy.FacingDirection, 0), ForceMode2D.Impulse);
    }

    public override void AnimationTrigger(bool coliderEnable)
    {
        base.AnimationTrigger(coliderEnable);

        enemy.attackCollider.enabled = coliderEnable;
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        stateMachine.ChangeState(enemy.ChaseState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
