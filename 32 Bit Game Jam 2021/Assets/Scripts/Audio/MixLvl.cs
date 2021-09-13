using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLvl : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetSfxLvl(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);
    }
    
    public void SetMusicVol (float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
    }

}
