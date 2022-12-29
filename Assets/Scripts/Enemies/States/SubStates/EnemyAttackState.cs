using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyOnGroundState
{
    private EnemyWeapon weapon;
    private bool ignoreAnimFinish;
    public string nextAttackAnim;
    private float timerStart = 0f;
    private bool timerStarted = false;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName) : base(enemy, stateMachine, enemyData, aminBoolName)
    {
        autoAnimStart = false;

        weapon = enemy.attackCollider.GetComponent<EnemyWeapon>();
        weapon.AttackHit += dealDamage;
        nextAttackAnim = "attack";
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        if (enemyData.hasTwoAttacks)
        {
            var rand = Random.Range(1, 3);

            switch (rand)
            {
                case 1: nextAttackAnim = animBoolName; break;
                case 2: nextAttackAnim = "attack2"; break;
            }
        }

        enemy.Animator.SetBool(nextAttackAnim, true);

        ignoreAnimFinish = false;
        isAnimationFinished = false;
        timerStarted = false;

        enemy.SetVelocityX(0f);
    }

    public void dealDamage(Collider2D sender, Collider2D colliderHit)
    {
        var player = colliderHit.GetComponent<Player>();

        player.GetDamaged(enemyData.damage, sender);

        ignoreAnimFinish = true;
        timerStarted = false;

        startExitTimer();
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

        enemy.Animator.SetBool(animBoolName, false);

        if (!ignoreAnimFinish)
        {
            Debug.Log("Атака не попала");
            
        }
        startExitTimer();
    }

    private void startExitTimer()
    {
        timerStart = Time.time + enemyData.waitAfterAttack;
        timerStarted = true;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.Animator.SetBool("attack2", false);
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
