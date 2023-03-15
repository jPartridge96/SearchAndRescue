using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Item item;
    private Inventory inventory;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            inventory = player.GetComponent<Inventory>();
        }
    }

    public string Message
    {
        get { return item != null ? "Pick up " + item.itemName : "Pick up item"; }
    }

    public void Interact()
    {
        if (inventory != null)
        {
            inventory.AddItem(item);
            Debug.Log("Picked up " + item.itemName);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Cannot pick up " + item.itemName);
        }
    }
}
