using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyOnGroundState
{
    private EnemyWeapon weapon;
    private bool ignoreAnimFinish;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
        weapon = enemy.attackCollider.GetComponent<EnemyWeapon>();
        weapon.AttackHit += dealDamage;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        //if (enemy.CheckDetection()) stateMachine.ChangeState(enemy.ChaseState);
        //if (enemy.CheckIfNeedsToJump()) stateMachine.ChangeState(enemy.JumpState);
        //if ( (int)enemy.transform.position.x != (int)enemyData.startingPosition.x) stateMachine.ChangeState(enemy.ReturnState);
    }

    public override void Enter()
    {
        base.Enter();

        ignoreAnimFinish = false;

        enemy.SetVelocityX(0f);
    }

    public void dealDamage(Collider2D sender, Collider2D colliderHit)
    {
        // colliderHit.getDamaged(enemyData.damage)
        Debug.Log("Атака попала");

        ignoreAnimFinish = true;
        timerStarted = false;

        startExitTimer();
    }

    public override void AnimationTrigger(bool coliderEnable)
    {
        base.AnimationTrigger(coliderEnable);

        enemy.attackCollider.enabled = coliderEnable;
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        enemy.Animator.SetBool(animBoolName, false);

        if (!ignoreAnimFinish)
        {
            Debug.Log("Атака не попала");
            
        }
        startExitTimer();
    }

    private float timerStart = 0f;
    private bool timerStarted = false;

    private void startExitTimer()
    {
        timerStart = Time.time + enemyData.waitAfterAttack;
        timerStarted = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished && timerStarted && Time.time >= timerStart)
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
