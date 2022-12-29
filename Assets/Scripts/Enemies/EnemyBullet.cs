using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 target;
    public float speed;
    public int damage;
    public Vector3 movementVector;

    public void Init(Vector3 target, float speed, int damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;

        movementVector= (this.target - transform.position).normalized * this.speed; 
    }

    void Update()
    {
        transform.position += movementVector * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.GetDamaged(damage, GetComponent<Collider2D>());
        }
        Destroy(gameObject);
    }
}
