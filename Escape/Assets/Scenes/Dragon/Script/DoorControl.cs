using UnityEngine;
using TMPro;
namespace Game.Interactable 
{
    public class DoorControl : MonoBehaviour, IInteractable
    {
        public AudioClip open, close;
        public AudioSource source;

        public bool isOpen;
        private Animator animator;

        void Start()
        {
            
            isOpen = false;
            animator = GetComponent<Animator>();
        }

        
        public void Interact()
        {
            isOpen = !isOpen;
            animator.SetBool("IsOpen", isOpen);

            // Kapýnýn açýlma veya kapanma animasyonu tetiklendiðinde ilgili ses efektini çal
            if (isOpen)
            {
                source.clip = open;
                source.Play();
            }
            else
            {
                source.clip = close;
                source.Play();
            }
        }
    }
}
