using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyPool : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;
    private float fadeTime = 1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !source.isPlaying)
        {
            StartCoroutine(AudioHelper.FadeIn(source, fadeTime));
            source.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(AudioHelper.FadeOut(source, fadeTime));
            source.Stop();
        }
    }
}
