using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactButton; // Assign the "E to Interact" button in the inspector
    public AudioSource interactionSound; // Assign an AudioSource with the interaction sound in the inspector
    [SerializeField] private float interactionDistance = 2f; // Customizable distance in the Inspector
    private Transform player; // Reference to the player's transform
    private bool isPlayingSound = false; // Tracks if the interaction sound is playing

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
            interactButton.SetActive(false);
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
    }
}
