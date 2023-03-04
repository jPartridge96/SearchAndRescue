using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public Vector3 startPosition;
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    public float cameraHeight = 1.5f; // added public variable
    float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = playerBody.position + new Vector3(startPosition.x, cameraHeight, startPosition.z); // modified camera position
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
