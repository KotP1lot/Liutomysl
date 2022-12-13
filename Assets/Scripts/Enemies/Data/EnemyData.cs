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

    [Header("Layer Masks")]
    public LayerMask groundMask;
    public LayerMask playerMask;

    
    [Header("Combat info")]
    public int health;
    public int damage;
    public float attackRange;
    public float waitAfterAttack;
    public Vector2 detectSize;
    public Vector3 detectOffset;
    [HideInInspector] public Collider2D playerCollider;

    [Header("Chase State")]
    public float waitBeforeChase;
    public float chaseBeforeReturnFor;
    [HideInInspector] public bool continueChasing;

    [Header("Wander State")]
    public bool canWander;
    public float wanderRange;
    public float wanderDistanceMin;
    public float wanderDistanceMax;
    public float idleTimeMin;
    public float idleTimeMax;

    [Header("Return State")]
    public float waitBeforeReturn;

    [Header("Gizmos")]
    public bool wanderGizmos;
    public bool detectGizmos;
    public bool attackGizmos;
    public bool jumpGizmos;
}
