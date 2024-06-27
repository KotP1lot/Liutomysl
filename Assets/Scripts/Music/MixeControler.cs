using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Snap
{
    StartSnap,
    NormalSnap,
    StunSnap,
    PauseSnap,
    DeathSnap,
}

public enum Theme
{
    MainMenu,
    Pause,
    Forest,
    GoblinCity
}
public class MixeControler : MonoBehaviour
{
    [SerializeField] private Snap firstSnap;
    [SerializeField] private Snap secondSnap;
    [SerializeField] private Theme firstAudioClip;
    [SerializeField] private float startTransitionTime;

    public static MixeControler mixer { get; private set; }

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private List<AudioMixerSnapshot> snaps;

    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private List<AudioClip> backgroundThemes;
    private void Awake()
    {
        if (mixer != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }

        mixer = this;
    }
    public void ChangeSnap(Snap snap, float transitionTime)
    {
        AudioMixerSnapshot newSnap;
        newSnap = snaps[(int)snap];
        newSnap.TransitionTo(transitionTime);
    }
    public void ToNormalSnap()
    {
        ChangeSnap(Snap.NormalSnap, 1f);
    }
    public void ToPauseMenu() 
    {
        ChangeSnap(Snap.PauseSnap, 1f);
    }
    public void ToMainMenu()
    {
        ChangeSnap(Snap.DeathSnap, 2f);
    }
    public void StartGame()
    {
        ChangeSnap(Snap.StartSnap, 1f);
    }
    public void ChangeSound(Theme theme) {

        backgroundSource.clip = backgroundThemes[(int)theme];
        backgroundSource.Play();
    }

    public void Start()
    {
        ChangeSound(firstAudioClip);
        ChangeSnap(secondSnap, startTransitionTime);
    }
}
