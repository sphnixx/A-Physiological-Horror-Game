using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For the Stamina UI

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Normal walk speed
    public float sprintSpeed = 8f; // Sprint speed
    public float maxStamina = 100f; // Maximum stamina
    public float stamina = 100f; // Current stamina
    public float staminaRegenRate = 5f; // Stamina regeneration rate per second
    public float sprintStaminaDrain = 10f; // Stamina drain rate while sprinting

    public Slider staminaBar; // Reference to the Stamina UI bar
    public AudioSource footstepSound; // Reference to the AudioSource for footsteps
    public float footstepInterval = 0.5f; // Time interval between footstep sounds
    private float footstepTimer = 0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isSprinting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footstepSound = GetComponent<AudioSource>(); // Get the AudioSource attached to the player
        staminaBar.value = stamina; // Initialize stamina bar
    }

    void Update()
    {
        // Get movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check if sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0;

        // Adjust speed based on sprinting
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        // Handle stamina
        if (isSprinting)
        {
            stamina -= sprintStaminaDrain * Time.deltaTime;
            if (stamina < 0) stamina = 0;
        }
        else
        {
            stamina += staminaRegenRate * Time.deltaTime;
            if (stamina > maxStamina) stamina = maxStamina;
        }

        // Update the stamina bar
        staminaBar.value = stamina;

        // Handle footstep sound
        if (movement.magnitude > 0)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0)
            {
                footstepSound.Play(); // Play the footstep sound
                footstepTimer = isSprinting ? footstepInterval / 2f : footstepInterval; // Faster footsteps when sprinting
            }
        }
        else
        {
            footstepSound.Stop();
        }
    }

    void FixedUpdate()
    {
        // Move the player based on sprinting or normal speed
        rb.MovePosition(rb.position + movement * (isSprinting ? sprintSpeed : moveSpeed) * Time.fixedDeltaTime);
    }
}
