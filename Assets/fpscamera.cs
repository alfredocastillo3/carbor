using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpscamera : MonoBehaviour
{
    private Transform camera;
    public Vector2 sensibility;

    void Start()
    {
        camera = transform.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X"); // Corregir el nombre del eje del mouse
        float ver = Input.GetAxis("Mouse Y"); // Corregir el nombre del eje del mouse

        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor * sensibility.x);
        }
        if (ver != 0)
        {
            camera.Rotate(Vector3.left * ver * sensibility.y);
        }
    }
}
