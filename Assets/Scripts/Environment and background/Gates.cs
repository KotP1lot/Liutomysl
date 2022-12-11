using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public ParticleSystem dust;
    public Animator animator;
    public void playParticles()
    {
        dust.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("Close", true);
    }
}
