using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region State Variables
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }

    [SerializeField]
    private EnemyData enemyData;
    #endregion
    
    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        // IdelState = new PlayerIdelState(this, StateMachine, playerData, "idle"); Приклад
        IdleState = new EnemyIdleState(this, StateMachine, enemyData, "idle");
    }
    private void Start()
    {
        //RB = GetComponent<Rigidbody2D>();
        //Animator = GetComponent<Animator>();
        //InputHandler = GetComponent<PlayerInputHandler>();
        //PlayerCollider = GetComponent<Collider2D>();
        StateMachine.Initialize(IdleState);
        //FacingDirection = 1;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
}
