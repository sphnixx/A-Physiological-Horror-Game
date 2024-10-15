using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactButton; // Assign the "E to Interact" button in the inspector
    public AudioSource interactionSound; // Assign an AudioSource with the interaction sound in the inspector
    [SerializeField] private float interactionDistance = 2f; // Customizable distance in the Inspector
    private Transform player; // Reference to the player's transform
    private bool isPlayingSound = false; // Tracks if the interaction sound is playing

    private bool questGiven = false; // Tracks if the quest has been given
    private bool questCompleted = false; // Tracks if the quest is completed

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

            // Handle input for interaction
            if (Input.GetKeyDown(KeyCode.E) && !interactionSound.isPlaying)
            {
                Interact();
            }
        }
        else
        {
            interactButton.SetActive(false); // Hide the "E to interact" button when out of range
        }

        // Check if the sound is playing and if the player presses the Space key to skip it
        if (isPlayingSound && Input.GetKeyDown(KeyCode.Space))
        {
            interactionSound.Stop(); // Stop the sound
            isPlayingSound = false; // Update the sound status
        }
    }

    private void Interact()
    {
        // Play the interaction sound only if it's not already playing
        if (!isPlayingSound)
        {
            interactionSound.Play();
            isPlayingSound = true; // Set the flag to indicate the sound is playing
        }
        Debug.Log("Interacted with NPC");

        // Handle the quest system
        HandleQuest();
    }

    private void HandleQuest()
    {
        if (!questGiven)
        {
            // Give the quest to the player
            Debug.Log("Quest Given: Bring me some water.");
            questGiven = true;
        }
        else if (questGiven && !questCompleted)
        {
            // If the quest is already given, update progress or complete it (placeholder logic)
            Debug.Log("Quest In Progress: Have you brought the water?");
            // Add additional logic here to check if the player has the water
        }
        else if (questCompleted)
        {
            // If the quest is completed
            Debug.Log("Quest Completed: Thank you for bringing the water.");
        }
    }
}
