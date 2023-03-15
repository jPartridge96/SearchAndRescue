using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemSlotPrefab;
    public Transform itemSlotContainer;

    public int maxSlots = 5;

    private Dictionary<Item, GameObject> itemSlots = new Dictionary<Item, GameObject>();
    public int currentSelectedSlot = -1;

    private void Start()
    {
        inventory.OnInventoryChanged += UpdateInventoryUI;
        UpdateInventoryUI();
    }

    private void Update()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectInventorySlot(i);
            }
        }
    }

    private void SelectInventorySlot(int slotIndex)
    {
        currentSelectedSlot = slotIndex;
        HighlightSelectedSlot(slotIndex);
        
    }

    private void HighlightSelectedSlot(int slotIndex)
    {
        int currentIndex = 0;
        foreach (var itemSlotEntry in itemSlots)
        {
            GameObject itemSlot = itemSlotEntry.Value;
            Image itemImage = itemSlot.transform.Find("ItemImage").GetComponent<Image>();


            if (currentIndex == slotIndex)
            {
                itemImage.color = Color.yellow;
            }
            else
            {
                itemImage.color = Color.white;
            }
            if (slotIndex == currentIndex)
            {
                Debug.Log(itemSlot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text);
            }
            currentIndex++;


        }
    }

    private void UpdateInventoryUI()
    {
        float xOffset = 0f;

        // Iterate through the inventory items
        foreach (var itemEntry in inventory.items)
        {
            Item item = itemEntry.Key;
            int itemCount = itemEntry.Value;

            GameObject itemSlot;

            // If the item slot already exists, update its position
            if (itemSlots.ContainsKey(item))
            {
                itemSlot = itemSlots[item];
                itemSlot.transform.localPosition = new Vector3(xOffset, 0, 0);
            }
            // If the item slot does not exist, create a new one
            else
            {
                itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
                itemSlot.transform.localPosition = new Vector3(xOffset, 0, 0);

                Image itemImage = itemSlot.transform.Find("ItemImage").GetComponent<Image>();
                itemImage.sprite = item.icon;

                itemSlots[item] = itemSlot;
            }

            // Update the item count text
            TextMeshProUGUI itemText = itemSlot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            itemText.text = item.itemName + "\n" + " x" + itemCount.ToString();

            xOffset += 125f;
        }

        // Remove any item slots that are no longer needed
        List<Item> itemsToRemove = new List<Item>();

        foreach (var itemSlotEntry in itemSlots)
        {
            if (!inventory.items.ContainsKey(itemSlotEntry.Key))
            {
                itemsToRemove.Add(itemSlotEntry.Key);
                Destroy(itemSlotEntry.Value);
            }
        }

        foreach (var itemToRemove in itemsToRemove)
        {
            itemSlots.Remove(itemToRemove);
        }
    }
}
