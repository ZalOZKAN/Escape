using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket hýzý
    public float turnSpeed = 5f; // Fare ile kameranýn dönüþ hýzý
    public Transform mainCamera; // Kameranýn transform bileþeni

    private Rigidbody rb; // Karakterin Rigidbody bileþeni

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bileþenini al
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Klavye giriþini al
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Fare giriþini al
        float mouseX = Input.GetAxis("Mouse X");

        // Kameranýn bakýþ açýsýna göre hareket vektörünü oluþtur
        Vector3 camForward = mainCamera.forward;
        Vector3 camRight = mainCamera.right;
        camForward.y = 0f; // Y eksenindeki dönme engellenir
        camRight.y = 0f; // Y eksenindeki dönme engellenir
        camForward.Normalize(); // Normalize edilir
        camRight.Normalize(); // Normalize edilir

        Vector3 movement = camForward * moveVertical + camRight * moveHorizontal;

        // Hareket vektörünü normalize et ve hareket hýzýyla çarp
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        // Rigidbody'ye güç uygula
        rb.MovePosition(transform.position + movement);

        // Kamerayý yatay eksende döndür
        Vector3 rotation = new Vector3(0f, mouseX * turnSpeed, 0f);
        Quaternion deltaRotation = Quaternion.Euler(rotation * Time.deltaTime);
        mainCamera.rotation *= deltaRotation;
    }
}
