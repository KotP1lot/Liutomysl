using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;

    protected float startTime;

    private string aminBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aminBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.aminBoolName = aminBoolName;
    }

    public virtual void Enter() 
    {
        DoChecks();
      //  player.Animator.SetBool(aminBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        //Debug.Log(aminBoolName);
    }

    public virtual void Exit() 
    {
      //  player.Animator.SetBool(aminBoolName, false);
    }

    public virtual void LogicUpdate() 
    {
    
    }

    public virtual void PhysicsUpdate() 
    {
        DoChecks();
    }

    public  virtual void DoChecks()
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
