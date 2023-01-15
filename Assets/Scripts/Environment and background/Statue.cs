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
    [SerializeField] private AudioClip _clipClose;
    [SerializeField] private AudioSource _audioSource;

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
        _audioSource.PlayOneShot(_clipClose);
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax + 0.5f);

        Destroy(gameObject);
    }
}
