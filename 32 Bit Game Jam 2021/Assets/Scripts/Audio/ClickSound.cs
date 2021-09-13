using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource myAudio;
    public AudioClip clickAudio;

    public void OnClick()
    {   
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
    }
}
