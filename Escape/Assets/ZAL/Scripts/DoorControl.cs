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

            // Kap�n�n a��lma veya kapanma animasyonu tetiklendi�inde ilgili ses efektini �al
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
