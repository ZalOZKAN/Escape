using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerDetectSystem detectSystem;
    private PlayerInput inputActions;

    void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (detectSystem.interactable != null)
        {
            detectSystem.interactable.Interact();
        }
    }
}
