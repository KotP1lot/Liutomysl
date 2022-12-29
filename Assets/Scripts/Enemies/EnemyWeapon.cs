using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private Collider2D weaponCollider;
    public Collider2D enemyCollider;
    void Start()
    {
        weaponCollider = GetComponent<Collider2D>();
    }

    public delegate void AttackHitHandler(Collider2D sender,Collider2D colliderHit);
    public event AttackHitHandler AttackHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.layer==6)
        {
            weaponCollider.enabled = false;
            AttackHit?.Invoke(enemyCollider, collision);
        }
    }
}
