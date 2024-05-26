using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerComponent : MonoBehaviour
{
    private PlayerInput playerInput;
    public CharacterController characterController;
    public GameObject camHolder;
    public float speed, sensivity, jumpHeight;
    private Vector2 move, look;
    private float lookRotation;
    public bool isGrounded;
    public bool isSprinting;
    public float walkSpeed, sprintSpeed;
    public Light spotlight;

    private Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck; // Zemin kontrolü için Transform bileþeni
    public float groundDistance = 0.4f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            Atla();
        }
    }

    private void SprintPressed()
    {
        isSprinting = true;
        speed = sprintSpeed;
    }

    private void SprintReleased()
    {
        isSprinting = false;
        speed = walkSpeed;
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.Player.Sprint.performed += x => SprintPressed();
            playerInput.Player.Sprint.canceled += x => SprintReleased();
            playerInput.Player.FlashLight.performed += x => OnFlashLightAction();
        }

        playerInput.Enable();
    }

    private void OnFlashLightAction()
    {
        spotlight.enabled = !spotlight.enabled;
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        // Atlamadan önce zemin kontrolü yap
        isGrounded = characterController.isGrounded;

        HareketEt();
        Bak();

        // Yerçekimi uygula
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Karakterin yere düzgün bir þekilde oturmasýný saðlar
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Check sprint state and adjust speed accordingly
        if (isGrounded && isSprinting)
        {
            speed = sprintSpeed;
        }
        else if (isGrounded && !isSprinting)
        {
            speed = walkSpeed;
        }
    }

    private void Bak()
    {
        transform.Rotate(Vector3.up * look.x * sensivity);

        lookRotation += (-look.y * sensivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    private void HareketEt()
    {
        Vector3 hareketYonu = transform.right * move.x + transform.forward * move.y;
        characterController.Move(hareketYonu * speed * Time.deltaTime);
    }

    private void Atla()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
