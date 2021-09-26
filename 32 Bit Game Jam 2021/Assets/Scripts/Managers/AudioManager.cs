using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }


    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource effectsSource;

    [SerializeField]
    private AudioSource randomSource;

    [SerializeField]
    private AudioSource uiSource;

    [SerializeField]
    private AudioSource explosionSource;

    public float LowPitchRange = 1f;
    public float HighPitchRange = 1.2f;
    public float LowVolumeRange = .8f;
    public float HighVolumeRange = 1;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // musicSource
    }

    public void PlayEffect(AudioClip clip)
    {
        effectsSource.clip = clip;
        effectsSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayUi(AudioClip clip)
    {
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        uiSource.pitch = randomPitch;
        uiSource.clip = clip;
        uiSource.Play();
    }
    public AudioSource RandomizePitchAndVolume(AudioSource inSource)
    {
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        float randomVolume = Random.Range(LowVolumeRange, HighVolumeRange);

        inSource.pitch = randomPitch;
        inSource.volume = randomVolume;

        return inSource;

    }

    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        randomSource.clip = clips[randomIndex];
        randomSource.Play();
    }

    public void StopRandomSource(params AudioClip[] clips)
    {
        randomSource.Stop();
    }
        
    public void PlayExplosion(AudioClip clip)
    {
        explosionSource.clip = clip;
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
