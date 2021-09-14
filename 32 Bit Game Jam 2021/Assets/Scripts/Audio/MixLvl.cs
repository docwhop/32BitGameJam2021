using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLvl : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetSfxLvl(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", Mathf.Log10 (sfxLvl) *20);
    }
    
    public void SetMusicVol (float musicLvl)
    {
        masterMixer.SetFloat("musicVol", Mathf.Log10 (musicLvl) *20);
    }

}
