using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public enum SoundForState
{
    FootStep,
    Stunned,
    Land,
    Damaged,
    Ladder,
    Attack,
    Dash,
    Pick,
    Key
}
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips;
    private float random;


    public void SoundEffect(SoundForState sound, bool pitchChange) {
        if (pitchChange)
        {
            random = Random.Range(0.85f, 1.1f);
            _audioSource.pitch = random;
        }
        else {
            _audioSource.pitch = 1.0f;
        }
       
        _audioSource.clip = _clips[(int)sound];
        _audioSource.Play();
    }
}
