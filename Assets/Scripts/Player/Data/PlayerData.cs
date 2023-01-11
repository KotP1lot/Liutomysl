using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
    public float movementVelocityOnLadder = 5f;
    public float dashVelocity = 20f;
    public int dashStaminaCost;
    public float stepForce;
    public float knockbackForce;

    [Header("Jump State")]
    public float jumpVelocity = 10f;

    [Header("In Air State")]
    public float variableJumpHeightMultiplier = 0.5f;
    public int fallDamageModifier = 10;

    [Header("Check Ground Box Size")]
    public Vector2 groundCheckSize;
    public float ladderCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform;
    public LayerMask whatIsLadder;

    [Header("Combat info")]
    public int maxHP;
    [HideInInspector] public int HP;
    public int maxSP;
    [HideInInspector] public int SP;
    public float spRegenDelayMin;
    public float spRegenDelayMax;
    public int spRegenAmount;
    public float spRegenSpeed;
    
    [Header("Light Attack 1")]
    public int LightDamage1;
    public int LightCost1;
    [Header("Light Attack 2")]
    public int LightDamage2;
    public int LightCost2;
    [Header("Light Attack 3")]
    public int LightDamage3;
    public int LightCost3;
    [Header("Strong Attack 1")]
    public int StrongDamage1;
    public int StrongCost1;
    [Header("Strong Attack 2")]
    public int StrongDamage2;
    public int StrongCost2;
    [Header("Strong Attack 3")]
    public int StrongDamage3;
    public int StrongCost3;
}
