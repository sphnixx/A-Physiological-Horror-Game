using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Singleton instance
    public static PlayerInventory instance;

    // Serialized fields for inventory slots
    [SerializeField] private GameObject item1; // Sword
    [SerializeField] private GameObject item2;
    [SerializeField] private GameObject item3;
    [SerializeField] private GameObject waterItem; // Water item

    // Variables to track currently equipped item
    private GameObject equippedItem;

    private Transform swordTransform; // Sword's transform for adjustment
    private List<GameObject> inventoryItems = new List<GameObject>(); // List to store items in the inventory

    void Awake()
    {
        // Ensure that there is only one instance of PlayerInventory
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }

        // Assuming item1 is the sword, let's deactivate it by default
        item1.SetActive(false);
        swordTransform = item1.transform; // Get the sword's transform for future adjustments

        // Make sure the sword is a child of the player
        item1.transform.SetParent(this.transform); // Attach the sword to the player
    }

    void Update()
    {
        HandleInventoryInput();
    }

    // Handle input for inventory slots
    void HandleInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Press 1 to equip the sword
        {
            EquipItem(item1, "Sword equipped");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Press 2
        {
            EquipItem(item2, "Item 2 equipped");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Press 3
        {
            EquipItem(item3, "Item 3 equipped");
        }
    }

    // Equip the item and print a message
    void EquipItem(GameObject item, string message)
    {
        if (equippedItem != null)
        {
            equippedItem.SetActive(false); // Deactivate the currently equipped item
        }

        equippedItem = item;
        equippedItem.SetActive(true); // Activate the new item
        Debug.Log(message);

        // Adjust the sword position if item1 (the sword) is equipped
        if (equippedItem == item1)
        {
            AdjustSwordPosition();
        }
    }

    // Adjust the sword's position and rotation to appear as if the player is holding it
    void AdjustSwordPosition()
    {
        // Adjust sword's position and rotation relative to the player (who is a square)
        swordTransform.localPosition = new Vector3(0.397f, 0.808f, 0.01604102f); // Position it as if held by the player (adjust as needed)
        swordTransform.localRotation = Quaternion.Euler(0f, 0f, 0f); // Adjust the rotation to make it look held
        swordTransform.localScale = new Vector3(3.423677f, 3.184142f, 1.6541f); // Scale the sword appropriately
    }

    // Method to pick up an item
    public void PickUpItem(GameObject item)
    {
        inventoryItems.Add(item);
        item.SetActive(false); // Hide the item in the world
        Debug.Log(item.name + " picked up!");
    }

    // Method to check if the player has the water item
    public static bool HasItem(GameObject item)
    {
        return instance.inventoryItems.Contains(item);
    }
}
