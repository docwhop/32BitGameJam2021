using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

    public void OnDamage()
    {
        source.PlayOneShot(clip);
    }
}
