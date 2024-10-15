using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Serialized fields for inventory slots
    [SerializeField] private GameObject item1; // Sword
    [SerializeField] private GameObject item2;
    [SerializeField] private GameObject item3;

    // Variables to track currently equipped item
    private GameObject equippedItem;

    private Transform swordTransform; // Sword's transform for adjustment

    void Start()
    {
        // Assuming item1 is the sword, let's deactivate it by default
        item1.SetActive(false);
        swordTransform = item1.transform; // Get the sword's transform for future adjustments

        // Make sure the sword is a child of the player
        item1.transform.SetParent(this.transform); // Attach the sword to the player
    }

    void Update()
    {
        HandleInventoryInput();
        HandleUnequipInput(); // Check for unequip action
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

    // Handle input for unequipping items
    void HandleUnequipInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Press Q to unequip the current item
        {
            UnequipItem();
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

    // Unequip the currently equipped item
    void UnequipItem()
    {
        if (equippedItem != null)
        {
            equippedItem.SetActive(false); // Deactivate the item
            Debug.Log(equippedItem.name + " unequipped");
            equippedItem = null; // Clear the reference to the equipped item
        }
    }

    // Adjust the sword's position and rotation to appear as if the player is holding it
    void AdjustSwordPosition()
    {
        // Adjust sword's position and rotation relative to the player (who is a square)
        swordTransform.localPosition = new Vector3(0.397f, 0.808f, 0.01604102f); // Position it as if held by the player
        swordTransform.localRotation = Quaternion.Euler(0f, 0f, 0f); // Adjust the rotation to make it look held
        swordTransform.localScale = new Vector3(3.423677f, 3.184142f, 1.6541f); // Scale the sword appropriately
    }
}
