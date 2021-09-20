using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public AudioMixer masterMixer;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Escape key pressed");
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else if (pausePanel.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
                ContinueGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        pausePanel.SetActive(true);
        masterMixer.SetFloat("musicCutoff", 1900);
        masterMixer.SetFloat("sfxCutoff", 0);
        GameManager.Instance.IsGamePaused = true;
        //Disable scripts that still work while timescale is set to 0
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        masterMixer.SetFloat("musicCutoff", 22000);
        masterMixer.SetFloat("sfxCutoff", 22000);
        GameManager.Instance.IsGamePaused = false;

        //enable the scripts again
    }

    public void PauseMenuToMainMenu()
    {


       
    }
}
