using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Player : MonoBehaviour, IDataPersistence
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
    public PlayerDeathState DeathState { get; private set; }

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

    public UI_Controller UI;
    public SceneTransition sceneTransition;

    private Vector3 lastSavePosition;
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
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death");
    }
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerCollider = GetComponent<Collider2D>();
        StateMachine.Initialize(IdelState);
        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
        SetLayerMask();

        if (InputHandler.pauseActive && !UI.pauseMenu.activeSelf)
        {
            MixeControler.mixer.ToPauseMenu();
            UI.ShowPauseMenu();
        }
        else if (!InputHandler.pauseActive && UI.pauseMenu.activeSelf)
        {
            MixeControler.mixer.ToNormalSnap();
            UI.HidePauseMenu();
        }
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
        if (velocity == 0) RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else { CheckIfShouldFlip(velocity < 0 ? -1 : 1); RB.constraints = RigidbodyConstraints2D.FreezeRotation; }

        if (playerData.SP == 0)
        {
            workspace.Set(velocity / 2, CurrentVelocity.y);
            Animator.SetFloat("SpeedMod", 0.5f);
        }
        else
        {
            workspace.Set(velocity, CurrentVelocity.y);
            Animator.SetFloat("SpeedMod", 1f);
        }
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
        return Physics2D.OverlapBox(groundCheck.position, playerData.groundCheckSize, 0, playerData.whatIsGround) 
            || Physics2D.OverlapBox(groundCheck.position, playerData.groundCheckSize, 0, playerData.whatIsPlatform);
    }
    public bool CheckIfOnlyOnGround()
    {
        return Physics2D.OverlapBox(groundCheck.position, playerData.groundCheckSize, 0, playerData.whatIsGround) && !Physics2D.OverlapBox(groundCheck.position, playerData.groundCheckSize, 0, playerData.whatIsPlatform);
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
            if (playerData.HP <= 0) 
            { 
                playerData.HP = 0;
                UI.HpBar.HideFill();

                StateMachine.ChangeState(DeathState);
                return;
            }

            UI.HpBar.SetValue(playerData.HP, playerData.maxHP);

            isDamaged = true;
            if (CheckIfGrounded() && sender != null) GetKnockedBack(sender.transform.position.x > transform.position.x ? -1 : 1, playerData.knockbackForceDamaged);
            StateMachine.ChangeState(DamagedState);
        }
    }
    public void GetStunned(Collider2D sender)
    {
        if (StateMachine.CurrentState == DamagedState) return;
        isDamaged = true;
        if (CheckIfGrounded()) GetKnockedBack(sender.transform.position.x > transform.position.x ? -1 : 1, playerData.knockbackForceStunned);

        StateMachine.ChangeState(StunnedState);
    }
    public void GetKnockedBack(int direction, float force)
    {
        SetVelocityX(0);
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        RB.AddForce(new Vector2(force * direction, 0), ForceMode2D.Impulse);
    }
    public void SpendStamina(int amount)
    {
        if (amount > 0)
        {
            StopAllCoroutines();

            playerData.SP -= amount;
            if (playerData.SP < 0) { playerData.SP = 0; }
            UI.StaminaBar.SetValue(playerData.SP, playerData.maxSP);

            StartCoroutine(RegenStamina());
        }
        
    }

    IEnumerator RegenStamina()
    {
        if (playerData.SP == 0)  yield return new WaitForSeconds(playerData.spRegenDelayMax);
        else yield return new WaitForSeconds(playerData.spRegenDelayMin);


        do
        {
            playerData.SP += playerData.spRegenAmount;
            
            UI.StaminaBar.SetValue(playerData.SP, playerData.maxSP);

            yield return new WaitForSeconds(playerData.spRegenSpeed);

        } while (playerData.SP < playerData.maxSP);
    }


    public void UpgradeHP()
    {
        playerData.maxHP += playerData.hpUpgrade;
        UI.HpBar.SetValue(playerData.HP, playerData.maxHP);
    }
    public void UpgradeSP()
    {
        playerData.maxSP += playerData.spUpgrade;
        playerData.SP = playerData.maxSP;  
        UI.StaminaBar.SetValue(playerData.SP, playerData.maxSP);
    }
    public void UpgradeDamage()
    {
        playerData.damage += playerData.damageUpgrade;
        UI.DamageBar.Upgrade();
    }
    public void UpgradeAtkSpd()
    {
        playerData.atkSpd += playerData.atkSpdUpgrade;

        UI.AtkSpdBar.Upgrade();
    }

    public void SetSavePosition(Vector3 position)
    {
        lastSavePosition = position;
    }

    public void LoadData(GameData data)
    {
        if (!data.axeAcquired) weapon.SetActive(false);
        lastSavePosition = data.lastSavePosition;
        transform.position = data.lastSavePosition;

        playerData.damage = data.playerDamage;
        playerData.atkSpd = data.playerAtkSpd;
        playerData.maxHP = data.playerMaxHP;
        playerData.maxSP = data.playerMaxSP;

        playerData.HP = playerData.maxHP;
        UI.HpBar.SetValue(playerData.HP, playerData.maxHP);
        playerData.SP = playerData.maxSP;
        UI.StaminaBar.SetValue(playerData.SP, playerData.maxSP);
        UI.DamageBar.SetValueFromPlayerData(playerData.damage, playerData.damageUpgrade);
        UI.AtkSpdBar.SetValueFromPlayerData(playerData.atkSpd, playerData.atkSpdUpgrade);
    }

    public void SaveData(ref GameData data)
    {
        data.lastSavePosition = lastSavePosition;
        data.playerDamage = playerData.damage;
        data.playerAtkSpd = playerData.atkSpd;
        data.playerMaxSP = playerData.maxSP;
        data.playerMaxHP = playerData.maxHP;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheck.position, playerData.groundCheckSize);
    }


}
