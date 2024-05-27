using UnityEngine;
using UnityEngine.SceneManagement;

public class AnahtarToplama : MonoBehaviour
{
    public int anahtarSayisi = 0; // Anahtar sayýsýný takip etmek için deðiþken
    public GameObject kapý; // Kapý objesini tanýmlamak için deðiþken
    public string sonrakiSahne; // Sonraki sahneye geçmek için sahne adý

    void Update()
    {
        // 2 metre mesafe ile anahtarlarý toplama
        foreach (GameObject anahtar in GameObject.FindGameObjectsWithTag("Key"))
        {
            float mesafe = Vector3.Distance(transform.position, anahtar.transform.position);
            if (mesafe <= 2f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anahtarSayisi++;
                    Destroy(anahtar); // Anahtarý yok et
                }
            }
        }

        // Toplamda 5 anahtara ulaþtýðýnda kapý önünde "E" tuþuna basýldýðýnda sonraki sahneye geç
        if (anahtarSayisi == 5 && Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, kapý.transform.position) <= 3f)
        {
            LoadNextScene();
        }



    }
    void LoadNextScene()
    {
        // Mevcut sahne indeksini al
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Bir sonraki sahne indeksine geç
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}