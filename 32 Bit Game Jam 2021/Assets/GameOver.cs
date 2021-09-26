using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;

    private void OnEnable()
    {
        EventManager.playerDiedEvent += GameOverRoutine;
    }

    private void OnDisable()
    {
        EventManager.playerDiedEvent += GameOverRoutine;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverRoutine()
    {
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }
}
