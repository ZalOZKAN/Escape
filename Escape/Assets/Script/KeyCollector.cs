using UnityEngine;
using UnityEngine.SceneManagement;

public class AnahtarToplama : MonoBehaviour
{
    public int anahtarSayisi = 0; // Anahtar say�s�n� takip etmek i�in de�i�ken
    public GameObject kap�; // Kap� objesini tan�mlamak i�in de�i�ken
    public string sonrakiSahne; // Sonraki sahneye ge�mek i�in sahne ad�

    void Update()
    {
        // 2 metre mesafe ile anahtarlar� toplama
        foreach (GameObject anahtar in GameObject.FindGameObjectsWithTag("Key"))
        {
            float mesafe = Vector3.Distance(transform.position, anahtar.transform.position);
            if (mesafe <= 2f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anahtarSayisi++;
                    Destroy(anahtar); // Anahtar� yok et
                }
            }
        }

        // Toplamda 5 anahtara ula�t���nda kap� �n�nde "E" tu�una bas�ld���nda sonraki sahneye ge�
        if (anahtarSayisi == 5 && Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, kap�.transform.position) <= 3f)
        {
            LoadNextScene();
        }



    }
    void LoadNextScene()
    {
        // Mevcut sahne indeksini al
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Bir sonraki sahne indeksine ge�
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}