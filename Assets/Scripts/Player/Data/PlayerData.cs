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

    [Header("Jump State")]
    public float jumpVelocity = 10f;

    [Header("In Air State")]
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Check Ground Box Size")]
    public Vector2 groundCheckSize;
    public float ladderCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsLadder;

    [Header("Default Ñharacteristics Data")]
    public float defaultHealthPoint = 100;
    public float defaultEndurancePoint = 100;
    public float HPGrowth = 0.1f;
    public float EnduranceGrowth = 0.1f;
}
