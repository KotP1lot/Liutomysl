using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Movement info")]
    public float movementVelocity;
    public float jumpVelocity;
    //[HideInInspector] public Vector3 startingPosition;
    public float jumpRayLength;
    public float fallDelay;
    public Vector3 groundCheckPosition;
    public Vector2 groundCheckSize;
    public float stepForce;
    public float knockbackForce;

    [Header("\nLayer Masks")]
    public LayerMask groundMask;
    public LayerMask playerMask;

    
    [Header("\nCombat info")]
    public int maxHP;
    //[HideInInspector] public int HP;
    public int damage;
    public Vector2 detectSize;
    public Vector3 detectOffset;
    //[HideInInspector] public Collider2D playerCollider;

    [Header("\nChase State")]
    public float waitBeforeChase;
    public float chaseBeforeReturnFor;
    [HideInInspector] public bool continueChasing;

    [Header("\nIdle State")]
    public float idleTimeMin;
    public float idleTimeMax;

    [Header("\nReturn State")]
    public float waitBeforeReturn;

    [Header("\nWander State")]
    public bool canWander;
    //[ConditionalField("canWander")] public float wanderRange;
    [ConditionalField("canWander")] public float wanderDistanceMin;
    [ConditionalField("canWander")] public float wanderDistanceMax;

    [Header("\nPatrol State")]
    public bool canPatrol;
    //[ConditionalField("canPatrol")] public float patrolRange;

    [Header("\nAct State")]
    public float actRange;

    [Header("\nAttack State")]
    public bool canAttack;

    [Header("\nShoot State")]
    public bool canShoot;
    [ConditionalField("canShoot")] public GameObject bulletPrefab;
    //[ConditionalField("canShoot")] public float shootRange;
    [ConditionalField("canShoot")] public bool useShootAsActRange;
    [ConditionalField("canShoot")] public Vector3 shootPoint;
    [ConditionalField("canShoot")] public float bulletVelocity;

    [Header("\nBlock State")]
    public bool canBlock;
    [ConditionalField("canBlock")] public bool counterAttack;
    [ConditionalField("canBlock")] public float blockExitDelay;

    [Header("\nGizmos")]
    public bool showGizmos;
    [ConditionalField("showGizmos")] public bool detectGizmos;
    [ConditionalField("showGizmos")] public bool jumpGizmos;
    [ConditionalField("showGizmos")] public bool actGizmos;
}
