using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 20.0f; // 3 dakika
    private float timer;
    private bool isPlayerAtExit = false;
    public TMP_Text timerText; // TextMeshPro referansý
    public int mainMenuIndex = 0;
    [SerializeField] private GameObject gameover;


    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        if (timer > 0 && !isPlayerAtExit)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timer <= 0)
            {
                if (!isPlayerAtExit)
                {
                    GameOver();
                }
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void GameOver()
    {
        gameover.SetActive(true);
        new WaitForSecondsRealtime(2); // Wait for 2 seconds in real time
        Debug.Log("Oyun Bitti");
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        SceneManager.LoadScene(mainMenuIndex);

    }


    public void PlayerReachedExit()
    {
        isPlayerAtExit = true;
        Debug.Log("Tebrikler! Çýkýþ kapýsýna ulaþtýnýz.");
        // Burada çýkýþa ulaþtýðýnda ne olmasýný istiyorsan onu yapabilirsin.
    }
}
