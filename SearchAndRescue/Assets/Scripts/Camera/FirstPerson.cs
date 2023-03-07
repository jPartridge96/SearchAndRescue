using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public Vector3 startPosition;
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    public float cameraHeight = 1.5f;
    float xRotation = 0f;
    public float interactionRange = 2f;
    private RaycastHit _hit;
    private bool _isInteractable = false;
    private Interactable _interactable;
    private GUIStyle _guiStyle = new GUIStyle();

    private void Start()
    {
        transform.position = playerBody.position + new Vector3(startPosition.x, cameraHeight, startPosition.z);
        _guiStyle.fontSize = 24;
        _guiStyle.normal.textColor = Color.white;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.E) && _isInteractable)
        {
            _interactable.Interact();
        }
    }

    private void OnGUI()
    {
        if (_isInteractable)
        {
            _guiStyle.normal.textColor = Color.green;
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height / 2 - 10, 20, 20), "+", _guiStyle);
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 20, 200, 20), _interactable.message, _guiStyle);
        }
        else
        {
            _guiStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height / 2 - 10, 20, 20), "+", _guiStyle);
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, interactionRange))
        {
            if (_hit.collider.CompareTag("Interactable"))
            {
                _isInteractable = true;
                _interactable = _hit.collider.GetComponent<Interactable>();
                _guiStyle.normal.textColor = Color.green;
            }
            else
            {
                _isInteractable = false;
                _interactable = null;
            }
        }
        else
        {
            _isInteractable = false;
            _interactable = null;
        }
    }
}
