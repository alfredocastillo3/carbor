using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InteractableObject : MonoBehaviour
{
    private Rigidbody rb;
    private bool isBeingHeld = false;
    private float pickupDistance = 3f;
    private Transform holdingParent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Crea un GameObject vac�o como hijo del objeto actual para que el objeto se mantenga en la jerarqu�a al ser agarrado.
        holdingParent = new GameObject("HoldingParent").transform;
        holdingParent.parent = transform.parent;
        holdingParent.position = transform.position;
    }

    private void Update()
    {
        // Detecta si el jugador hace clic o toca la pantalla.
        if (Input.GetMouseButtonDown(0))
        {
            TryPickUpObject();
        }

        // Si el objeto est� siendo sostenido, actualiza su posici�n para seguir al puntero del rat�n o al toque en la pantalla.
        if (isBeingHeld)
        {
            MoveObject();
        }

        // Detecta si el jugador suelta el clic o el toque.
        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }
    }

    private void TryPickUpObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Hace un raycast para detectar si el objeto es alcanzado por el rayo.
        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            // Verifica si el objeto alcanzado es el mismo que estamos tratando de agarrar.
            if (hit.collider.gameObject == gameObject)
            {
                isBeingHeld = true;
                rb.isKinematic = true; // Desactiva las f�sicas para que el objeto no caiga mientras lo sostienes.
                rb.velocity = Vector3.zero; // Detiene cualquier movimiento que pueda tener el objeto.
            }
        }
    }

    private void MoveObject()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        holdingParent.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    private void DropObject()
    {
        isBeingHeld = false;
        rb.isKinematic = false; // Vuelve a activar las f�sicas para que el objeto vuelva a caer normalmente.
    }
}
