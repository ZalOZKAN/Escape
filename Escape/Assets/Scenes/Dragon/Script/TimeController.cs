using UnityEngine;

public class TimeController : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // 'P' tuþuna basýldýðýnda oyunu durdur veya devam ettir
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused =!isPaused;
        }
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    // Oyunu durdur veya devam ettir


    // Oyunu durdur
    void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    // Oyunu devam ettir
    void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}