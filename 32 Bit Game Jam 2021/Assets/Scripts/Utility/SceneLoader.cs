using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private AudioClip gameplayMusic;
    [SerializeField]
    private AudioClip menuMusic;
    public AudioMixer masterMixer;

    public void LoadGameplayTestbed()
    {
        masterMixer.SetFloat("musicCutoff", 22000);
        AudioManager.Instance.PlayMusic(gameplayMusic);
        SceneManager.LoadScene("Gameplay Testbed");
        
    }

    public void LoadMainMenu()
    {
        masterMixer.SetFloat("musicCutoff", 22000);
        AudioManager.Instance.PlayMusic(menuMusic);
        SceneManager.LoadScene("Main Menu");
    }
}
