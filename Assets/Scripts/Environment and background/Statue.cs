using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public ParticleSystem particle;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody2d;
    public float delay = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            StartCoroutine(Destruct());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
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
