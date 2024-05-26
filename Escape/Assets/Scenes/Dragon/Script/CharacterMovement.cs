using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket h�z�
    public float turnSpeed = 5f; // Fare ile kameran�n d�n�� h�z�
    public Transform mainCamera; // Kameran�n transform bile�eni

    private Rigidbody rb; // Karakterin Rigidbody bile�eni

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Klavye giri�ini al
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Fare giri�ini al
        float mouseX = Input.GetAxis("Mouse X");

        // Kameran�n bak�� a��s�na g�re hareket vekt�r�n� olu�tur
        Vector3 camForward = mainCamera.forward;
        Vector3 camRight = mainCamera.right;
        camForward.y = 0f; // Y eksenindeki d�nme engellenir
        camRight.y = 0f; // Y eksenindeki d�nme engellenir
        camForward.Normalize(); // Normalize edilir
        camRight.Normalize(); // Normalize edilir

        Vector3 movement = camForward * moveVertical + camRight * moveHorizontal;

        // Hareket vekt�r�n� normalize et ve hareket h�z�yla �arp
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        // Rigidbody'ye g�� uygula
        rb.MovePosition(transform.position + movement);

        // Kameray� yatay eksende d�nd�r
        Vector3 rotation = new Vector3(0f, mouseX * turnSpeed, 0f);
        Quaternion deltaRotation = Quaternion.Euler(rotation * Time.deltaTime);
        mainCamera.rotation *= deltaRotation;
    }
}
