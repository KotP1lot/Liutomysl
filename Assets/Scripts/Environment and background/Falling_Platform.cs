using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{
    public ParticleSystem particleBreaking;
    public ParticleSystem particleBroke;
    public GameObject spriteObject;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public BoxCollider2D boxCollider;
    [SerializeField] private AudioClip _clipClose;
    [SerializeField] private AudioSource _audioSource;
    private void Start()
    {
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        animator = spriteObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(startBreaking());
        }
    }

    private IEnumerator startBreaking()
    {
        particleBreaking.Play();
        animator.SetBool("Breaking", true);
        _audioSource.clip = _clipClose;
        _audioSource.volume = 0.3f;
        _audioSource.loop = true;
        _audioSource.Play();
        yield return new WaitForSeconds(1);

        particleBreaking.Stop();
        StartCoroutine(Destruct());
    }

    private IEnumerator Destruct()
    {
        particleBroke.Play();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;

        yield return new WaitForSeconds(particleBroke.main.startLifetime.constantMax);

        Destroy(gameObject);
    }
}
