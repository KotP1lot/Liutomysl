using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int health=1;
    public ParticleSystem hitParticle;
    public ParticleSystem breakParticle;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Collider2D boxCollider;

    public Sprite halfHealth;
    private int maxHealth;

    void Start()
    {
        maxHealth = health;
    }

    public void GetDamaged()
    {
        health -= 1;

        if (health <= 0)
        {
            
            StartCoroutine(Destruct());
            return;
        }

        if (halfHealth != null && health <= maxHealth / 2)
        {
            spriteRenderer.sprite = halfHealth;
        }

        hitParticle.Play();
        animator.SetTrigger("gotHit");
    }

    private IEnumerator Destruct()
    {
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        breakParticle.Play();

        yield return new WaitForSeconds(breakParticle.main.startLifetime.constantMax);

        Destroy(gameObject);
    }
}
