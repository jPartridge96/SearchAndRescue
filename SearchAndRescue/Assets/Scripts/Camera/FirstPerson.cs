using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public Inventory inventory;
    public Transform dropPosition;
    public Vector3 startPosition;
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    public float cameraHeight = 1.5f;
    float xRotation = 0f;
    public float interactionRange = 2f;
    private RaycastHit _hit;
    private bool _isInteractable = false;
    private IInteractable _interactable;
    private GUIStyle _guiStyle = new GUIStyle();
    public InventoryUI inventoryUI;

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

private void DropItem()
{
    if (inventoryUI.currentSelectedSlot >= 0 && inventoryUI.currentSelectedSlot < inventory.items.Count)
    {
        int currentIndex = 0;
        foreach (var itemEntry in inventory.items)
        {
            if (currentIndex == inventoryUI.currentSelectedSlot)
            {
                Item itemToDrop = itemEntry.Key;
                inventory.RemoveItem(itemToDrop);

                GameObject droppedItem = Instantiate(itemToDrop.prefab, dropPosition.position, Quaternion.identity);
                droppedItem.AddComponent<ItemPickup>().item = itemToDrop;
                break;
            }

            currentIndex++;
        }
    }
}





    private void OnGUI()
    {
        if (_isInteractable && _interactable != null)
        {
            _guiStyle.normal.textColor = Color.green;
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height / 2 - 10, 20, 20), "+", _guiStyle);
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 20, 200, 20), _interactable.Message, _guiStyle);
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
                _interactable = _hit.collider.GetComponent<IInteractable>();
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
