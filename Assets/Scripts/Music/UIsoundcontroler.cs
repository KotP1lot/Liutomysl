using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIsoundcontroler : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_Click;
    [SerializeField] private AudioClip m_MouseOver;

    private void Start()
    {
        MouseOver.onMouseOver += OnMouseOverSound;   
    }
    public void OnClickSound()
    {
        m_AudioSource.PlayOneShot(m_Click);
    }

    public void OnMouseOverSound() 
    {
        m_AudioSource.PlayOneShot(m_MouseOver);
    }
}
