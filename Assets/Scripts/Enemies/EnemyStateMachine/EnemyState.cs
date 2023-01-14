using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    protected bool isAnimationFinished;

    protected float startTime;

    protected string animBoolName;

    protected bool autoAnimStart;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
        autoAnimStart = true;
    }

    public virtual void Enter()
    {
        DoChecks();

        if (autoAnimStart) enemy.Animator.SetBool(animBoolName, true);

        startTime = Time.time;
        isAnimationFinished = false;
        Debug.Log("enemy - " + animBoolName);
    }

    public virtual void Exit()
    {
        enemy.Animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        enemy.Animator.SetFloat("velocityX", Mathf.Abs(enemy.CurrentVelocity.x));
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationTrigger(bool something)
    {

    }

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }

    public virtual void StateFunction()
    {

    }
}