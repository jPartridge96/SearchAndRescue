using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject, IInteractable
{
    public string itemName;
    public string description;
    public Sprite icon;
    public GameObject prefab;

    public string Message
    {
        get { return "Interacting with " + itemName; }
    }

    public void Interact()
    {
        Debug.Log(Message);
    }
}
