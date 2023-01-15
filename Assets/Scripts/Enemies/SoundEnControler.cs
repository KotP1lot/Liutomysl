using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public enum SoundForEnState
{
   Fireball,
   DamagedIron,
}
public class SoundEnControler : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips;
    private float random;


    public void SoundEffect(SoundForEnState sound, bool pitchChange)
    {

        if (pitchChange)
        {
            random = Random.Range(.85f, 1.1f);
            _audioSource.pitch = random;
        }
        else
        {
            _audioSource.pitch = 1;
        }

        _audioSource.clip = _clips[(int)sound];
        _audioSource.Play();
    }
}
