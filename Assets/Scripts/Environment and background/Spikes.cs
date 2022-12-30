using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage;

    public ParticleSystem particle;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D box_collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.GetComponent<Player>();
            if (player.StateMachine.CurrentState == player.inAirState)
            {
                player.GetDamaged(damage, box_collider);
                StartCoroutine(Destruct());
            }
        }
    }

    private IEnumerator Destruct()
    {
        particle.Play();
        spriteRenderer.enabled = false;
        box_collider.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);

        Destroy(gameObject);
    }
}
