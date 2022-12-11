using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public ParticleSystem particle;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D box_collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"  /*&& player state = in air */ )
        {
            //do damage
            StartCoroutine(Destruct());
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
