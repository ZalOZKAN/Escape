using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public int mainMenuIndex = 0;
    [SerializeField] private GameObject youwon;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EndGameSequence());
        }
    }

    private IEnumerator EndGameSequence()
    {
        youwon.SetActive(true);
        Time.timeScale = 0; // Pause the game
        yield return new WaitForSecondsRealtime(2); // Wait for 2 seconds in real time
        Time.timeScale = 1; // Resume the game

        Debug.Log("Oyun Bitti");
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        SceneManager.LoadScene(mainMenuIndex); // Load the main menu scene
    }
}
