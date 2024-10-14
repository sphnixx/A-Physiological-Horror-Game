using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactButton; // Assign the "E to Interact" button in the inspector
    public float interactionDistance = 2f; // Distance at which the player can interact with the NPC
    private Transform player; // Reference to the player's transform

    private void Start()
    {
        player = Camera.main.transform; // Assuming the player is the camera
        interactButton.SetActive(false); // Hide the button initially
    }

    private void Update()
    {
        // Check distance between player and NPC
        float distance = Vector3.Distance(player.position, transform.position);

        // Show or hide the interaction button based on distance
        if (distance < interactionDistance)
        {
            interactButton.SetActive(true);
            interactButton.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up); // Adjust the button position above the NPC
        }
        else
        {
            interactButton.SetActive(false);
        }

        // Handle input for interaction
        if (interactButton.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        // Add interaction logic here (if any)
        Debug.Log("Interacted with NPC");
    }
}
