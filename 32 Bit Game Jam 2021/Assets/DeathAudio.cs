using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

    public void OnDeath()
    {
        source.PlayOneShot(clip);
    }
}
