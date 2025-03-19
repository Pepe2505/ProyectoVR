using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PcPlayerPanes : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;

    private Camera playerCamera;
    private XRDirectInteractor handInteractor; // Mano del jugador

    void Start()
    {
        playerCamera = Camera.main;
        handInteractor = FindObjectOfType<XRDirectInteractor>(); // Busca la mano
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerCamera != null)
        {
            Interact();
        }
    }

    void Interact()
    {
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.transform.position;
        Vector3 rayDirection = playerCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Pan"))
            {
                if (handInteractor != null && handInteractor.interactionManager != null)
                {
                    handInteractor.interactionManager.SelectEnter(handInteractor, grabInteractable);
                }
                else
                {
                    Debug.LogWarning("El interactionManager de la mano es null. Asegúrate de que el XR Interaction Manager está en la escena.");
                }
            }
        }
    }
}

