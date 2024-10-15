using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Serialized fields for inventory slots
    [SerializeField] private GameObject item1;
    [SerializeField] private GameObject item2;
    [SerializeField] private GameObject item3;

    // Variables to track currently equipped item
    private GameObject equippedItem;

    void Update()
    {
        HandleInventoryInput();
    }

    // Handle input for inventory slots
    void HandleInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Press 1
        {
            EquipItem(item1, "Item 1 equipped");
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
        equippedItem = item;
        Debug.Log(message);
    }
}
