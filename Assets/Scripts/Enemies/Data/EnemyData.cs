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
    [HideInInspector] public Vector3 startingPosition;
    public float jumpRayLength;
    public float fallDelay;
    public Vector3 groundCheckPosition;
    public Vector2 groundCheckSize;

    [Header("\nLayer Masks")]
    public LayerMask groundMask;
    public LayerMask playerMask;

    
    [Header("\nCombat info")]
    public int health;
    public int damage;
    public float attackRange;
    public float waitAfterAttack;
    public Vector2 detectSize;
    public Vector3 detectOffset;
    [HideInInspector] public Collider2D playerCollider;

    [Header("\nChase State")]
    public float waitBeforeChase;
    public float chaseBeforeReturnFor;
    [HideInInspector] public bool continueChasing;

    [Header("\nIdle State")]
    public float idleTimeMin;
    public float idleTimeMax;

    [Header("\nWander State")]
    public bool canWander;
    [ConditionalField("canWander")] public float wanderRange;
    [ConditionalField("canWander")] public float wanderDistanceMin;
    [ConditionalField("canWander")] public float wanderDistanceMax;

    [Header("\nPatrol State")]
    public bool canPatrol;
    [ConditionalField("canPatrol")] public float patrolRange;

    [Header("\nShoot State")]
    public bool canShoot;
    [ConditionalField("canShoot")] public GameObject bulletPrefab;
    [ConditionalField("canShoot")] public float shootRange;
    [ConditionalField("canShoot")] public Vector3 shootPoint;
    [ConditionalField("canShoot")] public float bulletVelocity;
    [ConditionalField("canShoot")] public float waitBeforeShoot;

    [Header("\nReturn State")]
    public float waitBeforeReturn;

    [Header("\nGizmos")]
    public bool wanderGizmos;
    public bool patrolGizmos;
    public bool detectGizmos;
    public bool attackGizmos;
    public bool jumpGizmos;
    public bool shootGizmos;
}
