using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region State Variables
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyWanderState WanderState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyReturnState ReturnState { get; private set; }
    public EnemyJumpState JumpState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyShootState ShootState { get; private set; }
    public EnemyDamagedState DamagedState { get; private set; }
    public EnemyBlockState BlockState { get; private set; }
    public EnemyActState ActState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }

    public string currentState;

    [SerializeField]
    private EnemyData enemyData;
    #endregion

    #region Other Variables
    public Rigidbody2D RB { get; private set; }
    public Collider2D enemyCollider { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public SoundEnControler soundEnControler { get; private set; }
 
    public Ray2D jumpRay = new Ray2D();

    private Vector2 workspace;

    public Collider2D attackCollider;

    public Animator Animator;

    public EnemyAlerts alerts;

    [Header("Ranges")]
    public float wanderRange=8;
    public float patrolRange=8;
    public float shootRange=16;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle");
        WanderState = new EnemyWanderState(this, StateMachine, enemyData, "chase");
        ChaseState = new EnemyChaseState(this, StateMachine, enemyData, "chase");
        ReturnState = new EnemyReturnState(this, StateMachine, enemyData, "chase");
        JumpState = new EnemyJumpState(this, StateMachine, enemyData, "jump");
        AttackState = new EnemyAttackState(this, StateMachine, enemyData, "attack");
        PatrolState = new EnemyPatrolState(this, StateMachine, enemyData, "chase");
        ShootState = new EnemyShootState(this, StateMachine, enemyData, "shoot");
        DamagedState = new EnemyDamagedState(this, StateMachine, enemyData, "damaged");
        BlockState = new EnemyBlockState(this, StateMachine, enemyData, "block");
        ActState = new EnemyActState(this, StateMachine, enemyData, "act");
        DeathState = new EnemyDeathState(this, StateMachine, enemyData, "death");

        enemyData.continueChasing = false;
        enemyData.startingPosition = transform.position;
    }
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        StateMachine.Initialize(IdleState);
        FacingDirection = 1;
        attackCollider.enabled = false;
        enemyData.HP = enemyData.maxHP;
        soundEnControler = GetComponent<SoundEnControler>();
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();

        currentState = StateMachine.CurrentState.GetType().Name;
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            FlipCharacter();
        }
    }
    private void FlipCharacter()
    {
        FacingDirection *= -1;

        transform.Rotate(0.0f, 180.0f, 0, 0f);

        alerts.Flip(FacingDirection);
    }
    public void SetVelocityX(float velocity)
    {
        if (velocity == 0) RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else { CheckIfShouldFlip(velocity < 0 ? -1 : 1); RB.constraints = RigidbodyConstraints2D.FreezeRotation; }

        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    
    public bool CheckDetection()
    {
        var collider = Physics2D.OverlapBox(transform.position+enemyData.detectOffset * FacingDirection,enemyData.detectSize,0,enemyData.playerMask);

        if (collider != null)
        {
            enemyData.playerCollider = collider;
            return true;
        }
        enemyData.playerCollider = null;
        return false;
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapBox(transform.position + enemyData.groundCheckPosition, enemyData.groundCheckSize, 0, enemyData.groundMask);
    }

    public bool CheckIfNeedsToJump()
    {
        if (CheckIfGrounded())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(FacingDirection, 0), enemyData.jumpRayLength, enemyData.groundMask);

            return hit && hit.collider.tag=="EnemyJump";
        }
        return false;
    }

    public bool CanAct()
    {
        if (CheckIfGrounded() && enemyData.useShootAsActRange && enemyData.canShoot)
        {
            return Physics2D.OverlapCircle(transform.position, shootRange, enemyData.playerMask);
        }
        if (CheckIfGrounded())
        {
            return Physics2D.OverlapCircle(transform.position, enemyData.actRange, enemyData.playerMask);
        }
        return false;
    }

    public bool CanShoot()
    {
        if (CheckIfGrounded() && enemyData.canShoot)
        {
            return Physics2D.OverlapCircle(transform.position, shootRange, enemyData.playerMask);
        }
        return false;
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public void EnableAttackCollider(int binaryBool) { StateMachine.CurrentState.AnimationTrigger(binaryBool == 1 ? true : false); }
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void GetDamaged(int amount, Collider2D sender)
    {
        if (StateMachine.CurrentState == BlockState)
        {
            if(CheckIfGrounded()) GetKnockedBack(sender.transform.position.x > transform.position.x ? -1 : 1, enemyData.knockbackForce/3);
            StateMachine.CurrentState.StateFunction();

            var player = sender.GetComponent<Player>();
            player.GetStunned(enemyCollider);
        }
        else if (StateMachine.CurrentState != DeathState)
        {
            if (CheckIfGrounded()) GetKnockedBack(sender.transform.position.x > transform.position.x ? -1 : 1, enemyData.knockbackForce);
            enemyData.continueChasing = true;

            enemyData.HP -= amount;
            if (enemyData.HP <= 0)
            {
                enemyData.HP = 0;

                StateMachine.ChangeState(DeathState);
                return;
            }

            StateMachine.ChangeState(DamagedState);
        }
    }
    public void GetKnockedBack(int direction, float force)
    {
        SetVelocityX(0f);
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;

        RB.AddForce(new Vector2(force * direction, 0), ForceMode2D.Impulse);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (enemyData.showGizmos)
        {
            if (enemyData.canWander)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(enemyData.startingPosition, wanderRange);
            }
            if (enemyData.detectGizmos)
            {
                Gizmos.color = Color.magenta;

                Gizmos.DrawWireCube(transform.position + enemyData.detectOffset * FacingDirection, enemyData.detectSize);
            }
            if (enemyData.actGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, enemyData.actRange);
            }
            if (enemyData.jumpGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(transform.position, new Vector2(enemyData.jumpRayLength * FacingDirection, 0));

                Gizmos.DrawWireCube(transform.position + enemyData.groundCheckPosition, enemyData.groundCheckSize);
            }
            if (enemyData.canPatrol)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(new Vector2(enemyData.startingPosition.x - patrolRange, enemyData.startingPosition.y - 0.5f),
                    new Vector2(enemyData.startingPosition.x + patrolRange, enemyData.startingPosition.y - 0.5f));
            }
            if (enemyData.canShoot)
            {
                Gizmos.color = Color.cyan;
                var actualShootPoint = new Vector3(enemyData.shootPoint.x * FacingDirection, enemyData.shootPoint.y, 0);
                Gizmos.DrawWireSphere(transform.position + actualShootPoint, 0.2f);
                Gizmos.DrawWireSphere(transform.position, shootRange);
            }
        }
    }
}
