using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InteractableObject : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public float grabDistance = 3f;

    private Camera mainCamera;
    private bool isGrabbing = false;
    private Transform grabbedObject;
    private Rigidbody grabbedRigidbody;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Movimiento del personaje
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Rotación de la cámara con el mouse
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up, mouseX);

        // Agarre de objetos
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isGrabbing)
            {
                DropObject();
            }
            else
            {
                GrabObject();
            }
        }
    }

    void GrabObject()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                grabbedObject = hit.transform;
                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();

                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.isKinematic = true;
                    grabbedObject.SetParent(transform);
                    isGrabbing = true;
                }
            }
        }
    }

    void DropObject()
    {
        if (grabbedObject != null)
        {
            if (grabbedRigidbody != null)
            {
                grabbedRigidbody.isKinematic = false;
            }

            grabbedObject.SetParent(null);
            grabbedObject = null;
            grabbedRigidbody = null;
            isGrabbing = false;
        }
    }
}
