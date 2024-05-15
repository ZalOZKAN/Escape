using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerDetectSystem : MonoBehaviour
{
    public float rayLength = 10f;
    public IInteractable interactable;
    public LayerMask layerMask;
    public GameObject interactText;

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength, layerMask))
        {
            if (hit.collider.TryGetComponent(out IInteractable newInteractable))
            {
                interactable = newInteractable;
                interactText.SetActive(true);
            }
        }
        else
        {
            interactable = null;
            interactText.SetActive(false);
        }
    }

}
