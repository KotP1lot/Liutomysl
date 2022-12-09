using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    protected bool isAnimationFinished;

    protected float startTime;

    private string aminBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string aminBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.aminBoolName = aminBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        //  enemy.Animator.SetBool(aminBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        Debug.Log(aminBoolName);
    }

    public virtual void Exit()
    {
        //  enemy.Animator.SetBool(aminBoolName, false);
    }

    public virtual void LogicUpdate()
    {

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

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }
}