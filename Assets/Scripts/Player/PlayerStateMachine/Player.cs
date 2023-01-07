using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdelState IdelState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerLadderClimbState LadderClimbState { get; private set; }
    public PlayerLadderGrabState LadderGrabState { get; private set; }
    public PlayerDashState DashState {get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDamagedState DamagedState { get; private set; }
    public PlayerStunnedState StunnedState { get; private set; }

    [SerializeField]
    public PlayerData playerData;
    #endregion

    #region Components
    [SerializeField]
    public GameObject weapon;
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Collider2D PlayerCollider { get; private set; }

    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform ladderCheck;
    #endregion

    #region Other Variables

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;
    private bool JumpDownInput;

    [HideInInspector] public bool isDamaged;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdelState = new PlayerIdelState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        inAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        LadderGrabState = new PlayerLadderGrabState(this, StateMachine, playerData, "grabLadder");
        LadderClimbState = new PlayerLadderClimbState(this, StateMachine, playerData, "climbLadder");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        DamagedState = new PlayerDamagedState(this, StateMachine, playerData, "damaged");
        StunnedState = new PlayerStunnedState(this, StateMachine, playerData, "stunned");
    }
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerCollider = GetComponent<Collider2D>();
        StateMachine.Initialize(IdelState);
        FacingDirection = 1;

        playerData.HP = playerData.maxHP;

        weapon.SetActive(false);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
        SetLayerMask();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetLayerMask()
    {
        JumpDownInput = InputHandler.JumpDownInput;
        gameObject.layer = JumpDownInput ? LayerMask.NameToLayer("IgnoreCollision") : LayerMask.NameToLayer("Player");
    }
    public void SetVelocityX(float velocity)
    {
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
    #endregion

    #region Check Functions
    public bool CheckIfTouchingLadder()
    {
        return Physics2D.OverlapCircle(ladderCheck.position, playerData.ladderCheckDistance, playerData.whatIsLadder);
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, playerData.groundCheckSize, 0, playerData.whatIsGround);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            FlipCharacter();
        }
    }
    #endregion

    #region Other Fucntoins
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void FlipCharacter()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0, 0f);
    }

    public void GetDamaged(int amount, Collider2D sender=null)
    {
        if (amount > 0)
        {
            playerData.HP -= amount;
            Debug.Log($"Damaged by {amount}  |  HP: {playerData.HP}/{playerData.maxHP} ");
            // death?

            isDamaged = true;
            if (CheckIfGrounded() && sender != null) GetKnockedBack(sender.transform.position.x > transform.position.x ? -1 : 1);
            StateMachine.ChangeState(DamagedState);
        }
    }
    public void GetStunned(Collider2D sender)
    {
        isDamaged = true;
        if (CheckIfGrounded()) GetKnockedBack(sender.transform.position.x > transform.position.x ? -1 : 1);

        StateMachine.ChangeState(StunnedState);
    }
    public void GetKnockedBack(int direction)
    {
        SetVelocityX(0);

        RB.AddForce(new Vector2(playerData.knockbackForce * direction, 0), ForceMode2D.Impulse);
    }
    #endregion
}
