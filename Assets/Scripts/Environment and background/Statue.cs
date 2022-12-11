using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public ParticleSystem particle;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public float delay = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            StartCoroutine(Destruct());
        }
    }

    private IEnumerator Destruct()
    {
        yield return new WaitForSeconds(delay);
        particle.Play();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);

        Destroy(gameObject);
    }
}
