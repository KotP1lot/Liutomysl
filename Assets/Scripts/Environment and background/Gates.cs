using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public ParticleSystem dust;
    public Animator animator;
    [SerializeField] private AudioClip _clipClose;
    [SerializeField] private AudioSource _audioSource;
    private bool _isPlayed = false;
    public void playParticles()
    {
        dust.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isPlayed)
        {
            _audioSource.PlayOneShot(_clipClose);
            _isPlayed = true;
            animator.SetBool("Close", true);
        }
    }
}
