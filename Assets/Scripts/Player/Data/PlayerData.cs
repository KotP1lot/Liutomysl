using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
    public float movementVelocityOnLadder = 5f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;

    [Header("In Air State")]
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Check Ground Box Size")]
    public Vector2 groundCheckSize;
    public float ladderCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsLadder;
}
